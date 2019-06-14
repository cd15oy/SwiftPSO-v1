/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.BoundaryConstraint;
using SwiftPSO.Core.Problem;

namespace SwiftPSO.Core.Particle.Behaviour
{
    public interface IBehaviour
    {
        IBoundaryConstraint BoundaryConstraint { get; set; }

        void PerformIteration(IParticle particle, IProblem problem);

    }
}