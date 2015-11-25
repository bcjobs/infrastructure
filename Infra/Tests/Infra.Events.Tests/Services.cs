using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Events.Tests
{
    [TestClass]
    class Services
    {
        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            TestService = WrapperFactory.Create<ITestService>(new TestService());
            AsyncService = WrapperFactory.Create<IAsyncService>(new AsyncService());
            GenericService = WrapperFactory.Create<IGenericService<string>>(new GenericService<string>());
        }

        public static ITestService TestService { get; private set; }
        public static IAsyncService AsyncService { get; private set; }
        public static IGenericService<string> GenericService { get; private set; }
    }
}
