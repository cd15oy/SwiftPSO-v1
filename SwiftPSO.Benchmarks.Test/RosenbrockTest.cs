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
    public class RosenbrockTest
    {
        [Test]
        public void Evaluate()
        {
            Rosenbrock function = new Rosenbrock();

            Vector v1 = new Vector(new double[] { 1, 2, 3 });
            Vector v2 = new Vector(new double[] { 3, 2, 1 });
            Vector v3 = new Vector(new double[] { 1, 2, 3, 4 });

            Assert.AreEqual(201, function.Evaluate(v1));
            Assert.AreEqual(5805, function.Evaluate(v2));
            Assert.AreEqual(2705, function.Evaluate(v3));
        }
    }
}
