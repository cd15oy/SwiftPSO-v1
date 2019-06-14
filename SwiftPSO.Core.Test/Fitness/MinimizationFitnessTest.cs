/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using NUnit.Framework;
using SwiftPSO.Core.Fitness;

namespace SwiftPSO.Core.Test.Fitness
{
    [TestFixture]
    public class MinimizationFitnessTest
    {
        [Test]
        public void CompareTo()
        {
            IFitness f1 = new MinimizationFitness(1);
            IFitness f2 = new MinimizationFitness(2);

            Assert.Greater(f1.CompareTo(f2), 0);
            Assert.Less(f2.CompareTo(f1), 0);

            Assert.AreEqual(f1.CompareTo(f1), 0);
            Assert.AreEqual(f2.CompareTo(f2), 0);

            Assert.Greater(f1.CompareTo(InferiorFitness.GetInstance()), 0);
            Assert.Greater(f2.CompareTo(InferiorFitness.GetInstance()), 0);

            Assert.Less(InferiorFitness.GetInstance().CompareTo(f1), 0);
            Assert.Less(InferiorFitness.GetInstance().CompareTo(f2), 0);
        }
    }
}