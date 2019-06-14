/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Types;

namespace SwiftPSO.Core.Particle.GuideProvider
{
    public class NBestGuideProvider : IGuideProvider
    {
        public Vector GetGuide(IParticle particle)
        {
            return particle.NeighbourhoodBest.BestPosition;
        }
    }
}