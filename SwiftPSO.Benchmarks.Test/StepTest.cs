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
    public class StepTest
    {
        [Test]
        public void Evaluate()
        {
            Step function = new Step();
            Vector v1 = new Vector(new double[] { -0.5, -0.5 });
            Vector v2 = new Vector(new double[] { 1, 2 });

            Assert.AreEqual(0, function.Evaluate(v1));
            Assert.AreEqual(5, function.Evaluate(v2));
        }
    }
}
