using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Data.Entities
{
    public sealed class Genre : IEquatable<Genre>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IReadOnlyList<Book> Books { get; set; }

        public Genre()
        {
            Name = string.Empty;
            Books = new List<Book>();
        }

        public bool Equals(Genre? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Genre) obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}