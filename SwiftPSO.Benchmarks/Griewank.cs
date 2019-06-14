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
    public class Griewank : IContinuousFunction
    {
        public double Evaluate(Vector input)
        {
            double sumSquare = 0;
            double prodCos = 1;
            for (int i = 0; i < input.Length; i++)
            {
                sumSquare += input[i] * input[i];
                prodCos *= Math.Cos(input[i] / Math.Sqrt(i + 1)); //use (i+1) since index should start at 1
            }

            return 1 + sumSquare / 4000 - prodCos;
        }
    }
}