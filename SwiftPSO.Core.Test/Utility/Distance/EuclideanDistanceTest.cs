/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using NUnit.Framework;
using SwiftPSO.Core.Types;
using SwiftPSO.Core.Utility.Distance;
using System;

namespace SwiftPSO.Core.Test.Utility.Distance
{
    [TestFixture]
    public class EuclideanDistanceTest
    {
        [Test]
        public void Distance()
        {
            EuclideanDistance distance = new EuclideanDistance();

            Vector v1 = new Vector(new double[] { 4, 3, 2 });
            Vector v2 = new Vector(new double[] { 2, 3, 4 });

            Assert.AreEqual(Math.Sqrt(8), distance.Measure(v1, v2));
            Assert.AreEqual(distance.Measure(v2, v1), distance.Measure(v1, v2));

            Assert.AreEqual(0, distance.Measure(v1, v1));
            Assert.AreEqual(0, distance.Measure(v2, v2));
        }

        [Test]
        public void DifferentDimensionException()
        {
            EuclideanDistance distance = new EuclideanDistance();
            Vector v1 = new Vector(new double[] { 1, 2, 3 });
            Vector v2 = new Vector(new double[] { 1, 2, 3, 4 });
            Assert.Throws<ArgumentException>(() => distance.Measure(v1, v2));
            Assert.Throws<ArgumentException>(() => distance.Measure(v2, v1));
        }

    }
}