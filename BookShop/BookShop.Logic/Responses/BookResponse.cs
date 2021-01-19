using System;
using System.Collections.Generic;

namespace BookShop.Logic.Responses
{
    public sealed class BookResponse
    {
        public int Id { get; set; }

        public decimal PriceBeforeDiscount { get; set; }

        public decimal PriceAfterDiscount { get; set; }

        public DateTime ArrivalDate { get; set; }

        public IReadOnlyCollection<string> Authors { get; set; }

        public IReadOnlyCollection<string> Genres { get; set; }
    }
}
