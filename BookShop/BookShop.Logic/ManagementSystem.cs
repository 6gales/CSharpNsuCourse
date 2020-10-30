using System;
using System.Collections.Generic;
using System.Linq;
using BookShop.Data;
using BookShop.Logic.Discounts;

namespace BookShop.Logic
{
    public class ManagementSystem
    {
        private const int BooksLeftPercent = 10;
        private const decimal MonthUnsoldBooksPercent = 0.75m;
        private const decimal NewBookCost = 0.07m;

        private readonly IBookShopRepository _repository;
        private readonly IBookProvider _bookProvider;
        
        public decimal Balance { get; private set; }

        public IReadOnlyCollection<BookInstance> BooksWithDiscounts { get; private set; }

        public ManagementSystem(IBookShopRepository repository, IBookProvider bookProvider, decimal balance = 0.0m)
        {
            _repository = repository;
            _bookProvider = bookProvider;
            Balance = balance;
        }

        public void SystemUpdate(DateTime dateTime, IReadOnlyCollection<Discount> discounts)
        {
            if (_repository.Capacity / BooksLeftPercent > _repository.Count || NeedOrder(dateTime))
            {
                var books = _bookProvider.ProvideBooks(Balance, _repository.Capacity - _repository.Count);
                foreach (var book in books)
                {
                    Balance -= book.Price * NewBookCost;
                    _repository.AddBook(book);
                }
            }

            ApplyDiscounts(dateTime, discounts);
        }
        
        public BookInstance SellBook(int id)
        {
            var book = _repository.DeleteBook(id);
            Balance += book.Price;
            return book;
        }

        private bool NeedOrder(DateTime dateTime)
        {
            var count = (decimal) _repository.BookInstances.Count(b => b.IsNew(dateTime));

            return count / _repository.Capacity > MonthUnsoldBooksPercent;
        }

        private void ApplyDiscounts(DateTime dateTime, IReadOnlyCollection<Discount> discounts)
        {
            BooksWithDiscounts = _repository.BookInstances.Select(bookInstance =>
                discounts.Aggregate(bookInstance, 
                    (current, discount) => discount.Apply(current, dateTime))
                ).ToList();
        }
    }
}