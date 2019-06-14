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
    public class Iterations : IMeasurement<Integer>
    {
        public Integer GetValue(IAlgorithm algorithm)
        {
            return new Integer(algorithm.Iteration);
        }
    }
}