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
    public class Rana : IContinuousFunction
    {
        public double Evaluate(Vector input)
        {
            double sum = 0.0;
            double a = 0.0;
            double b = 0.0;

            for (int i = 0; i < input.Length - 1; i++)
            {
                a = Math.Sqrt(Math.Abs(input[i + 1] + 1 - input[i]));
                b = Math.Sqrt(Math.Abs(input[i + 1] + 1 + input[i]));

                sum += (input[i] * Math.Sin(a) * Math.Cos(b) + (input[i + 1] + 1) * Math.Cos(a) * Math.Sin(b));
            }
            return sum;
        }
    }
}
