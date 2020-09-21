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
    public class Stag
    {   
       
        public (double,double) Calculate(IEnumerable<OptimizationSolution> solutions)
        {
            OptimizationSolution[] solutionArray = solutions.ToArray();
            OptimizationSolution[] normalizedFit = Helpers.NormalizeFitness(solutionArray);

            double lengthOfStagnatedRegions = 0;
            double numberOfStagnatedRegions = 0;

            //Now we test a selection of different sizes for the moving average and standard deviation
            for(int i = 6; i < 21; i += 2) {
                //Smooth the sequence of fitness values 
                OptimizationSolution[] weightedAve = Helpers.EWMAFitness(normalizedFit, 2.0/(i+1.0));

                IFitness ave = Helpers.AverageFitness(weightedAve, 0, weightedAve.Length);
                IFitness sd = Helpers.StandardDeviationFitness(weightedAve,ave, 0, weightedAve.Length);
                
                //Get a moving standard deviation
                IFitness[] movingSD = new IFitness[weightedAve.Length-(i-1)];
                for(int j = i; j < weightedAve.Length; j++)
                    movingSD[j-i] = Helpers.StandardDeviationFitness(weightedAve, ave, j-i, j);

                //Now we want to calculate the proportion of stagnated regions, and the proportion of the lengths of the stagnated regions for this beta value 
                //We eventually choose the beta which maximize these values, as a reasonable conservative choice. 
                //This process was outlined by Bossman and Engelbrecht 

                double lStag = 0;
                double nStag = 0;
                bool stuck = false;
                double sumRegionLength = 0;
                double numRegions = 0;
                double len = 0;

                for(int j = 0; j < len-(i-1); j++) {
                    if(stuck) {
                        if(movingSD[j].Value < sd.Value) {
                            len += 1.0;
                        } else {
                            stuck = false;
                            sumRegionLength += len;
                            len = 0.0;
                        }
                    } else {
                        if(movingSD[j].Value < sd.Value) {
                            numRegions += 1.0;
                            stuck = true;
                            len += 1.0;
                        }
                    }
                }
                if(len > 0)
                    sumRegionLength += len;

                lStag = sumRegionLength/numRegions;
                nStag = numRegions;
                if(lStag > lengthOfStagnatedRegions) {
                    lengthOfStagnatedRegions = lStag;
                    numberOfStagnatedRegions = nStag;
                }
            }
            
            return (lengthOfStagnatedRegions,numberOfStagnatedRegions);
           
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
