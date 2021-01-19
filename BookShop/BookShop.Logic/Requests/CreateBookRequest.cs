using System;
using System.Collections.Generic;

namespace BookShop.Logic.Requests
{
    public sealed class CreateBookRequest
    {
        public string Title { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public IReadOnlyCollection<string> Genres { get; set; } = new string[0];

        public IReadOnlyCollection<string> Authors { get; set; } = new string[0];

        public DateTime ArrivalDate { get; set; }
    }
}
