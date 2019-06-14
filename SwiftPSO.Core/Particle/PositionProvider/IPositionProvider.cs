/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */

namespace SwiftPSO.Core.Particle.PositionProvider
{
    public interface IPositionProvider
    {
        void UpdatePosition(IParticle particle);
    }
}