/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;
using SwiftPSO.Core.Types;
using SwiftPSO.Core.Utility.Center;

namespace SwiftPSO.Core.Particle.GuideProvider
{
    public class SwarmCenterGuideProvider : IGuideProvider
    {
        public ICenter CenterStrategy;

        public SwarmCenterGuideProvider() : this(new SpatialCenter())
        { }

        public SwarmCenterGuideProvider(ICenter centerStrategy)
        {
            CenterStrategy = centerStrategy;
        }

        public Vector GetGuide(IParticle particle)
        {
            PSO pso = AbstractAlgorithm.CurrentInstance as PSO;

            return CenterStrategy.GetCenter(pso.Particles); //TODO: this recalculates for each particle... -> is this needed? For async, it makes sense
                                                            //for sync, this would be affected by each particle update though...
        }
    }
}
