using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Data.Entities
{
    public sealed class Book : IEquatable<Book>
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        public IList<Author> Authors { get; set; }
        
        public IList<Genre> Genres { get; set; }

        public Book()
        {
            Title = string.Empty;
            Genres = new List<Genre>();
            Authors = new List<Author>();
        }

        public bool Equals(Book? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Title == other.Title && Authors.Equals(other.Authors) && Genres.Equals(other.Genres);
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is Book other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Authors, Genres);
        }
    }
}