using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests
{
    [TestClass]
    class Services
    {
        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            TestService = WrapperFactory.Create<ITestService>(new TestService());
            AsyncService = WrapperFactory.Create<IAsyncService>(new AsyncService());
        }

        public static ITestService TestService { get; private set; }
        public static IAsyncService AsyncService { get; private set; }
    }
}
