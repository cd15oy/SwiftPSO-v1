/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Core.Measurement
{
    public class Fitness : IMeasurement<Real>
    {
        public Real GetValue(IAlgorithm algorithm)
        {
            return new Real(algorithm.GetBestSolution().Fitness.Value);
        }
    }
}