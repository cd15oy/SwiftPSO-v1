/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Fitness;

namespace SwiftPSO.Core.Particle.NeighbourhoodBestUpdate
{
    public class StandardNeighbourhoodBestUpdate : INeighbourhoodBestUpdate
    {
        public IFitness GetSocialBestFitness(IParticle particle)
        {
            return particle.BestFitness;
        }
    }
}