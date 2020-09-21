/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Fitness;
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Types;
using System;
using System.Collections.Generic;

namespace SwiftPSO.FLA.Utility
{
    static class Helpers
    {
        public static OptimizationSolution GetBest(IEnumerable<OptimizationSolution> solutions)
        {
            OptimizationSolution best = new OptimizationSolution(null, InferiorFitness.GetInstance());

            foreach (OptimizationSolution solution in solutions)
            {
                if (solution.CompareTo(best) > 0) best = solution;
            }

            return best;
        }

        /// <summary>
        /// Construct a new <see cref="OptimizationSolution"/> as a normalized instance of the provided <see cref="OptimizationSolution"/>.
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public static OptimizationSolution Normalize(OptimizationSolution solution, Bounds[] bounds)
        {
            if (solution.Position.Length != bounds.Length) throw new ArgumentException("Position and Bound lengths do not match.");
            Vector normPos = solution.Position.Clone();
            for (int i = 0; i < normPos.Length; i++)
            {
                normPos[i] = (normPos[i] - bounds[i].LowerBound) / bounds[i].Range;
            }

            return new OptimizationSolution(normPos, solution.Fitness);
        }

        /// <summary>
        /// Normalizes the fitnesses of an array of <see cref="OptimizationSolution"/> values />
        /// </summary>
        /// <param name="solution"></param>
        /// <returns></returns>
        public static OptimizationSolution[] NormalizeFitness(OptimizationSolution[] solutions)
        {
            OptimizationSolution[] ret = new OptimizationSolution[solutions.Length];
            double mx = solutions[0].Fitness.Value;
            double mn = solutions[0].Fitness.Value;
            foreach(OptimizationSolution x in solutions) {
                if(x.Fitness.Value > mx) mx = x.Fitness.Value;
                if(x.Fitness.Value < mn) mn = x.Fitness.Value;
            }
            
            for(int i = 0; i < solutions.Length; i++) {
                ret[i] = new OptimizationSolution(solutions[i].Position, solutions[i].Fitness.GetNew((solutions[i].Fitness.Value - mn)/(mx - mn)));
            }

            return ret;
        }

        /// <summary>
        /// Converts the fitness of an array of <see cref="OptimizationSolution"/> to the exponential weighted moving average. />
        /// </summary>
        /// <param name="solution">An array of  <see cref="OptimizationSolution"/>.</param>
        /// <param name="beta">A parameter (in [0.0,1.0]) which controls the relative weight of the next solution.</param>
        /// <returns></returns>
        public static OptimizationSolution[] EWMAFitness(OptimizationSolution[] solutions, double beta)
        {
            OptimizationSolution[] ret = new OptimizationSolution[solutions.Length];
            
            ret[0] = solutions[0];
            for(int i = 1; i < ret.Length; i++) {
                double oldFit = solutions[i].Fitness.Value;
                double newFit = (beta*oldFit) + ((1.0-beta)*solutions[i-1].Fitness.Value);
                ret[i] = new OptimizationSolution(solutions[i].Position, solutions[i].Fitness.GetNew(newFit));
            }
            
            return ret;
        }

        /// <summary>
        /// Get the average fitness of adjacent <see cref="OptimizationSolution"/>s in an array./>
        /// </summary>
        /// <param name="solution">An array of  <see cref="OptimizationSolution"/>.</param>
        /// <param name="start">Index of first <see cref="OptimizationSolution"/> to use. (Inclusive)</param>
        /// <param name="end">Index of the end of the section over which the calculate the average.(Exclusive)</param>
        /// <returns></returns>
        public static IFitness AverageFitness(OptimizationSolution[] solutions, int start, int end)
        {
            if (start < 0 || start >= solutions.Length) throw new ArgumentException("start must fall within solutions.");
            if (end < 0 || end > solutions.Length) throw new ArgumentException("end must fall within solutions.");
            if (end < start) throw new ArgumentException("start must be less than end");

            double sum = 0;

            for(int i = start; i < end; i++)
                sum += solutions[i].Fitness.Value; 

            return solutions[0].Fitness.GetNew(sum/(end - start + 0.0));
        }

        /// <summary>
        /// Get the standard deviation of fitness of adjacent <see cref="OptimizationSolution"/>s in an array./>
        /// </summary>
        /// <param name="solution">An array of  <see cref="OptimizationSolution"/>.</param>
        /// <param name="start">Index of first <see cref="OptimizationSolution"/> to use. (Inclusive)</param>
        /// <param name="end">Index of the end of the section over which the calculate the standard deviation.(Exclusive)</param>
        /// <returns></returns>
        public static IFitness StandardDeviationFitness(OptimizationSolution[] solutions, IFitness Average, int start, int end)
        {
            if (start < 0 || start > solutions.Length) throw new ArgumentException("start must fall within solutions.");
            if (end < 0 || end > solutions.Length) throw new ArgumentException("end must fall within solutions.");
            if (end < start) throw new ArgumentException("start must be less than end");

            double ave = Average.Value;
            double sum = 0;

            for(int i = start; i < end; i++)
                sum += Math.Pow(solutions[i].Fitness.Value-ave, 2); 

            return solutions[0].Fitness.GetNew(Math.Sqrt(sum/(end - start - 1.0))); //-1 for sample STDDEV 
        }
        
    }
}
