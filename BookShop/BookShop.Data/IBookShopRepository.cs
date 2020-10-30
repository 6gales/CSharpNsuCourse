using System.Collections.Generic;

namespace BookShop.Data
{
    public interface IBookShopRepository
    {
        int Capacity { get; }
        
        int Count { get; }
        
        IReadOnlyCollection<BookInstance> BookInstances { get; }

        BookInstance DeleteBook(int id);
        
        void AddBooks(IEnumerable<BookInstance> bookInstances);
        
        void AddBook(BookInstance bookInstances);
    }
}