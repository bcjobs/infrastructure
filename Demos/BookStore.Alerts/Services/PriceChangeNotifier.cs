using Infra.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Alerts.Services
{
    public class PriceChangeNotifier : IHandler<If<PriceChanged, Succeeded>>
    {
        public PriceChangeNotifier(IBookReader reader)
        {
            Reader = reader;
        }

        public IBookReader Reader { get; set; }

        public async Task<bool> HandleAsync(If<PriceChanged, Succeeded> e)
        {
            // do something (e.g. raise mail message event)
            System.Diagnostics.Debug.WriteLine($"Book price changed from {e.Subject.OldPrice} to {e.Subject.NewPrice}.");
            return true;
        }
    }
}
