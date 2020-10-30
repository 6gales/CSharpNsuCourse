using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Api.JsonModels;

namespace BookShop.Api.Proxy
{
    public interface IBookServiceProxy
    {
        Task<IEnumerable<JsonBook>> GetBooks(int count);
    }
}