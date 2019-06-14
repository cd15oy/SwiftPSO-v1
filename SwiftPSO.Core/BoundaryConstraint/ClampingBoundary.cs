/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Particle;
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Core.BoundaryConstraint
{
    /// <inheritdoc />
    /// <summary>
    /// Enforces that particles must stay within the bounds of the search space. If particles leave, they are placed on
    /// the boundary.
    /// </summary>
    public class ClampingBoundary : IBoundaryConstraint
    {
        public void Enforce(IParticle particle, IProblem problem)
        {
            //particle.Position.Map(Clamp); //TODO: verify boundary clamping works correctly

            for (int i = 0; i < particle.Position.Length; i++)
            {
                particle.Position[i] = Clamp(particle.Position[i], problem.ProblemDomain.DimensionBounds[i]);
            }

        }

        private static double Clamp(double component, Bounds bounds) //TODO: need bounds here!
        {
            if (component.CompareTo(bounds.LowerBound) < 0)
            {
                return bounds.LowerBound;
            }

            if (component.CompareTo(bounds.UpperBound) > 0)
            {
                return bounds.UpperBound;
            }

            return component;
        }
    }
}