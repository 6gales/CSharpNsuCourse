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
                #warning если какая-то книга по какой-то причине не сможет, например, добавиться в базу (полетит какой-то exception)
                #warning то всё сообщение упадёт в очередь ошибок, что ведёт к тому, что часть книг уже добавиться и ты не сможешь 
                #warning снова послать сообщение в обработку. 
                #warning варианта исправления два - либо накручить работу с базой через транзакции, либо вызов ниже обернуть в try\catch 
                #warning с соответствующим логированием в блоке cathc (например, полное тело книги которая не добавилось)
                #warning в этом случае можно будет хотя бы ручками потом добавить данные в базу. 
                #warning хотя транзакции, конечно, лучше. 
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