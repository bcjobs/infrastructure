using BookStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Demo.Web.Controllers
{
    [RoutePrefix("api/books")]
    public class BookController : ApiController
    {
        public BookController(IBookCatalog catalog)
        {
            Catalog = catalog;
        }

        IBookCatalog Catalog { get; }

        [HttpGet]
        public IEnumerable<Book> Get([FromUri] BookQuery query)
        {
            query = query ?? new BookQuery();
            return Catalog.Read(query);
        }

        [Route("{id:int}")]
        [HttpGet]
        public Book Get(int id)
        {
            return Catalog.Read(id);
        }

        [HttpPost]
        public Book Post([FromBody] Book book)
        {
            var id = Catalog.Write(book);
            return Catalog.Read(id);
        }

        [Route("{id:int}")]
        [HttpPut]
        public Book Put(int id, [FromBody] Book book)
        {
            if (id != book.Id)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Catalog.Write(book);
            return book;
        }
    }
}
