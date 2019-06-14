/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.ControlParameter;
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Random.Distributions;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Benchmarks
{
    public class UniformRandomFunction : IContinuousFunction
    {
        public UniformDistribution FitnessDistribution { get; set; }

        public UniformRandomFunction()
        {
            FitnessDistribution = new UniformDistribution(new ConstantControlParameter(0), new ConstantControlParameter(1));
        }

        public double Evaluate(Vector input)
        {
            return FitnessDistribution.Sample();
        }
    }
}
