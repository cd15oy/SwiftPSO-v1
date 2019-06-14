/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Types;
using System;
using System.Collections.Generic;

namespace SwiftPSO.Benchmarks.CEC2005
{
    public class HybridComposition : IContinuousFunction
    {
        public double ScaleConstant { get; set; }
        private List<CEC2005Function> _functions;

        public HybridComposition()
        {
            _functions = new List<CEC2005Function>();
            ScaleConstant = 2000;
        }

        public double Evaluate(Vector input)
        {
            int dims = input.Length;

            // Get the raw weights
            double wMax = double.NegativeInfinity;
            double wSum = 0.0;
            foreach (CEC2005Function f in _functions)
            {
                f.Shift(input);
                double sumSqr = Math.Pow(f.Shifted.Norm(), 2);

                f.Weight = Math.Exp(-1.0 * sumSqr / (2.0 * dims * f.Sigma * f.Sigma));

                if (wMax < f.Weight)
                    wMax = f.Weight;

                wSum += f.Weight;
            }

            // Modify the weights
            double w1mMaxPow = 1.0 - Math.Pow(wMax, 10.0);
            foreach (CEC2005Function f in _functions)
            {
                if (f.Weight != wMax)
                {
                    f.Weight = f.Weight * w1mMaxPow;
                }

                f.Weight = f.Weight / wSum;
            }

            double sumF = 0.0;
            foreach (CEC2005Function f in _functions)
            {
                sumF += f.Weight * (ScaleConstant * f.Evaluate(input) + f.Bias);
            }

            return sumF;
        }

        public void AddFunction(CEC2005Function function)
        {
            _functions.Add(function);
        }
    }
}
