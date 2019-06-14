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
    public class SphericalTest
    {
        [Test]
        public void Evaluate()
        {
            Spherical function = new Spherical();
            Vector v1 = new Vector(new double[] { 1, 2, 3 });
            Vector v2 = new Vector(new double[] { 0, 0, 0 });

            Assert.AreEqual(14, function.Evaluate(v1));
            Assert.AreEqual(0, function.Evaluate(v2));
        }
    }
}
