/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.ControlParameter;
using System;

namespace SwiftPSO.Core.Random.Distributions
{

    public class GaussianDistribution : IDistribution
    {
        public IControlParameter Mean { get; set; }
        public IControlParameter StandardDeviation { get; set; }

        public GaussianDistribution() : this(new ConstantControlParameter(0), new ConstantControlParameter(1))
        { }

        public GaussianDistribution(IControlParameter mean, IControlParameter standardDeviation)
        {
            Mean = mean;
            StandardDeviation = standardDeviation;
        }

        /// <summary>
        /// Generate a number from a Gaussian distribution using the Box-Muller transform.
        /// </summary>
        public double Sample()
        {
            //TODO: consider Ziggurat algorithm for faster Gaussian sampling
            double u1 = 1 - RandomProvider.NextDouble();
            double u2 = 1 - RandomProvider.NextDouble();
            double normal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

            return StandardDeviation.Parameter * normal + Mean.Parameter;
        }
    }
}
