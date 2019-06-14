/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using NUnit.Framework;
using SwiftPSO.Core.ControlParameter;
using SwiftPSO.Core.Problem.Decorator;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Benchmarks.Test.Decorator
{
    [TestFixture]
    public class ShiftDecoratorTest //TODO: this essentially belongs in the core test, but can't be instantiated without a concrete function
    {
        [Test]
        public void Evaluate()
        {
            ShiftDecorator decorator = new ShiftDecorator(new Spherical());
            Vector v1 = new Vector(new double[] { 0, 0 });
            Vector v2 = new Vector(new double[] { 5, 5 });

            decorator.HorizontalShift = new ConstantControlParameter(0);
            decorator.VerticalShift = new ConstantControlParameter(1);
            Assert.AreEqual(1, decorator.Evaluate(v1));

            decorator.HorizontalShift = new ConstantControlParameter(5);
            Assert.AreEqual(1, decorator.Evaluate(v2));

            decorator.VerticalShift = new ConstantControlParameter(-1);
            Assert.AreEqual(-1, decorator.Evaluate(v2));
        }
    }
}
