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
    public class MichalewiczTest
    {
        [Test]
        public void Evaluate()
        {
            Michalewicz function = new Michalewicz();
            Vector v1 = new Vector(new double[] { 0, 0 });
            Vector v2 = new Vector(new double[] { 1.5, 1.3 });

            Assert.AreEqual(0, function.Evaluate(v1));
            Assert.AreEqual(-0.07497735029244701, function.Evaluate(v2), 1e-16);
        }
    }
}
