/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Types;
using SwiftPSO.Core.Utility.Distance;
using SwiftPSO.FLA.Utility;
using System.Collections.Generic;
using System.Linq;

namespace SwiftPSO.FLA.Metrics
{
    public class DispersionMetric
    {
        private EuclideanDistance distanceMeasure = new EuclideanDistance();

        public double Calculate(IEnumerable<OptimizationSolution> solutions, Bounds[] bounds, double sampleSize = 0.1)
        {
            OptimizationSolution[] sorted = solutions.OrderByDescending(x => x.Fitness).Select(x => Helpers.Normalize(x, bounds)).ToArray();

            int numSamples = sorted.Length;
            int subsetSize = (int)(sampleSize * numSamples);

            //take subset of best entities
            OptimizationSolution[] subset = sorted.Take(subsetSize).ToArray();

            double totalDispersion = 0;
            double subsetDispersion = 0;

            double distance = 0;

            //calculate dispersion of all points (use size - 1 to account for comparison with self)
            //must use sorted as it has been normalized!
            foreach (var sample in sorted)
            {
                distance += sorted.Sum(x => distanceMeasure.Measure(sample.Position, x.Position)) / (numSamples - 1);
            }

            totalDispersion = distance / numSamples;

            distance = 0; //reset total distance

            //calculate dispersion of best points (use size - 1 to account for comparison with self)
            foreach (var sample in subset)
            {
                distance += subset.Sum(x => distanceMeasure.Measure(sample.Position, x.Position)) / (subsetSize - 1);
            }

            subsetDispersion = distance / subsetSize;

            return subsetDispersion - totalDispersion;//(Math.Sqrt(3.0 * 30) / 4.0 - 0.1);//
        }
    }
}
