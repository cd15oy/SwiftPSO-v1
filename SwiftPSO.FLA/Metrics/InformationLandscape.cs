/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;
using SwiftPSO.FLA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SwiftPSO.FLA.Metrics
{
    public class InformationLandscape
    {
        public double Calculate(IEnumerable<OptimizationSolution> solutions, IObjective objective)
        {
            OptimizationSolution[] samples = solutions.ToArray();
            OptimizationSolution best = Helpers.GetBest(samples);
            OptimizationSolution[] reference = solutions.Select(x => new OptimizationSolution(x.Position, objective.Evaluate(SphericalReference(x, best)))).ToArray();

            List<double> ilVec = ILVector(samples, best); //information landscape of problem
            List<double> ilVecRef = ILVector(reference, best); //information landscape of reference

            return AbsoluteDistance(ilVec, ilVecRef);
        }

        public List<double> ILVector(OptimizationSolution[] samples, OptimizationSolution best)
        {
            var length = (samples.Length - 1) * (samples.Length - 2) / 2;
            List<double> result = new List<double>(length);

            for (int i = 0; i < samples.Length; i++)
            {
                if (samples[i].Position.Equals(best.Position)) continue; //skip the best parameter set

                for (int j = i + 1; j < samples.Length; j++) //only consider solutions in upper half (due to symmetry)
                {
                    if (samples[j].Position.Equals(best.Position)) continue; //skip the best parameter set
                    if (samples[i].Fitness.CompareTo(samples[j].Fitness) > 0) result.Add(1);
                    else if (samples[i].Fitness.CompareTo(samples[j].Fitness) < 0) result.Add(0);
                    else result.Add(0.5);
                }
            }
            //Console.WriteLine(length);
            //Console.WriteLine(result.Count);
            //Console.WriteLine("---------");
            return result;
        }

        public double AbsoluteDistance(List<double> landscape, List<double> reference)
        {
            double distance = 0;
            for (int i = 0; i < landscape.Count; i++)
            {
                distance += Math.Abs(landscape[i] - reference[i]);
            }

            return distance / landscape.Count;
        }

        public double SphericalReference(OptimizationSolution solution, OptimizationSolution best)
        {
            double sum = 0;
            for (int i = 0; i < solution.Position.Length; i++)
            {
                double d = solution.Position[i] - best.Position[i];
                sum += d * d;
            }

            return sum;
        }
    }
}
