/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Fitness;
using System;
using System.Collections.Generic;
using SwiftPSO.FLA.Utility;
using System.Linq;

namespace SwiftPSO.FLA.Metrics
{
    public class FitnessDistribution
    {   
       
        public (double,double) Calculate(IEnumerable<OptimizationSolution> solutions)
        {
            OptimizationSolution[] solutionArray = solutions.ToArray();
            
            double skew = 0.0;
            double kurtosis = 0.0; 

            IFitness AveFit = Helpers.AverageFitness(solutionArray, 0, solutionArray.Length);

            double skewNumerator = 0.0;
            double skewDenominator = 0.0;
            double kurtosisNumerator = 0.0;
            double kurtosisDenominator = 0.0;

            foreach(OptimizationSolution s in solutions) {
                double difference = s.Fitness.Value - AveFit.Value;
                double exponentialDiff = difference*difference;

                skewDenominator += exponentialDiff;
                kurtosisDenominator += exponentialDiff;

                exponentialDiff *= difference;
                skewNumerator += exponentialDiff;

                exponentialDiff *= difference;
                kurtosisNumerator += exponentialDiff;
            }
           
            skewNumerator /= solutionArray.Length;
            skewDenominator /= (solutionArray.Length-1.0);
            skewDenominator = Math.Pow(skewDenominator, 3.0/2.0);
            skew = skewNumerator/skewDenominator;

            kurtosisNumerator /= solutionArray.Length;
            kurtosisDenominator = Math.Pow(kurtosisDenominator/solutionArray.Length, 2);
            kurtosis = (kurtosisNumerator/kurtosisDenominator) - 3;

            return (skew, kurtosis);
        }

        

        /// <summary>
        /// Determine if a solution is in a neutral section 
        /// </summary>
        /// <param name="threshold"> A double represeting the threshold for neutrality.</param>
        /// <param name="solutions"> An array of optimization solutions with normalized fitness.</param>
        /// <param name="index"> The index of the solution to test.!-- --></param>
        /// <returns>True if neural, false otherwise.</returns>
        private bool Neutral(OptimizationSolution[] solutions, int index, double threshold)
        {
            if(index < 0 || index >= solutions.Length)
                throw new ArgumentException("The solution to test must have both left and right neighbors.");

            double mx = solutions[index].Fitness.Value;
            double mn = solutions[index].Fitness.Value;
            for(int i = -1; i < 2; i++) {
                mx = (mx > solutions[index+i].Fitness.Value)? mx : solutions[index+i].Fitness.Value;
                mn = (mn < solutions[index+i].Fitness.Value)? mn : solutions[index+i].Fitness.Value;
            }

            if(mx - mn < threshold) 
                return true;
            else 
                return false;
        }
    }
}
