using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.IoC
{
    public abstract class Types : IEnumerable<Type>
    {
        public static Types Referenced { get; } = new AssemblyTypes(Assemblies.Referenced);
        public static Types Local { get; } = new AssemblyTypes(Assemblies.Local);
        public static Types In(string directory) => In(Assemblies.In(directory));
        public static Types In(Assemblies assemblies) => new AssemblyTypes(assemblies);

        public abstract IEnumerator<Type> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Types With<TAttribute>() where TAttribute : Attribute
        {
            return new SelectedTypes(
                this, 
                t => t.IsDefined(typeof(TAttribute), false));
        }

        public Types KindOf(string kind)
        {
            return new SelectedTypes(
                this,
                t => t.Namespace?.EndsWith("." + kind) == true);
        }

        public Types Only<T>()
        {
            return new SelectedTypes(
                this,
                t => typeof(T).IsAssignableFrom(t));
        }

        public Types Skip<T>()
        {
            return this.Skip(typeof(T));
        }

        public Types Skip(Type type)
        {
            return new SelectedTypes(
                this,
                t => !type.IsAssignableFrom(t));
        }

        public Types Interfaces()
        {
            return new SelectedTypes(
                this,
                t => t.IsInterface);
        }

        public Types Classes()
        {
            return new SelectedTypes(
                this,
                t => t.IsClass && !t.IsAbstract);
        }
  
        public static Types operator +(Types x, Types y)
        {
            return new CombinedTypes(x, y);
        }
    }
}
