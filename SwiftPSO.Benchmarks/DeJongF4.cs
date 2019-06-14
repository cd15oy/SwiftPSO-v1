/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Random.Distributions;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Benchmarks
{
    public class DeJongF4 : IContinuousFunction
    {
        private GaussianDistribution noise;

        public DeJongF4()
        {
            noise = new GaussianDistribution();
        }

        public double Evaluate(Vector input)
        {
            double sum = 0;

            for (int i = 0; i < input.Length; ++i)
            {
                double xi = input[i];
                sum += (i + 1) * (xi * xi * xi * xi) + noise.Sample();
            }

            return sum;

        }
    }
}
