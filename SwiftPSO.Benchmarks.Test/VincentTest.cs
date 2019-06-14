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
    public class VincentTest
    {
        [Test]
        public void Evaluate()
        {
            Vincent function = new Vincent();
            Vector v1 = new Vector(new double[] { 5, 5 });
            Vector v2 = new Vector(new double[] { 10, 10 });
            Vector v3 = new Vector(new double[] { 8, 8 });

            Assert.AreEqual(0.7537419467, function.Evaluate(v1), 1e-9);
            Assert.AreEqual(1.7194207255, function.Evaluate(v2), 1e-9);
            Assert.AreEqual(-1.861700698, function.Evaluate(v3), 1e-9);
        }
    }
}
