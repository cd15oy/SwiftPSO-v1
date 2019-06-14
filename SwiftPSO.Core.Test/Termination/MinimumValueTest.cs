/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using NUnit.Framework;
using SwiftPSO.Core.Termination;

namespace SwiftPSO.Core.Test.Termination
{
    [TestFixture]
    public class MinimumValueTest
    {
        [Test]
        public void Terminate()
        {
            MinimumValue minimumValue = new MinimumValue();

            Assert.True(minimumValue.Terminate(0, 0));
            Assert.True(minimumValue.Terminate(0, 1));
            Assert.True(minimumValue.Terminate(-1, 0));
            Assert.True(minimumValue.Terminate(5, 10));
            Assert.False(minimumValue.Terminate(1, 0));
            Assert.False(minimumValue.Terminate(0, -1));
            Assert.False(minimumValue.Terminate(10, 5));


        }

        [Test]
        public void CalculateCompletion()
        {
            MinimumValue minimumValue = new MinimumValue();

            Assert.AreEqual(0, minimumValue.CalculateCompletion(8, 0)); //stores the maximum value on the first instance
            Assert.AreEqual(0.5, minimumValue.CalculateCompletion(4, 0));
            Assert.AreEqual(0.5, minimumValue.CalculateCompletion(8, 0)); //should store the previously calculated value of 0.5
            Assert.AreEqual(0.75, minimumValue.CalculateCompletion(2, 0));
            Assert.AreEqual(1.0, minimumValue.CalculateCompletion(0, 0));
            Assert.AreEqual(1.0, minimumValue.CalculateCompletion(-1, 0)); //test that we have already attained 100%
        }
    }
}
