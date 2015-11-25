using BookStore.EF6.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.EF6.Services
{
    public class BookReader : IBookReader
    {
        public IEnumerable<Book> Read(BookQuery query)
        {
            var books = from b in new StoreContext().Books.AsNoTracking()
                        where b.PublishedAt >= query.After
                        where b.PublishedAt < query.Before
                        where query.Author == null || b.Authors.Any(a => a.Name.Contains(query.Author))
                        where query.Title == null || b.Title.Contains(query.Title)
                        orderby b.PublishedAt descending
                        select b;

            return books
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .AsEnumerable()
                .Select(Book);
        }

        public Book Read(int id)
        {
            return new StoreContext()
                .Books
                .AsEnumerable()
                .Select(Book)
                .FirstOrDefault();
        }

        Book Book(EBook eBook)
        {
            return new Book(
                eBook.Id,
                eBook.Isbn,
                eBook.Title,
                eBook.PublishedAt,
                eBook.Price,
                eBook.Authors.Select(a => a.Name)
            );
        }
    }
}
