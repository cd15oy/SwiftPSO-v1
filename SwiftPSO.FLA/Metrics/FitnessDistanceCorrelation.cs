/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Utility.Distance;
using SwiftPSO.FLA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SwiftPSO.FLA.Metrics
{
    public class FitnessDistanceCorrelation
    {
        private EuclideanDistance distanceMeasure = new EuclideanDistance();

        public double Calculate(IEnumerable<OptimizationSolution> solutions)
        {
            OptimizationSolution[] samples = solutions.ToArray();
            int numSamples = samples.Length;

            OptimizationSolution best = Helpers.GetBest(samples);

            List<double> fitnesses = new List<double>(numSamples);
            List<double> distances = new List<double>(numSamples);

            double fitnessSum = 0;
            double distanceSum = 0;

            foreach (var sample in samples)
            {
                fitnessSum += sample.Fitness.Value;
                fitnesses.Add(sample.Fitness.Value);

                double distance = distanceMeasure.Measure(sample.Position, best.Position);
                distanceSum += distance;
                distances.Add(distance);
            }

            double avgFitness = fitnessSum / numSamples;
            double avgDistance = distanceSum / numSamples;

            double numerator = 0;
            double lDenominator = 0;
            double rDenominator = 0;

            //calculate the sums used in the final calculation
            for (int i = 0; i < numSamples; i++)
            {
                double fitnessDiff = fitnesses[i] - avgFitness;
                double distanceDiff = distances[i] - avgDistance;
                numerator += fitnessDiff * distanceDiff;

                lDenominator += fitnessDiff * fitnessDiff;
                rDenominator += distanceDiff * distanceDiff;
            }

            return numerator / (Math.Sqrt(lDenominator) * Math.Sqrt(rDenominator));
        }
    }
}
