using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Data.Entities
{
    public sealed class ShopState : IEquatable<ShopState>
    {
        [Key]
        public int Id { get; set; }

        public decimal Balance { get; set; }

        public int Capacity { get; set; }

        public TimeSpan NewBookTimeSpan { get; set; }

        public IList<BookInstance> BookInstances { get; set; }

        public IList<Discount> Discounts { get; set; }

        public ShopState()
        {
            BookInstances = new List<BookInstance>();
            Discounts = new List<Discount>();
        }

        public bool Equals(ShopState? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ShopState) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
