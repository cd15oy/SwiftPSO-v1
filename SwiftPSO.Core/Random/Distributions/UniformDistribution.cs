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
    public class UniformDistribution : IDistribution
    {
        public IControlParameter LowerBound { get; set; }
        public IControlParameter UpperBound { get; set; }

        public UniformDistribution() : this(new ConstantControlParameter(0), new ConstantControlParameter(1))
        { }

        public UniformDistribution(IControlParameter lowerBound, IControlParameter upperBound)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }

        public double Sample()
        {
            if (LowerBound.Parameter > UpperBound.Parameter) throw new ArgumentException("The lower bound must be less than the upper bound");

            double rand = RandomProvider.NextDouble();
            return (UpperBound.Parameter - LowerBound.Parameter) * rand + LowerBound.Parameter;
        }
    }
}
