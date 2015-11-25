using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.EF6.Entities
{
    class EAuthor
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public virtual IList<EBook> Books { get; set; } = new List<EBook>();
    }
}
