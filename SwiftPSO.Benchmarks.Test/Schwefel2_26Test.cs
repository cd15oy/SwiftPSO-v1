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
    public class Schwefel2_26Test
    {
        [Test]
        public void Evaluate()
        {
            Schwefel2_26 function = new Schwefel2_26();

            for (int d = 1; d <= 5; d++)
            {
                Vector v1 = Vector.Fill(420.968746, d);
                Assert.AreEqual(-418.982887272433 * d, function.Evaluate(v1), 0.0000001);
            }
        }
    }
}
