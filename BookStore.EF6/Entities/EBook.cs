using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.EF6.Entities
{
    class EBook
    {
        public int Id { get; private set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public decimal Price { get; set; }
        public virtual IList<EAuthor> Authors { get; set; } = new List<EAuthor>();
    }
}
