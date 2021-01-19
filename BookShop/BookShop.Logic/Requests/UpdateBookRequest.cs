using System.Collections.Generic;

namespace BookShop.Logic.Requests
{
    public sealed class UpdateBookRequest
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public decimal? Price { get; set; }

        public IReadOnlyCollection<string>? Genres { get; set; }

        public IReadOnlyCollection<string>? Authors { get; set; }
    }
}
