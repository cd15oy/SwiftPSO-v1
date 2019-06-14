/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;

namespace SwiftPSO.Core.Particle.PositionInitialization
{
    public class RandomPositionInitialization : IPositionInitialization
    {
        public void Initialize(IParticle particle, IProblem problem)
        {
            particle.Position.Randomize(problem.ProblemDomain.DimensionBounds);
        }
    }
}