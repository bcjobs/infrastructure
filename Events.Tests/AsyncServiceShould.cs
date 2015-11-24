using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Events.Tests
{
    [TestClass]
    public class AsyncServiceShould
    {
        [TestMethod]
        public async Task BeWrapped()
        {
            int count = 0;
            using (Event.Subscribe(async (Greeting g) => {
                count++;
                return true;
            }))
            {
                await Services.AsyncService.Hello();   
            }
            Assert.AreEqual(1, count);
        }
    }

    public interface IAsyncService
    {
        Task<string> Hello();
    }

    public class AsyncService : IAsyncService
    {
        public async Task<string> Hello()
        {
            new Greeting("Hello").Implement();
            return await Task.Run(() => "Hello");
        }
    }

    public class Greeting
    {
        public Greeting(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
