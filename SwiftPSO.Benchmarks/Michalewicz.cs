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
    public class Michalewicz : IContinuousFunction
    {
        public int M { get; set; }

        public Michalewicz() : this(10)
        { }

        public Michalewicz(int m)
        {
            M = m;
        }

        public double Evaluate(Vector input)
        {
            double sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                sum += Math.Sin(input[i]) * Math.Pow(Math.Sin(((i + 1) * input[i] * input[i]) / Math.PI), 2 * M); //TODO: CIlib uses Math.sin((i + 1) * x_i*x_i), which seems incorrect
            }

            return -sum;
        }
    }
}