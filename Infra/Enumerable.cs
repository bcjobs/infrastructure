using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public static class Enumerable
    {
        public static void Replace<T>(this IList<T> list, IEnumerable<T> source)
        {
            list.Clear();
            foreach (var item in source)
                list.Add(item);
        }
    }
}
