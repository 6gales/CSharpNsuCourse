using System;

namespace BookShop.MessageContract
{
    public interface IBookResponse
    {
        public int Id { get; }
        public string Title { get; }
        public string Genre { get; }
        public int Price { get; }
        public bool IsNew { get; }
        public DateTime DateOfDelivery { get; }
    }
}
