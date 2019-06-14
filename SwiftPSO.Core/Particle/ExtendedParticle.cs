/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Particle.Behaviour;
using SwiftPSO.Core.Particle.VelocityProvider;

namespace SwiftPSO.Core.Particle
{
    public class ExtendedParticle : AbstractParticle
    {
        public ExtendedParameterSet Parameters { get; set; }

        public ExtendedParticle() : base()
        {
            StandardBehaviour behaviour = new StandardBehaviour();
            behaviour.VelocityProvider = new ExtendedVelocityProvider();
            Behaviour = behaviour;
        }
    }
}
