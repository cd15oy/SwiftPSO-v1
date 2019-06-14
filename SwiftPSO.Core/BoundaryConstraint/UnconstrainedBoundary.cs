/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Particle;
using SwiftPSO.Core.Problem;

namespace SwiftPSO.Core.BoundaryConstraint
{
    public class UnconstrainedBoundary : IBoundaryConstraint
    {
        public void Enforce(IParticle particle, IProblem problem)
        {
            //Do nothing.
        }
    }
}