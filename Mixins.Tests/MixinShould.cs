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

        [TestMethod]
        public void ImplementGenericMethod()
        {
            var mixin = Mixin.Create<IGenericMixin>(new GenericReader());
            Assert.AreEqual(22, mixin.Read(22));
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

    public interface IGenericReader
    {
        T Read<T>(T value);
    }

    public class GenericReader : IGenericReader
    {
        public T Read<T>(T value) => value;
    }

    public interface IGenericMixin : IGenericReader
    {
    }
}
