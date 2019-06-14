/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using NUnit.Framework;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Benchmarks.Test
{
    [TestFixture]
    public class AckleyTest
    {
        [Test]
        public void Evaluate()
        {
            Ackley function = new Ackley();
            Vector v1 = new Vector(new double[] { 1, 2, 3 });
            Vector v2 = new Vector(new double[] { 0, 0, 0 });

            Assert.AreEqual(7.0164536, function.Evaluate(v1), 1e-8);
            Assert.AreEqual(0, function.Evaluate(v2), 1e-8);
        }
    }
}
