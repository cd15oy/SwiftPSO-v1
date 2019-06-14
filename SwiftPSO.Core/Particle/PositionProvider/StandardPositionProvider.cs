/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */

namespace SwiftPSO.Core.Particle.PositionProvider
{
    public class StandardPositionProvider : IPositionProvider
    {
        public void UpdatePosition(IParticle particle)
        {
            //TOOD: update previous position
            for (int i = 0; i < particle.Position.Length; i++)
            {
                particle.Position[i] += particle.Velocity[i];
            }
        }
    }
}