using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoC.Tests.Services;
using System.Linq;

namespace IoC.Tests
{
    [TestClass]
    public class TypesShould
    {
        [TestMethod]
        public void FindAttributedTypes()
        {
            var types = Types.Local.With<SelectedAttribute>()
                .ToArray(); 

            CollectionAssert.AreEqual(
                new[] { typeof(SelectedType) },
                types);
        }

        [TestMethod]
        public void FindNamespacedTypes()
        {
            var types = Types.Local.KindOf("Services")
                .ToArray();

            CollectionAssert.AreEqual(
                new[] { typeof(ServiceType) },
                types);
        }

        [TestMethod]
        public void CombineTypes()
        {
            var namespaces = (Types.Local + Types.In("CodeContracts"))
                .Select(t => t.Namespace)
                .ToArray();

            CollectionAssert.Contains(namespaces, "System.Diagnostics.Contracts");
            CollectionAssert.Contains(namespaces, "IoC");
            CollectionAssert.Contains(namespaces, "IoC.Tests");
            CollectionAssert.Contains(namespaces, "IoC.Tests.Services");
        }
    }

    [Selected]
    public class SelectedType
    {
    }

    public class SelectedAttribute : Attribute
    {
    }

    namespace Services
    {
        public class ServiceType
        {
        } 
    }
}
