using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC
{
    public abstract class Types : IEnumerable<Type>
    {
        public static Types Local { get; } = new AssemblyTypes(Assemblies.Local);
        public static Types In(string directory) => In(Assemblies.In(directory));
        public static Types In(Assemblies assemblies) => new AssemblyTypes(assemblies);

        public abstract IEnumerator<Type> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Types With<TAttribyte>() where TAttribyte : Attribute
        {
            return new SelectedTypes(
                this, 
                t => t.IsDefined(typeof(TAttribyte), false));
        }

        public Types KindOf(string kind)
        {
            return new SelectedTypes(
                this,
                t => t.Namespace?.EndsWith("." + kind) == true);
        }

        public static Types operator +(Types x, Types y)
        {
            return new CombinedTypes(x, y);
        }
    }
}
