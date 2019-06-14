/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Particle;
using SwiftPSO.Core.Topology;
using System.Collections.Generic;

namespace SwiftPSO.Core.Algorithm
{
    public abstract class SinglePopulationPSO : AbstractAlgorithm
    {
        public List<IParticle> Particles { get; set; }
        public ITopology Topology { get; set; }
        public int SwarmSize { get; set; }

        public SinglePopulationPSO() : base()
        {
            Topology = new GBestTopology();
            SwarmSize = 30;
        }
    }
}
