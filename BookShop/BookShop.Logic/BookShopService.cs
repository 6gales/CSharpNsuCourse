using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Data;
using BookShop.Data.Entities;
using BookShop.Logic.Exceptions;
using BookShop.Logic.Requests;
using BookShop.Logic.Responses;

namespace BookShop.Logic
{
    public sealed class BookShopService
    {
        private const decimal BooksLeftPercent = 10.0m;
        private const decimal MonthUnsoldBooksPercent = 0.75m;
        private const decimal NewBookCost = 0.07m;

        private readonly int _stateId;
        private readonly BookShopContextFactory _contextFactory;
        private readonly IBookProvider _bookProvider;


        public BookShopService(int stateId, BookShopContextFactory contextFactory, IBookProvider bookProvider)
        {
            _stateId = stateId;
            _contextFactory = contextFactory;
            _bookProvider = bookProvider;
            using var context = _contextFactory.GetContext();
            context.EnsureShopStateCreated(_stateId);
        }

        public async Task<BookResponse> GetBookById(int id)
        {
            await using var context = _contextFactory.GetContext();
            var bookInstance = await context.GetById<BookInstance>(id);
            if (bookInstance is null)
            {
                throw new RecordNotFoundException(id, typeof(BookInstance));
            }

            if (bookInstance.ShopStateId != _stateId)
            {
                throw new WrongShopStateException(bookInstance.ShopStateId, _stateId);
            }

            return await InstanceToResponse(context, bookInstance);
        }

        public async Task<IReadOnlyCollection<BookResponse>> GetBooks(int? offset, int? count)
        {
            await using var context = _contextFactory.GetContext();
            var books = await context.GetBooks(_stateId, offset, count);

            return await InstancesToResponse(context, books);

        }

        public async Task DeleteBookById(int id, bool isPurchase = false)
        {
            await using var context = _contextFactory.GetContext();
            var bookInstance = await context.GetById<BookInstance>(id);
            if (bookInstance is null)
            {
                throw new RecordNotFoundException(id, typeof(BookInstance));
            }

            if (bookInstance.ShopStateId != _stateId)
            {
                throw new WrongShopStateException(bookInstance.ShopStateId, _stateId);
            }

            if (isPurchase)
            {
                var state = await context.GetById<ShopState>(_stateId);
                state.Balance += bookInstance.Price;
            }

            await context.Remove(bookInstance);
        }

        public async Task<BookResponse> UpdateBook(UpdateBookRequest updateBookRequest)
        {
            #warning хорошо сделал этот метод, молодец
            await using var context = _contextFactory.GetContext();
            var bookInstance = await context.GetById<BookInstance>(updateBookRequest.Id);
            if (bookInstance is null)
            {
                throw new RecordNotFoundException(updateBookRequest.Id, typeof(BookInstance));
            }

            if (bookInstance.ShopStateId != _stateId)
            {
                throw new WrongShopStateException(bookInstance.ShopStateId, _stateId);
            }

            if (updateBookRequest.Price != null)
            {
                bookInstance.Price = (decimal) updateBookRequest.Price;
            }

            if (updateBookRequest.Authors != null)
            {
                bookInstance.Book.Authors =
                    context.GetOrCreateMany(updateBookRequest.Authors, a => new Author {Name = a});
            }

            if (updateBookRequest.Genres != null)
            {
                bookInstance.Book.Genres = context.GetOrCreateMany(updateBookRequest.Genres, g => new Genre {Name = g});
            }
            
            #warning разве что можно добавить требование для updateBookRequest, что нулами там ничего быть не может 
            #warning и просто брать все поля, которые пришли там, и обновлять данные в bookInstance

            await context.SaveChangesAsync();
            return await InstanceToResponse(context, bookInstance);
        }

        public async Task<BookResponse> CreateBook(CreateBookRequest createBookRequest)
        {
            await using var context = _contextFactory.GetContext();

            var authors = context.GetOrCreateMany(createBookRequest.Authors, a => new Author {Name = a});
            var genres = context.GetOrCreateMany(createBookRequest.Genres, g => new Genre {Name = g});
            var book = await context.GetOrCreate(new Book
            {
                Authors = authors,
                Genres = genres,
                Title = createBookRequest.Title
            });

            var shopState = await context.GetById<ShopState>(_stateId);

            var bookInstance = new BookInstance
            {
                Price = createBookRequest.Price,
                ArrivalDate = createBookRequest.ArrivalDate,
                BookId = book.Id,
                Book = book,
                ShopStateId = _stateId,
                ShopState = shopState
            };

            shopState.Balance -= bookInstance.Price * NewBookCost;
            bookInstance = await context.Create(bookInstance);

            return await InstanceToResponse(context, bookInstance);
        }

        public async Task UpdateSystem(DateTime systemTme)
        {
            await using var context = _contextFactory.GetContext();
            var state = await context.GetById<ShopState>(_stateId);
            var books = await context.GetBooks(_stateId);

            if (state.Capacity / BooksLeftPercent >= books.Count ||
                (decimal) books.Count(b => systemTme - b.ArrivalDate < state.NewBookTimeSpan) / state.Capacity >
                MonthUnsoldBooksPercent)
            {
                await _bookProvider.OrderBooks(state.Balance, state.Capacity - books.Count);
            }
        }

        private async Task<IReadOnlyCollection<BookResponse>> InstancesToResponse(BookShopContext context, IEnumerable<BookInstance> bookInstances)
        {
            var shopState = await context.GetShopState(_stateId);
            var discounts = shopState.Discounts;
            return bookInstances.Select(b => InstanceToResponse(b, discounts)).ToList();
        }

        private async Task<BookResponse> InstanceToResponse(BookShopContext context, BookInstance bookInstance)
        {
            var shopState = await context.GetShopState(_stateId);
            return InstanceToResponse(bookInstance, shopState.Discounts);
        }

        private BookResponse InstanceToResponse(BookInstance bookInstance, IEnumerable<Discount> discounts)
        {
            return new BookResponse
            {
                Id = bookInstance.Id,
                ArrivalDate = bookInstance.ArrivalDate,
                Authors = bookInstance.Book.Authors.Select(a => a.Name).ToList(),
                Genres = bookInstance.Book.Genres.Select(g => g.Name).ToList(),
                PriceBeforeDiscount = bookInstance.Price,
                PriceAfterDiscount = discounts
                    .Where(d => d.CanApply(bookInstance))
                    .Aggregate(bookInstance.Price, (price, discount) => price * discount.Multiplier)
            };

        }
    }
}