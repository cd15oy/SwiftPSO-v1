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
using System;

namespace SwiftPSO.Benchmarks.Test.Decorator
{
    [TestFixture]
    public class ExpandedDecoratorTest //TODO: this essentially belongs in the core test, but can't be instantiated without a concrete function
    {
        [Test]
        public void Evaluate()
        {
            ExpandedDecorator decorator = new ExpandedDecorator();
            IContinuousFunction function = new Spherical();
            decorator.Function = function;
            decorator.SplitSize = 1;

            Vector v1 = new Vector(new double[] { 0, 0 });
            Vector v2 = new Vector(new double[] { 1, 1 });
            Vector v3 = new Vector(new double[] { 1, 2, 3 });

            Assert.AreEqual(0, decorator.Evaluate(v1));
            Assert.AreEqual(4, decorator.Evaluate(v2));
            Assert.AreEqual(28, decorator.Evaluate(v3));
        }

        [Test]
        public void Error()
        {
            ExpandedDecorator decorator = new ExpandedDecorator();
            IContinuousFunction function = new Spherical();
            decorator.Function = function;
            decorator.SplitSize = 3;

            Vector v1 = new Vector(new double[] { 0, 0 });
            Assert.Throws<ArgumentException>(() => decorator.Evaluate(v1));
        }
    }
}
