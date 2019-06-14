/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;

namespace SwiftPSO.Core.Particle.PersonalBestUpdate
{
    /// <summary>
    /// Only update the personal best if it is within the bounds of the search space
    /// </summary>
    public class BoundedPersonalBestUpdate : IPersonalBestUpdate
    {
        public IPersonalBestUpdate Delegate { get; set; }

        public BoundedPersonalBestUpdate()
        {
            Delegate = new StandardPersonalBestUpdate();
        }

        public void UpdatePersonalBest(IParticle particle, IProblem problem)
        {
            if (particle.Position.IsInsideBounds(problem.ProblemDomain.DimensionBounds))
            {
                Delegate.UpdatePersonalBest(particle, problem);
            }
        }
    }
}