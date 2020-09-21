/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;
using System;
using System.Collections.Generic;
using SwiftPSO.FLA.Utility;
using System.Linq;

namespace SwiftPSO.FLA.Metrics
{
    public class MMeasure
    {   
        double Threshold{get;}

        public MMeasure(double t) {
            Threshold = t;
        }
        public MMeasure() {
            Threshold = 0.00000001; //This value taken from Malan and Engelbrecht
        }
        public (double,double) Calculate(IEnumerable<OptimizationSolution> solutions)
        {
            OptimizationSolution[] solutionArray = solutions.ToArray();
            OptimizationSolution[] normalizedFit = Helpers.NormalizeFitness(solutionArray);

            int totalNeutralSections = 0;
            int longestNeutralSequence = 0;
            int lengthOfCurrentSection = 0;

            for(int i = 1; i < solutionArray.Length-1; i++) {
                if(Neutral(solutionArray, i, Threshold)) {
                    totalNeutralSections++;
                    lengthOfCurrentSection++;
                } else {
                    if(lengthOfCurrentSection > longestNeutralSequence) longestNeutralSequence = lengthOfCurrentSection;
                    lengthOfCurrentSection = 0;
                }
            }

            double ProportionOfNeutralSections = totalNeutralSections/(solutionArray.Length + 0.0);
            double ProportionOfLongestSection = longestNeutralSequence/(solutionArray.Length + 0.0);

            return (ProportionOfNeutralSections, ProportionOfLongestSection);
           
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
