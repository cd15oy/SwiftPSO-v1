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
    public class Ackley : IContinuousFunction
    {
        public double Evaluate(Vector input)
        {
            double sumSquare = 0;
            double sumCos = 0;

            for (int i = 0; i < input.Length; i++)
            {
                sumSquare += input[i] * input[i];
                sumCos += Math.Cos(2 * Math.PI * input[i]);
            }

            return 20 + Math.E - (20 * Math.Exp(-0.2 * Math.Sqrt(sumSquare / input.Length))) - Math.Exp(sumCos / input.Length);
        }
    }
}
