/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using Moq;
using NUnit.Framework;
using SwiftPSO.Core.Algorithm;
using SwiftPSO.Core.Measurement;
using SwiftPSO.Core.Termination;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Core.Test.Termination
{
    [TestFixture]
    public class MeasurementTerminationTest
    {
        [Test]
        public void Terminate()
        {
            Mock<IAlgorithm> mockAlgorithm = new Mock<IAlgorithm>();

            //set maximum iteration to 5
            MeasurementTermination termination = new MeasurementTermination(new Iterations(), new MaximumValue(), 10);

            //set algorithm iteration to return 5, thus terminate should be false and we should be at 50%
            mockAlgorithm.Setup(x => x.Iteration).Returns(5);
            Assert.False(termination.Terminate(mockAlgorithm.Object));
            Assert.AreEqual(0.5, termination.CalculateCompletion(mockAlgorithm.Object));

            //set algorithm iteration to return 10, thus termination should be true and we should be at 100%
            mockAlgorithm.Setup(x => x.Iteration).Returns(10);
            Assert.True(termination.Terminate(mockAlgorithm.Object));
            Assert.AreEqual(1.0, termination.CalculateCompletion(mockAlgorithm.Object));


            //test minimization termination using a mock measurement and the mock algorithm
            Mock<IMeasurement<Real>> measureMock = new Mock<IMeasurement<Real>>();

            //set measurment output to 10
            measureMock.Setup(x => x.GetValue(mockAlgorithm.Object)).Returns(new Real(10));

            //set minimum value to 5, thus termination should be false
            termination = new MeasurementTermination(measureMock.Object, new MinimumValue(), 5);
            Assert.False(termination.Terminate(mockAlgorithm.Object));

            //set minimum value to 3, thus termination should be true
            measureMock.Setup(x => x.GetValue(mockAlgorithm.Object)).Returns(new Real(3));
            Assert.True(termination.Terminate(mockAlgorithm.Object));

        }
    }
}
