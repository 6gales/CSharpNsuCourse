using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Data.Entities
{
    public abstract class Discount
    {
        [Key]
        public int Id { get; set; }

        public decimal Multiplier { get; set; }

        public bool IsEnabled { get; set; }

        public bool ApplyOnNew { get; set; }

        public IList<ShopState> ShopStates { get; set; }

        public Discount()
        {
            ShopStates = new List<ShopState>();
        }

        protected abstract bool HasDiscount(BookInstance bookInstance);

        public bool CanApply(BookInstance bookInstance)
        {
            return IsEnabled && HasDiscount(bookInstance);
        }
    }
}