using System.Collections.Generic;
using BookShop.Data;

namespace BookShop.Logic
{
    public interface IBookProvider
    {
        IEnumerable<BookInstance> ProvideBooks(decimal maxPrice, int maxCount);
    }
}