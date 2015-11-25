using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Infra.Events.Tests
{
    [TestClass]
    public class GenericServiceShould
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
                await Services.GenericService.Hello();   
            }
            Assert.AreEqual(1, count);
        }
    }

    public interface IGenericService<T>
    {
        Task<string> Hello();
    }

    public class GenericService<T> : IGenericService<T>
    {
        public async Task<string> Hello()
        {
            new Greeting("Hello").Implement();
            return await Task.Run(() => "Hello");
        }
    }
}
