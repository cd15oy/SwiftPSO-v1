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
    public class Schwefel2_22 : IContinuousFunction
    {
        public double Evaluate(Vector input)
        {
            double sum = Math.Abs(input[0]); //this avoids needing an if statement in the loop
            double product = sum;

            for (int i = 1; i < input.Length; i++) //index from 1 since we have the first
            {
                sum += Math.Abs(input[i]);
                product *= Math.Abs(input[i]);
            }

            return sum + product;
        }
    }
}
