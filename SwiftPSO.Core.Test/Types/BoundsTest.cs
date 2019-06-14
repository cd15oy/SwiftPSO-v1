/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using NUnit.Framework;
using SwiftPSO.Core.Types;
using System;

namespace SwiftPSO.Core.Test.Types
{
    [TestFixture]
    public class BoundsTest
    {
        [Test]
        public void Range()
        {
            const double lower = -5;
            const double upper = 10;
            Bounds bounds = new Bounds(lower, upper);

            Assert.AreEqual(lower, bounds.LowerBound);
            Assert.AreEqual(upper, bounds.UpperBound);

            Assert.True(bounds.IsInsideBounds(lower));
            Assert.True(bounds.IsInsideBounds(upper));

            Assert.False(bounds.IsInsideBounds(lower - 1E-15));
            Assert.False(bounds.IsInsideBounds(upper + 1E-15));
        }

        [Test]
        public void InvalidBounds()
        {
            Assert.Throws<ArgumentException>(() => new Bounds(2, 1));
            Assert.DoesNotThrow(() => new Bounds(1, 1));
        }
    }
}