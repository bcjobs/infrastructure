using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Linq;
using System.IO;
using Infra.IoC;

namespace Infra.Events.Tests
{
    [TestClass]
    public class AssembliesShould
    {
        [TestMethod]
        public void ListLocalAssembly()
        {
            var names = Assemblies.Local
                .Select(a => Path.GetFileName(a.Location))
                .ToArray();

            CollectionAssert.Contains(names, "IoC.dll");
        }

        [TestMethod]
        public void ListSubdirectoryAssembly()
        {
            var names = Assemblies.In("CodeContracts")
                .Select(a => Path.GetFileName(a.Location))
                .ToArray();

            CollectionAssert.Contains(names, "IoC.Tests.Contracts.dll");
        }

        [TestMethod]
        public void CombineAssemblies()
        {
            var names = (Assemblies.Local + Assemblies.In("CodeContracts"))
                .Select(a => Path.GetFileName(a.Location))
                .ToArray();

            CollectionAssert.Contains(names, "IoC.dll");
            CollectionAssert.Contains(names, "IoC.Tests.Contracts.dll");
        }
    }
}
