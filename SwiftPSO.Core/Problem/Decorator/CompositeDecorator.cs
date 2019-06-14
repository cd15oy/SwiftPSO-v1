/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Types;

namespace SwiftPSO.Core.Problem.Decorator
{
    public class CompositeDecorator : IContinuousFunction
    {
        public IContinuousFunction InnerFunction { get; set; }
        public IContinuousFunction OuterFunction { get; set; }

        public CompositeDecorator()
        { }

        public CompositeDecorator(IContinuousFunction innerFunction, IContinuousFunction outerFunction)
        {
            InnerFunction = innerFunction;
            OuterFunction = outerFunction;
        }

        public double Evaluate(Vector input)
        {
            Vector innerVector = new Vector(1);
            innerVector[0] = InnerFunction.Evaluate(input);
            return OuterFunction.Evaluate(innerVector);
        }
    }
}
