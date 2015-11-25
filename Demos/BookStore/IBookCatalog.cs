using Infra.Mixins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    [Mixin]
    public interface IBookCatalog : IBookReader, IBookWriter
    {

    }
}
