/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Types;
using System;

namespace SwiftPSO.Core.Problem.Decorator
{
    public class RoundingDecorator : IContinuousFunction
    {
        public IContinuousFunction Function { get; set; }

        public RoundingDecorator(IContinuousFunction function)
        {
            Function = function;
        }

        public double Evaluate(Vector input)
        {
            return Function.Evaluate(input.Map(x =>
            {
                if (Math.Abs(x) < 0.5) return x;
                else return Round(2 * x) / 2.0;
            }));
        }

        private int Round(double f)
        {
            double b = f - (int)f;

            if (f <= 0.0 && b >= 0.5) return (int)f - 1;
            else if (b < 0.5) return (int)f;
            else return (int)f + 1;
        }
    }
}
