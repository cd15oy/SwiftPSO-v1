/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using NUnit.Framework;
using SwiftPSO.Core.Types;
using System;

namespace SwiftPSO.Benchmarks.Test
{
    [TestFixture]
    public class RastriginTest
    {
        [Test]
        public void Evaluate()
        {
            Rastrigin function = new Rastrigin();
            Vector v1 = new Vector(new double[] { Math.PI / 2, Math.PI / 2 });
            Vector v2 = new Vector(new double[] { 0, 0 });

            Assert.AreEqual(42.9885094392, function.Evaluate(v1), 0.0000000001);
            Assert.AreEqual(0, function.Evaluate(v2));
        }
    }
}
