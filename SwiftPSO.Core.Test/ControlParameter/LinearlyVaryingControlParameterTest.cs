/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using NUnit.Framework;

namespace SwiftPSO.Core.Test.ControlParameter
{
    [TestFixture]
    public class LinearlyVaryingControlParameterTest
    {
        [Test]
        public void Parameter()
        {
            //can't mock instance on abstractPSO or set calculate completion
            //instance is null on mocking IAlgorithm..

            //Mock<IAlgorithm> mockAlgorithm = new Mock<IAlgorithm>();
            //mockAlgorithm.Setup(x => x.CalculateCompletion()).Returns(0);

            //AbstractPSO.CurrentInstance = mockAlgorithm.Object;
            //mockAlgorithm.Setup(x => AbstractPSO.CurrentInstance).Returns(mockAlgorithm.Object);

            //Mock<ITerminationCriterion> mockTermination = new Mock<ITerminationCriterion>();
            //mockTermination.Setup(x => x.CalculateCompletion(mockAlgorithm.Object)).Returns(0);

            //mockAlgorithm.Setup(x => x.TerminationCriterion).Returns(mockTermination.Object);

            //mockAlgorithm.Setup(x => AbstractPSO.CurrentInstance.CalculateCompletion()).Returns(0);

            //LinearlyVaryingControlParameter parameter = new LinearlyVaryingControlParameter(0.9, 0.4);

            //Assert.AreEqual(parameter.Parameter, 0.9);
        }
    }
}
