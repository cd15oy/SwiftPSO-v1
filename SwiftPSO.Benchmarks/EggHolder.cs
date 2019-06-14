/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Types;
using System;

namespace SwiftPSO.Benchmarks
{
    public class EggHolder : IContinuousFunction
    {
        public double Evaluate(Vector input)
        {
            double sum = 0;
            for (int i = 0; i < input.Length - 1; i++)
            {
                sum += -(input[i + 1] + 47) * Math.Sin(Math.Sqrt(Math.Abs(input[i + 1] + input[i] / 2 + 47))) - input[i] * Math.Sin(Math.Sqrt(Math.Abs(input[i] - (input[i + 1] + 47))));
            }

            return sum;
        }
    }
}
