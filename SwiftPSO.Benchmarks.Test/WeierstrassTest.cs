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
    public class WeierstrassTest
    {
        [Test]
        public void Evaluate()
        {
            Weierstrass function = new Weierstrass();
            Vector v1 = new Vector(new double[] { 0, 0, 0 });
            Assert.AreEqual(0, function.Evaluate(v1));

            Vector v2 = new Vector(new double[] { 0.1, 0.1 });
            function = new Weierstrass(0.5, 3, 2);
            Assert.AreEqual(1.786474508, function.Evaluate(v2), 1e-9);
        }
    }
}
