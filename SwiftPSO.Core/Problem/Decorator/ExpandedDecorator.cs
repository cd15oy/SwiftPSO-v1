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
    public class ExpandedDecorator : IContinuousFunction
    {
        public IContinuousFunction Function { get; set; }
        public int SplitSize { get; set; }

        public ExpandedDecorator() : this(null, 1)
        { }

        public ExpandedDecorator(IContinuousFunction function) : this(function, 1)
        { }

        public ExpandedDecorator(IContinuousFunction function, int splitSize)
        {
            Function = function;
            SplitSize = SplitSize;
        }

        public double Evaluate(Vector input)
        {
            if (SplitSize > input.Length) throw new ArgumentException("SplitSize must be smaller than the input.");

            double sum = 0;

            for (int i = SplitSize; i < input.Length; i++)
            {
                sum += Function.Evaluate(input.Slice(i - SplitSize, i + SplitSize));
            }

            sum += Function.Evaluate(FinalVector(input));

            return sum;
        }

        Vector FinalVector(Vector input)
        {
            double[] final = new double[SplitSize + 1];
            for (int i = 0; i < final.Length - 1; i++)
            {
                final[i] = input[input.Length - SplitSize + i];
            }

            final[final.Length - 1] = input[0];

            return new Vector(final);
        }
    }
}
