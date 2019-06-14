/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;
using SwiftPSO.Core.Particle;
using System.Collections.Generic;

namespace SwiftPSO.Core.Iteration
{
    /// <summary>
    /// An asynchronous iteration strategy for PSO.
    /// </summary>
    public class AsynchronousIteration : IIteration<PSO>
    {
        /// <summary>
        /// Each iteration strategy follows the following steps.
        /// 
        /// For all particles:
        /// <list type="number">
        /// <item>Perform iteration.</item>
        /// <item>Calculate fitness.</item>
        /// <item>Update particles in the current particle's neighbourhood.</item>
        /// </list>
        /// 
        /// </summary>
        public void PerformIteration(PSO algorithm)
        {
            //update each particle
            for (int i = 0; i < algorithm.Particles.Count; i++)
            {
                IParticle particle = algorithm.Particles[i];
                particle.PerformIteration(algorithm);
                particle.CalculateFitness(algorithm.Problem);

                List<IParticle> neighbours = algorithm.Topology.GetNeighbours(particle, algorithm.Particles);
                for (int j = 0; j < neighbours.Count; j++)
                {
                    IParticle other = neighbours[j];
                    if (particle.BestFitness.CompareTo(other.NeighbourhoodBest.BestFitness) > 0)
                    {
                        other.NeighbourhoodBest = particle;
                    }
                }
            }
        }
    }
}
