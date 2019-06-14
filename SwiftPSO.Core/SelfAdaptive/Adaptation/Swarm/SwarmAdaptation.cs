using SwiftPSO.Core.Algorithm;
/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
namespace SwiftPSO.Core.SelfAdaptive.Adaptation.Swarm
{
    public abstract class SwarmAdaptation
    {
        /// <summary>
        /// Called on the first iteration.
        /// </summary>
        /// <param name="algorithm"></param>
        public virtual void Initialization(PSO algorithm)
        { }

        /// <summary>
        /// Called before the adaptation phase occurs.
        /// </summary>
        /// <param name="algorithm"></param>
        public virtual void PreAdaptation(PSO algorithm)
        { }

        /// <summary>
        /// Perform the adaptation.
        /// </summary>
        /// <param name="algorithm"></param>
        public abstract void Adapt(PSO algorithm);

        /// <summary>
        /// Called after the adaptation occurs.
        /// </summary>
        /// <param name="algorithm"></param>
        public virtual void PostIteration(PSO algorithm)
        { }
    }
}
