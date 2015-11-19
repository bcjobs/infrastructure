using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IoC
{
    public abstract class Assemblies : IEnumerable<Assembly>
    {
        static string LocalDirectory { get; } = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static Assemblies Local { get; } = new DirectoryAssemblies(LocalDirectory);
        public static Assemblies In(string directory) => new DirectoryAssemblies(directory);

        public abstract IEnumerator<Assembly> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static Assemblies operator+(Assemblies x, Assemblies y)
        {
            return new CombinedAssemblies(x, y);
        }
    }
}
