/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using NUnit.Framework;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Core.Test.Types
{
    [TestFixture]
    public class RealTest
    {
        [Test]
        public void Addition()
        {
            Real op1 = new Real(5);
            Real op2 = new Real(15);

            Real result = op1 + op2;

            Assert.AreEqual(20, result.DoubleValue);
            Assert.AreEqual(5, op1.DoubleValue);
            Assert.AreEqual(15, op2.DoubleValue);
        }

        [Test]
        public void Subtraction()
        {
            Real op1 = new Real(5);
            Real op2 = new Real(15);

            Real result = op1 - op2;

            Assert.AreEqual(-10, result.DoubleValue);
            Assert.AreEqual(5, op1.DoubleValue);
            Assert.AreEqual(15, op2.DoubleValue);
        }

        [Test]
        public void Multiplication()
        {
            Real op1 = new Real(5);
            Real op2 = new Real(15);

            Real result = op1 * op2;

            Assert.AreEqual(75, result.DoubleValue);
            Assert.AreEqual(5, op1.DoubleValue);
            Assert.AreEqual(15, op2.DoubleValue);
        }
    }
}