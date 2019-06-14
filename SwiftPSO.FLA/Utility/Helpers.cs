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
    }
}
