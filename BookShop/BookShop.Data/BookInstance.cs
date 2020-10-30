using System;

namespace BookShop.Data
{
    public class BookInstance
    {
        public BookInstance(DateTime arrivalDate, decimal price, Book book, int id)
        {
            ArrivalDate = arrivalDate;
            Price = price;
            Book = book;
            Id = id;
        }

        public decimal Price { get; }
        public DateTime ArrivalDate { get; }
        public Book Book { get; }
        public int Id { get; }

        public BookInstance UpdatePrice(decimal multiplier)
        {
            return new BookInstance(ArrivalDate, Price * multiplier, Book, Id);
        }

        public bool IsNew(DateTime now) => now - ArrivalDate <= TimeSpan.FromDays(30);
    }
}