using BookShop.MessageContract;

namespace BookShop.Api.ContractImplementation
{
    public sealed class ProvideBooksRequest : IProvideBooksRequest
    {
        public int MaxBookAmount { get; set; }
        public decimal MaxTotalCost { get; set; }
        public bool AllowDebt { get; set; }
    }
}