/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using NUnit.Framework;
using SwiftPSO.Core.Random;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Core.Test.Random
{
    [TestFixture]
    public class RandomProviderTest
    {
        int iters = 100;

        [Test]
        public void NextDouble()
        {
            for (int i = 0; i < iters; i++)
            {
                double rand = RandomProvider.NextDouble();
                Assert.GreaterOrEqual(rand, 0);
                Assert.LessOrEqual(rand, 1);
            }
        }

        [Test]
        public void NextDoubleBounds()
        {
            Bounds bounds = new Bounds(-2.5, 6.3);

            for (int i = 0; i < iters; i++)
            {
                double rand = RandomProvider.NextDouble(bounds);
                Assert.GreaterOrEqual(rand, bounds.LowerBound);
                Assert.LessOrEqual(rand, bounds.UpperBound);
            }
        }
    }
}
