using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC
{
    [ContractClass(typeof(TypesContract))]
    public abstract class Types : IEnumerable<Type>
    {
        public static Types Local { get; } = new AssemblyTypes(Assemblies.Local);
        public static Types In(string directory) => In(Assemblies.In(directory));
        public static Types In(Assemblies assemblies) => new AssemblyTypes(assemblies);

        public abstract IEnumerator<Type> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            Contract.Ensures(Contract.Result<IEnumerator>() != null);
            return GetEnumerator();
        }

        public Types With<TAttribute>() where TAttribute : Attribute
        {
            Contract.Ensures(Contract.Result<Types>() != null);
            return new SelectedTypes(
                this, 
                t => t.IsDefined(typeof(TAttribute), false));
        }

        public Types KindOf(string kind)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(kind));
            Contract.Ensures(Contract.Result<Types>() != null);
            return new SelectedTypes(
                this,
                t => t.Namespace?.EndsWith("." + kind) == true);
        }

        public Types Interfaces()
        {
            Contract.Ensures(Contract.Result<Types>() != null);
            return new SelectedTypes(
                this,
                t => t.IsInterface);
        }

        public Types Classes()
        {
            Contract.Ensures(Contract.Result<Types>() != null);
            return new SelectedTypes(
                this,
                t => t.IsClass && !t.IsAbstract);
        }

        public static Types operator +(Types x, Types y)
        {
            Contract.Requires<ArgumentNullException>(x != null);
            Contract.Requires<ArgumentNullException>(y != null);
            Contract.Ensures(Contract.Result<Types>() != null);
            return new CombinedTypes(x, y);
        }
    }

    [ContractClassFor(typeof(Types))]
    abstract class TypesContract : Types
    {
        public override IEnumerator<Type> GetEnumerator()
        {
            Contract.Ensures(Contract.Result<IEnumerator<Type>>() != null);
            throw new NotImplementedException();
        }
    }
}
