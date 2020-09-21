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
    public class Elliptic : IContinuousFunction
    {
        public double ConditionNumber { get; set; }

        public Elliptic() : this(1e6)
        { }

        public Elliptic(double conditionNumber)
        {
            ConditionNumber = conditionNumber;
        }

        public double Evaluate(Vector input)
        {
            double sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                sum += Math.Pow(ConditionNumber, i / (input.Length - 1.0)) * input[i] * input[i];
            }

            return sum;
        }
    }
}