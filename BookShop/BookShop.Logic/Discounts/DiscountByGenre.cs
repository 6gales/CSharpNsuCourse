using System.Linq;
using BookShop.Data;

namespace BookShop.Logic.Discounts
{
    public class DiscountByGenre : Discount
    {
        private readonly Genre _genre;

        public DiscountByGenre(decimal multiplier, Genre genre) : base(multiplier)
        {
            _genre = genre;
        }

        protected override bool HasDiscount(BookInstance bookInstance)
        {
            return bookInstance.Book.Genres.Contains(_genre);
        }
    }
}