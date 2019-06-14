/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using NUnit.Framework;
using SwiftPSO.Core.Termination;
using System;

namespace SwiftPSO.Core.Test.Termination
{
    [TestFixture]
    public class MaximumValueTest
    {
        [Test]
        public void Terminate()
        {
            MaximumValue maximumValue = new MaximumValue();

            Assert.True(maximumValue.Terminate(50, 50));
            Assert.True(maximumValue.Terminate(100, 50));
            Assert.False(maximumValue.Terminate(50, 100));

        }

        [Test]
        public void CalculateCompletion()
        {
            MaximumValue maximumValue = new MaximumValue();

            Assert.AreEqual(maximumValue.CalculateCompletion(-1, 100), 0);
            Assert.AreEqual(maximumValue.CalculateCompletion(0, 100), 0);
            Assert.AreEqual(maximumValue.CalculateCompletion(50, 100), 0.5);
            Assert.AreEqual(maximumValue.CalculateCompletion(100, 100), 1.0);
            Assert.AreEqual(maximumValue.CalculateCompletion(100, 50), 1.0);

            Assert.Throws<ArgumentException>(() => maximumValue.CalculateCompletion(0, 0));
        }
    }
}
