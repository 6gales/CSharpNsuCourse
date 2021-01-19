using System.Linq;
using System.Threading.Tasks;
using BookProvider.ContractImplementation;
using BookProvider.Integration;
using BookShop.MessageContract;
using MassTransit;

namespace BookProvider.Rabbit
{
    public sealed class BookProducer
    {
        private readonly IBookServiceProxy _bookServiceProxy;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IMassTransitConfiguration _configuration;

        public BookProducer(IBookServiceProxy bookServiceProxy, ISendEndpointProvider sendEndpointProvider,
            IMassTransitConfiguration configuration)
        {
            _bookServiceProxy = bookServiceProxy;
            _sendEndpointProvider = sendEndpointProvider;
            _configuration = configuration;
        }

        public async Task ProduceBooks(IProvideBooksRequest provideBooksRequest)
        {
            var books = await _bookServiceProxy.GetBooks(provideBooksRequest.MaxBookAmount);
            var totalCost = 0.0m;

            if (!provideBooksRequest.AllowDebt)
            {
                books = books.TakeWhile(b =>
                {
                    totalCost += b.Price * 0.07m;
                    return totalCost < provideBooksRequest.MaxTotalCost;
                });
            }

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(_configuration.QueueAddress);
            await endpoint.Send(new ProvideBooksResponse(books.ToList(), totalCost));
        }
    }
}