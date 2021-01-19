namespace BookShop.Data.Entities
{
    public class DiscountByGenre : Discount
    {
        public Genre Genre { get; set; }

        protected override bool HasDiscount(BookInstance bookInstance)
        {
            return bookInstance.Book.Genres.Contains(Genre);
        }
    }
}