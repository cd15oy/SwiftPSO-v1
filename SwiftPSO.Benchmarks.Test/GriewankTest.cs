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
    public class GriewankTest
    {
        [Test]
        public void Evaluate()
        {
            Griewank function = new Griewank();
            Vector v1 = new Vector(new double[] { 0, 0, 0 });
            Vector v2 = new Vector(new double[] { Math.PI / 2, Math.PI / 2 });

            Assert.AreEqual(0, function.Evaluate(v1));
            Assert.AreEqual(1.0012337, function.Evaluate(v2), 1e-7);
        }
    }
}
