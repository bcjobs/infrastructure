using Infra.Events;
using Infra.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public class PriceChanged : ILoggable<Succeeded>
    {
        public PriceChanged(int bookId, decimal oldPrice, decimal newPrice)
        {
            BookId = bookId;
            OldPrice = oldPrice;
            NewPrice = newPrice;
        }

        public int BookId { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
    }
}
