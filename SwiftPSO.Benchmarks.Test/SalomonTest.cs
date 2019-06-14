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
    public class SalomonTest
    {
        [Test]
        public void Evaluate()
        {
            Salomon function = new Salomon();
            Vector v1 = new Vector(new double[] { 0, 0 });
            Vector v2 = new Vector(new double[] { 1, 2 });

            Assert.AreEqual(0, function.Evaluate(v1));
            Assert.AreEqual(1.13618107303302, function.Evaluate(v2), 1e-10);
        }
    }
}
