/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;

namespace SwiftPSO.Core.SelfAdaptive.Adaptation.Swarm
{
    /// <summary>
    /// An adaptation strategy that does nothing.
    /// </summary>
    public class DoNothingAdaptation : SwarmAdaptation
    {
        public override void Adapt(PSO algorithm)
        {
            //do nothing!
        }
    }
}
