using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infra.IoC
{
    public abstract class Assemblies : IEnumerable<Assembly>
    {
        public static Assemblies Referenced { get; } = new ReferencedAssemblies();

        static string LocalDirectory { get; } = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static Assemblies Local { get; } = new DirectoryAssemblies(LocalDirectory);
        public static Assemblies In(string directory) => new DirectoryAssemblies(directory);

        public static Assemblies Entry => new CombinedAssemblies(Assembly.GetEntryAssembly());

        public abstract IEnumerator<Assembly> GetEnumerator();
   
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void ForAll(Action<Assembly> action)
        {
            foreach (var assembly in this)
                action(assembly);
        }

        public static Assemblies operator+(Assemblies x, Assemblies y)
        {
            return new CombinedAssemblies(x, y);
        }

        public Assemblies AndOf<T>() =>
            AndOf(typeof(T));

        public Assemblies AndOf(params Type[] types) =>
            new CombinedAssemblies(this, 
                new CombinedAssemblies(types
                    .Select(t => t.Assembly)
                    .ToArray()));
    }
}
