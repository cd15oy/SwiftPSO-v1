/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using NUnit.Framework;
using SwiftPSO.Core.ControlParameter;

namespace SwiftPSO.Core.Test.ControlParameter
{
    [TestFixture]
    public class ConstantControlParameterTest
    {
        [Test]
        public void Parameter()
        {
            const double value = 3.4;
            ConstantControlParameter parameter = new ConstantControlParameter(value);
            Assert.AreEqual(value, parameter.Parameter);

            const double newValue = -1.2;
            parameter.Parameter = newValue;
            Assert.AreEqual(newValue, parameter.Parameter);
        }
    }
}