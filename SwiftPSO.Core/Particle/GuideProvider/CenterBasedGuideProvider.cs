/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;
using SwiftPSO.Core.Random;
using SwiftPSO.Core.Types;
using SwiftPSO.Core.Utility.Center;
using System.Collections.Generic;

namespace SwiftPSO.Core.Particle.GuideProvider
{
    public class CenterBasedGuideProvider : IGuideProvider
    {
        public int Number { get; set; }
        public ICenter CenterStrategy { get; set; }

        public CenterBasedGuideProvider() : this(3, new SpatialCenter())
        { }

        public CenterBasedGuideProvider(int number, ICenter centerStrategy)
        {
            Number = number;
            CenterStrategy = centerStrategy;
        }

        public Vector GetGuide(IParticle particle)
        {
            //TODO: add normal distribution around the center?
            return CenterStrategy.GetCenter(GetRandomParticles());
        }

        private List<IParticle> GetRandomParticles()
        {
            PSO pso = AbstractAlgorithm.CurrentInstance as PSO;
            if (Number > pso.Particles.Count) Number = pso.Particles.Count;

            HashSet<int> selections = new HashSet<int>();

            //get random particle indices
            while (selections.Count < Number)
            {
                selections.Add(RandomProvider.NextInt(0, pso.Particles.Count - 1));
            }

            List<IParticle> result = new List<IParticle>(Number);
            foreach (int index in selections)
            {
                result.Add(pso.Particles[index]);
            }

            return result;
        }
    }
}
