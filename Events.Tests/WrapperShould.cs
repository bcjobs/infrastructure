using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Events;
using Moq;
using System.Threading.Tasks;

namespace Events.Tests
{
    [TestClass]
    public class WrapperShould
    {        
        [TestMethod]
        public async Task NotifySuccess()
        {
            int count = 0;
            using (Event.Subscribe(async (TestAction e) =>
            {
                count++;
                return true;
            }))
            using (Event.Subscribe(async (If<TestAction, Succeeded> e) =>
            {
                count++;
                return true;
            }))
            {
                Services.TestService.Succeed();
            }
            Assert.AreEqual(2, count);
        }

        [TestMethod]        
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task NotifyFailure()
        {
            int count = 0;
            using (Event.Subscribe(async (TestAction e) =>
            {
                count++;
                return true;
            }))
            using (Event.Subscribe(async (If<TestAction, Failed> e) =>
            {
                count++;
                return true;
            }))            
                try
                {
                    Services.TestService.Fail();
                    Assert.Fail("Exception expected");
                }
                finally
                {
                    Assert.AreEqual(2, count);
                }
        }
    }

    public interface ITestService : IBase
    {
        void Succeed();
        void Fail();
    }

    public interface IBase
    {
        void Foo();
    }

    public class TestService : ITestService
    {
        public void Fail()
        {
            new TestAction().Implement();
            throw new InvalidOperationException();
        }

        public void Foo()
        {            
        }

        public void Succeed()
        {
            new TestAction().Implement();
        }
    }

    class TestAction
    {
    }
}
