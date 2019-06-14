/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Core.Measurement
{
    /// <summary>
    /// Calculate the percentage of particles with a bound violation in at least one dimension.
    /// </summary>
    public class BoundViolations : IMeasurement<Real>
    {
        public Real GetValue(IAlgorithm algorithm)
        {
            SinglePopulationPSO pso = algorithm as SinglePopulationPSO;

            int violations = 0;

            for (int i = 0; i < pso.Particles.Count; i++)
            {
                if (!pso.Particles[i].Position.IsInsideBounds(algorithm.Problem.ProblemDomain.DimensionBounds)) violations++;
            }

            return new Real((double)violations / pso.Particles.Count);
        }
    }
}