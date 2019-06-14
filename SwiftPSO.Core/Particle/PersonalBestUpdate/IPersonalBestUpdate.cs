/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;

namespace SwiftPSO.Core.Particle.PersonalBestUpdate
{
    public interface IPersonalBestUpdate
    {
        void UpdatePersonalBest(IParticle particle, IProblem problem);
    }
}