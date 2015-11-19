using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mixins.Tests
{
    [TestClass]
    public class MixinShould
    {
        [TestMethod]
        public void ImplementInterface()
        {
            var mixin = Mixin.Create<IMixin>(new A(), new B());
            Assert.AreEqual(1, mixin.GetA());
            Assert.AreEqual(2, mixin.GetB());
        }
    }

    public interface IA
    {
        int GetA();
    }

    public interface IB
    {
        int GetB();
    }

    public interface IMixin : IA, IB
    {
    }

    class A : IA
    {
        public int GetA()
        {
            return 1;
        }
    }

    class B : IB
    {
        public int GetB()
        {
            return 2;
        }
    }
}
