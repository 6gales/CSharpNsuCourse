using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Data.Entities
{
    public sealed class BookInstance
    {
        [Key]
        public int Id { get; set; }

        public decimal Price { get; set; }
        
        public DateTime ArrivalDate { get; set; }
        
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [ForeignKey(nameof(ShopState))]
        public int ShopStateId { get; set; }
        public ShopState ShopState { get; set; }

        public BookInstance()
        {
            Book = new Book();
            ShopState = new ShopState();
        }
    }
}