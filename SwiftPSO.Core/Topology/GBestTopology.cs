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
    public class GBestTopology : ITopology
    {
        public List<IParticle> GetNeighbours(IParticle particle, List<IParticle> swarm)
        {
            return swarm;
        }
    }
}