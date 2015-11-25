using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Events.Dispatching
{
    class CartesianProduct<T> : IEnumerable<T[]>
    {
        public CartesianProduct(IEnumerable<IEnumerable<T>> sets)
        {
            Contract.Requires<ArgumentNullException>(sets != null);
            Contract.Requires<ArgumentNullException>(Contract.ForAll(sets, set => set != null));
            Contract.Ensures(Sets != null);
            Contract.Ensures(Contract.ForAll(Sets, set => set != null));

            Sets = sets
                .Select(s => s.ToArray())
                .ToArray();
        }

        T[][] Sets { get; }

        IEnumerable<T[]> Iterate(IEnumerable<int> idecies)
        {
            Contract.Requires<ArgumentNullException>(idecies != null);
            Contract.Ensures(Contract.Result<IEnumerable<T[]>>() != null);
            Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T[]>>(), a => a != null));

            var level = idecies.Count();
            if (level == Sets.Length)
                yield return idecies
                    .Select((idx, pos) => Sets[pos][idx])
                    .ToArray();
            else
                for (int i = 0; i < Sets[level].Length; i++)
                    foreach (var combination in Iterate(idecies.Concat(new[] { i })))
                        yield return combination;
        }

        public IEnumerator<T[]> GetEnumerator()
        {            
            Contract.Ensures(Contract.Result<IEnumerator<T[]>>() != null);
            return Iterate(Enumerable.Empty<int>())
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Contract.Ensures(Contract.Result<IEnumerator>() != null);
            return GetEnumerator();
        }
    }
}
