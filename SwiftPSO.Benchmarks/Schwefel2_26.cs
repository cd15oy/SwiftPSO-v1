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
    public class Schwefel2_26 : IContinuousFunction
    {
        public double Evaluate(Vector input)
        {
            double sum = 0.0;

            for (int i = 0; i < input.Length; i++)
            {
                sum += input[i] * Math.Sin(Math.Sqrt(Math.Abs(input[i])));
            }
            return -sum;
        }
    }
}
