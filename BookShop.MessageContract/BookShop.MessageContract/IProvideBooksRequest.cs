namespace BookShop.MessageContract
{
    public interface IProvideBooksRequest
    {
        int MaxBookAmount { get; }

        decimal MaxTotalCost { get; }

        bool AllowDebt { get; }
    }
}
