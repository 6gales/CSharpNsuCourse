using System.Threading.Tasks;
using BookShop.Logic;
using BookShop.Logic.Requests;
using BookShop.MessageContract;
using MassTransit;

namespace BookShop.Api.Rabbit
{
    public sealed class ProvideBookResponseConsumer : IConsumer<IProvideBooksResponse>
    {
        private readonly BookShopService _bookShopService;

        public ProvideBookResponseConsumer(BookShopService bookShopService)
        {
            _bookShopService = bookShopService;
        }

        public async Task Consume(ConsumeContext<IProvideBooksResponse> context)
        {
            foreach (var book in context.Message.ProvidedBooks)
            {
                await _bookShopService.CreateBook(new CreateBookRequest
                {
                    Title = book.Title,
                    Price = book.Price,
                    Authors = new[] {string.Empty},
                    Genres = new[] {book.Genre}
                });
            }
        }
    }
}