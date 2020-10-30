using System.Collections.Generic;

namespace BookShop.Data
{
    public class Book
    {
        public Book(int id, string title, string author, IReadOnlyList<Genre> genres)
        {
            Id = id;
            Genres = genres;
            Title = title;
            Author = author;
        }

        public int Id { get; }
        public string Title { get; }
        public string Author { get; }
        public IReadOnlyList<Genre> Genres { get; }
    }
}