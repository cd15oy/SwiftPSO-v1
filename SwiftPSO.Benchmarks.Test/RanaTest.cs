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
    public class RanaTest
    {
        [Test]
        public void Evaluate()
        {
            Rana function = new Rana();
            Vector v1 = new Vector(new double[] { -300.3376, 500 });
            Vector v2 = new Vector(new double[] { -500, -500, -500 });

            Assert.AreEqual(-500.802160296661, function.Evaluate(v1), 0.00000001);
            Assert.AreEqual(-928.5478, function.Evaluate(v2), 0.0001);
        }
    }
}
