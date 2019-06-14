/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.ControlParameter;
using SwiftPSO.Core.Random.Distributions;
using SwiftPSO.Core.Types;
using System;

namespace SwiftPSO.Core.Problem.Decorator
{
    /// <summary>
    /// Adds noise to the result of the provided function. The noise is added as: f(x) + (offset + scale * |N(0,1)|).
    /// </summary>
    public class NoisyDecorator : IContinuousFunction
    {
        public IContinuousFunction Function { get; set; }
        public IControlParameter Scale { get; set; }
        public IControlParameter Offset { get; set; }

        private IDistribution noise;

        public NoisyDecorator()
        {
            noise = new GaussianDistribution();
            Scale = new ConstantControlParameter(0.4);
            Offset = new ConstantControlParameter(1);
        }

        public double Evaluate(Vector input)
        {
            return Function.Evaluate(input) * (Offset.Parameter + Scale.Parameter * Math.Abs(noise.Sample()));
        }
    }
}
