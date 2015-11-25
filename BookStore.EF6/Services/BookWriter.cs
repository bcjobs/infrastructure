using Base;
using BookStore.EF6.Entities;
using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.EF6.Services
{
    public class BookWriter : IBookWriter
    {
        public BookWriter(IClock clock)
        {
            Clock = clock;
        }

        IClock Clock { get; }

        public int Write(Book book)
        {
            var context = new StoreContext();
            var eBook = EBook(context, book);

            if (eBook.Id != 0 && eBook.Price != book.Price)
                new PriceChanged(eBook.Id, eBook.Price, book.Price).Implement();

            eBook.Isbn = book.Isbn;
            eBook.Title = book.Title;
            eBook.PublishedAt = book.PublishedAt;
            eBook.Price = book.Price;
            eBook.Authors.Replace(book.Authors.Select(a => EAuthor(context, a)));

            context.SaveChanges();
            return eBook.Id;
        }

        EBook EBook(StoreContext context, Book book)
        {
            var eBook = context.Books.FirstOrDefault(b => b.Id == book.Id);
            if (eBook != null)
                return eBook;

            eBook = new EBook { RegisteredAt = Clock.GetTime() };
            context.Books.Add(eBook);
            return eBook;
        }

        EAuthor EAuthor(StoreContext context, string name)
        {
            var eAuthor = context.Authors.FirstOrDefault(a => a.Name == name);
            if (eAuthor != null)
                return eAuthor;

            eAuthor = new EAuthor { Name = name };
            return eAuthor;
        }
    }
}
