/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Particle;
using SwiftPSO.Core.Topology;
using SwiftPSO.Core.Types;
using System.Collections.Generic;

namespace SwiftPSO.Core.Utility.Center
{
    /// <summary>
    /// Calculate the center as the global best position found thus far.
    /// </summary>
    public class GBestCenter : ICenter
    {
        public Vector GetCenter(List<IParticle> particles)
        {
            return TopologyHelper.GetBestParticle(particles).BestPosition; //TODO: should this be best position or position?
        }
    }
}