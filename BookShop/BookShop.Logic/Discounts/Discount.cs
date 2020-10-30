using System;
using BookShop.Data;

namespace BookShop.Logic.Discounts
{
    public abstract class Discount
    {
        protected Discount(decimal multiplier)
        {
            Multiplier = multiplier;
        }

        public decimal Multiplier { get; }

        protected abstract bool HasDiscount(BookInstance bookInstance);

        public BookInstance Apply(BookInstance bookInstance, DateTime dateTime)
        {
            if (bookInstance.IsNew(dateTime) && HasDiscount(bookInstance))
            {
                return bookInstance.UpdatePrice(Multiplier);                
            }

            return bookInstance;
        }
    }
}