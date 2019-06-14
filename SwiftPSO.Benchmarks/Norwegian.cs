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
    public class Norwegian : IContinuousFunction
    {
        public double Evaluate(Vector input)
        {
            double product = 1;
            for (int i = 0; i < input.Length; i++)
            {
                product *= Math.Cos(Math.PI * Math.Pow(input[i], 3)) * ((99 + input[i]) / 100);
            }

            return product;
        }
    }
}