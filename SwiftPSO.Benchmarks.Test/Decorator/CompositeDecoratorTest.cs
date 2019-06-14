/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using NUnit.Framework;
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Problem.Decorator;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Benchmarks.Test.Decorator
{
    [TestFixture]
    public class CompositeDecoratorTest //TODO: this essentially belongs in the core test, but can't be instantiated without a concrete function
    {
        [Test]
        public void Evaluate()
        {
            CompositeDecorator decorator = new CompositeDecorator();
            IContinuousFunction function = new Spherical();
            decorator.InnerFunction = function;
            decorator.OuterFunction = function;

            Vector v1 = new Vector(new double[] { 0, 0 });
            Vector v2 = new Vector(new double[] { 1, 1 });
            Vector v3 = new Vector(new double[] { 1, 2, 3 });


            Assert.AreEqual(0, decorator.Evaluate(v1));
            Assert.AreEqual(4, decorator.Evaluate(v2));
            Assert.AreEqual(196, decorator.Evaluate(v3));
        }
    }
}
