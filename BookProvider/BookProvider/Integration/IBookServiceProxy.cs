using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.MessageContract;

namespace BookProvider.Integration
{
    public interface IBookServiceProxy
    {
        Task<IEnumerable<IBookResponse>> GetBooks(int count);
    }
}