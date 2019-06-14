/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;
using SwiftPSO.Core.ControlParameter;

namespace SwiftPSO.Core.SelfAdaptive.Detection.Swarm
{
    /// <summary>
    /// This detection strategy will periodically return true, where the period is specified as a number of iterations.
    /// </summary>
    public class PeriodicDetection : ISwarmDetection
    {
        public IControlParameter Period { get; set; }

        public PeriodicDetection()
        {
            Period = new ConstantControlParameter(5);
        }

        public bool Detect(PSO algorithm)
        {
            return algorithm.Iteration % Period.Parameter == 0;
        }
    }
}
