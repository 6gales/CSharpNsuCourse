using System.Threading.Tasks;
using BookShop.MessageContract;
using MassTransit;

namespace BookProvider.Rabbit
{
    public sealed class ProvideBooksRequestConsumer : IConsumer<IProvideBooksRequest>
    {
        private readonly BookProducer _bookProducer;

        public ProvideBooksRequestConsumer(BookProducer bookProducer)
        {
            _bookProducer = bookProducer;
        }

        public async Task Consume(ConsumeContext<IProvideBooksRequest> context)
        {
            await _bookProducer.ProduceBooks(context.Message);
        }
    }
}