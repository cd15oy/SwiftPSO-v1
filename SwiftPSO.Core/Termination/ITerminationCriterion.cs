/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;

namespace SwiftPSO.Core.Termination
{
    public interface ITerminationCriterion
    {
        bool Terminate(IAlgorithm algorithm);

        double CalculateCompletion(IAlgorithm algorithm);
    }
}