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
    public class Shubert : IContinuousFunction
    {
        public double Evaluate(Vector input)
        {
            double result = 1;
            for (int i = 0; i < input.Length; i++)
            {
                double temp = 0;
                for (int j = 1; j <= 5; j++)
                {
                    temp += j * Math.Cos((j + 1) * input[i] + j);
                }

                result *= temp;
            }

            return result;
        }
    }
}
