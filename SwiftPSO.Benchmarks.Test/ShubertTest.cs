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
    public class ShubertTest
    {
        [Test]
        public void Evaluate()
        {
            Shubert function = new Shubert();
            Vector v1 = new Vector(new double[] { 0, 0 });
            Vector v2 = new Vector(new double[] { -8, -8 });
            Vector v3 = new Vector(new double[] { -7.2, -7.7 });

            Assert.AreEqual(19.875836249, function.Evaluate(v1), 1e-9);
            Assert.AreEqual(7.507985827, function.Evaluate(v2), 1e-9);
            Assert.AreEqual(-157.071676802, function.Evaluate(v3), 1e-9);
        }
    }
}
