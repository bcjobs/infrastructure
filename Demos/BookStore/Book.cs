using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public class Book
    {
        public Book(
            int id, 
            string isbn,
            string title, 
            DateTime publishedAt, 
            decimal price, 
            IEnumerable<string> authors)
        {
            Id = id;
            Isbn = isbn;
            Title = title;
            PublishedAt = publishedAt;
            Price = price;
            Authors = authors;
        }

        public int Id { get; }
        public string Isbn { get; }
        public string Title { get; }
        public DateTime PublishedAt { get; }
        public decimal Price { get; }
        public IEnumerable<string> Authors { get; }
    }
}
