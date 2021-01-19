using System.Collections.Generic;

namespace BookShop.MessageContract
{
    public interface IProvideBooksResponse
    {
        IReadOnlyCollection<IBookResponse> ProvidedBooks { get; }

        decimal TotalCost { get; }
    }
}