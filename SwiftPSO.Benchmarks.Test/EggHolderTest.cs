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
    public class EggHolderTest
    {
        [Test]
        public void Evaluate()
        {
            EggHolder function = new EggHolder();
            Vector v1 = new Vector(new double[] { 200, 100 });
            Vector v2 = new Vector(new double[] { 512, 404.2319 });

            Assert.AreEqual(-166.745338888944, function.Evaluate(v1), 1e-10);
            Assert.AreEqual(-959.640662710616, function.Evaluate(v2), 1e-10);
        }
    }
}
