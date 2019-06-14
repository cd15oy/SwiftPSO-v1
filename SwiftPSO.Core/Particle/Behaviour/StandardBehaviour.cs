/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.BoundaryConstraint;
using SwiftPSO.Core.Particle.PositionProvider;
using SwiftPSO.Core.Particle.VelocityProvider;
using SwiftPSO.Core.Problem;

namespace SwiftPSO.Core.Particle.Behaviour
{
    public class StandardBehaviour : IBehaviour
    {
        public IBoundaryConstraint BoundaryConstraint { get; set; }
        public IVelocityProvider VelocityProvider { get; set; }
        public IPositionProvider PositionProvider { get; set; }

        public StandardBehaviour()
        {
            BoundaryConstraint = new UnconstrainedBoundary();
            VelocityProvider = new StandardVelocityProvider();
            PositionProvider = new StandardPositionProvider();
        }

        public void PerformIteration(IParticle particle, IProblem problem)
        {
            VelocityProvider.UpdateVelocity(particle);
            particle.UpdatePreviousPosition();
            PositionProvider.UpdatePosition(particle);
            BoundaryConstraint.Enforce(particle, problem);
        }

    }
}