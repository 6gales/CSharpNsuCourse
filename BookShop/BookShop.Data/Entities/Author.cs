using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Data.Entities
{
    public sealed class Author : IEquatable<Author>
    {
        #warning я встречал такой подход, мне он просто не нравится. слишком как-то получается на много строк может быть разнесена конфигурация
        #warning ну и fluent api EFa всё-таки позволяет больше. 
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IList<Book> Books { get; set; }

        public Author()
        {
            Name = string.Empty;
            Books = new List<Book>();
        }

        public bool Equals(Author? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is Author other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
