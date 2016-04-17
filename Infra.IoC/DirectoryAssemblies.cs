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
    class DirectoryAssemblies : Assemblies
    {
        public DirectoryAssemblies(string directory)            
        {
            Directory = new DirectoryInfo(directory);
        }

        DirectoryInfo Directory { get; }

        public override IEnumerator<Assembly> GetEnumerator()
        {
            var assemblies = from fi in Directory.EnumerateFiles()
                             where fi.Extension == ".dll" || fi.Extension == ".exe"
                             let a = AssemblyOrNull(fi.FullName)
                             where a != null
                             select a;

            return assemblies.GetEnumerator();
        }

        [DebuggerHidden]
        Assembly AssemblyOrNull(string path)
        {
            try
            {
                return Assembly.LoadFile(path);
            }
            catch
            {
                return null;
            }
        }
    }
}
