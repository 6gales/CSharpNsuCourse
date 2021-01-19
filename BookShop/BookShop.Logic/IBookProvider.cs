using System.Threading.Tasks;

namespace BookShop.Logic
{
    public interface IBookProvider
    {
        Task OrderBooks(decimal maxPrice, int maxCount);
    }
}