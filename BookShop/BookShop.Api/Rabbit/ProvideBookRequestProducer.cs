using System.Threading.Tasks;
using BookShop.Api.ContractImplementation;
using BookShop.Logic;
using BookShop.MessageContract;
using MassTransit;

namespace BookShop.Api.Rabbit
{
    public sealed class ProvideBookRequestProducer : IBookProvider
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IMassTransitConfiguration _configuration;

        public ProvideBookRequestProducer(ISendEndpointProvider sendEndpointProvider, IMassTransitConfiguration configuration)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _configuration = configuration;
        }
        public async Task OrderBooks(decimal maxPrice, int maxCount)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(_configuration.QueueAddress);
            await endpoint.Send(new ProvideBooksRequest{MaxBookAmount = maxCount, MaxTotalCost = maxPrice, AllowDebt = true});
        }
    }
}
