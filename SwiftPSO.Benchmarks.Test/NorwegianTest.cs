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
    public class NorwegianTest
    {
        [Test]
        public void Evaluate()
        {
            Norwegian function = new Norwegian();
            Vector v1 = new Vector(new double[] { 1, 1, 1 });
            Vector v2 = new Vector(new double[] { 0.5, 1, 0.5 });

            Assert.AreEqual(-1, function.Evaluate(v1));
            Assert.AreEqual(-0.845039, function.Evaluate(v2), 1e-6);
        }
    }
}
