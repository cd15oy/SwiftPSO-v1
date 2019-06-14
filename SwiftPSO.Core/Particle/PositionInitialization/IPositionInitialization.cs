/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;

namespace SwiftPSO.Core.Particle.PositionInitialization
{
    public interface IPositionInitialization
    {
        /// <summary>
        /// Initializes the position of the supplied particle.
        /// </summary>
        /// <param name="particle"></param>
        /// <param name="problem"></param>
        void Initialize(IParticle particle, IProblem problem);
    }
}