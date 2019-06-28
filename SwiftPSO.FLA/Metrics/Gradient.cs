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
using System.Linq;

namespace SwiftPSO.FLA.Metrics
{
    public class Gradient
    {
        public (double avg, double dev) Calculate(IEnumerable<OptimizationSolution> solutions, double stepSize, Domain problemDomain)
        {
            OptimizationSolution[] solutionArray = solutions.ToArray();
            double[] gradients = new double[solutionArray.Length - 1];

            double fMin = double.PositiveInfinity;
            double fMax = double.NegativeInfinity;

            for (int i = 1; i < solutionArray.Length; i++)
            {
                fMin = Math.Min(fMin, solutionArray[i].Fitness.Value);
                fMax = Math.Max(fMax, solutionArray[i].Fitness.Value);
            }

            double fRange = fMax - fMin;

            double domSum = 0;
            for (int i = 0; i < problemDomain.Dimension; i++)
            {
                domSum += problemDomain[i].Range;
            }

            double gradSum = 0;
            //calculate gradients
            double distTerm = stepSize / domSum;

            for (int i = 1; i < solutionArray.Length; i++)
            {
                OptimizationSolution a = solutionArray[i - 1];
                OptimizationSolution b = solutionArray[i];

                double fitTerm = (b.Fitness.Value - a.Fitness.Value) / fRange;

                gradients[i - 1] = fitTerm / distTerm;

                gradSum += Math.Abs(gradients[i - 1]);
            }

            double avg = gradSum / solutionArray.Length;

            double devSum = 0;
            for (int i = 0; i < gradients.Length; i++)
            {
                devSum += Math.Pow(avg - Math.Abs(gradients[i]), 2);
            }

            double dev = Math.Sqrt(devSum / (solutionArray.Length - 2));

            return (avg, dev);
        }
    }
}
