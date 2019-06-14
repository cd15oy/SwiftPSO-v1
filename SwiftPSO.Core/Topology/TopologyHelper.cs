/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Particle;
using System.Collections.Generic;

namespace SwiftPSO.Core.Topology
{
    public static class TopologyHelper
    {
        public static IParticle GetNeighbourhoodBest(List<IParticle> swarm, IParticle particle, ITopology topology)
        {
            return swarm.Count == 0 ? null : GetBestParticle(topology.GetNeighbours(particle, swarm));
        }

        /// <summary>
        /// Return the particle with the best value for BestFitness.
        /// </summary>
        /// <remarks>This is not the current best particle. </remarks>
        /// <param name="particles"></param>
        /// <returns></returns>
        public static IParticle GetBestParticle(List<IParticle> particles)
        {
            //cycle through neighbours, keeping track of the best yet
            if (particles.Count == 0) return null;
            IParticle best = particles[0];
            for (int i = 1; i < particles.Count; i++)
            {
                IParticle neighbour = particles[i];
                if (neighbour.BestFitness.CompareTo(best.BestFitness) > 0) best = neighbour;
            }

            return best;
        }

        /// <summary>
        /// Return the particle with the best value for Fitness.
        /// </summary>
        /// <remarks>This is the particle with the best current fitness. </remarks>
        /// <param name="particles"></param>
        /// <returns></returns>
        public static IParticle GetCurrentBestParticle(List<IParticle> particles)
        {
            //cycle through neighbours, keeping track of the best yet
            if (particles.Count == 0) return null;
            IParticle best = particles[0];
            for (int i = 1; i < particles.Count; i++)
            {
                IParticle neighbour = particles[i];
                if (neighbour.Fitness.CompareTo(best.Fitness) > 0) best = neighbour;
            }

            return best;
        }

    }
}