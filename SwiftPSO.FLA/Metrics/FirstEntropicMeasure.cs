/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SwiftPSO.FLA.Metrics
{
    public class FirstEntropicMeasure
    {
        public double Calculate(IEnumerable<OptimizationSolution> solutions)
        {
            OptimizationSolution[] solutionArray = solutions.ToArray();
            double fem = double.NegativeInfinity;
            double eStar = CalculateEStar(solutionArray);

            for (double e = 0; e < eStar; e += 0.05 * eStar)
            {
                fem = Math.Max(fem, InformationContent(FeatureString(e, solutionArray)));
            }

            return fem;
        }

        /// <summary>
        /// Calculate the minimum pairwise difference in fitness
        /// </summary>
        /// <param name="solutions"></param>
        /// <returns></returns>
        private double CalculateEStar(OptimizationSolution[] solutions)
        {
            double eStar = double.NegativeInfinity;
            for (int i = 1; i < solutions.Length; i++)
            {
                eStar = Math.Max(eStar, Math.Abs(solutions[i].Fitness.Value - solutions[i - 1].Fitness.Value) + double.Epsilon);
            }

            return eStar;
        }

        /// <summary>
        /// Create the feature string based on the current value of e.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="solutions"></param>
        /// <returns></returns>
        private int[] FeatureString(double e, OptimizationSolution[] solutions)
        {
            int[] features = new int[solutions.Length - 1];
            for (int i = 1; i < solutions.Length; i++)
            {
                double a = solutions[i - 1].Fitness.Value;
                double b = solutions[i].Fitness.Value;

                if (b - a < -e) features[i - 1] = -1;
                else if (Math.Abs(b - a) <= e) features[i - 1] = 0;
                else features[i - 1] = 1;
            }

            return features;
        }

        /// <summary>
        /// Calculate the information content based on the feature string.
        /// </summary>
        /// <param name="features"></param>
        /// <returns></returns>
        private double InformationContent(int[] features)
        {
            int[] counts = new int[6];
            for (int i = 1; i < features.Length; i++)
            {
                if (features[i] != features[i - 1])
                {
                    int index = FeatureHash(features[i - 1], features[i]);
                    counts[index]++;
                }
            }

            double entropySum = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                entropySum += Entropy((double)counts[i] / features.Length);
            }

            return -entropySum;
        }

        /// <summary>
        /// Create a unique integer for each feature pair.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        private int FeatureHash(int p, int q)
        {
            int hash = -p + 2 * q;
            if (hash < 0) hash += 3;
            else hash += 2;

            return hash;
        }

        /// <summary>
        /// Calculate the entropy associated with the count of the feature pair.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private double Entropy(double p)
        {
            if (p == 0) return 0;
            else return p * (Math.Log(p) / Math.Log(6));
        }
    }
}
