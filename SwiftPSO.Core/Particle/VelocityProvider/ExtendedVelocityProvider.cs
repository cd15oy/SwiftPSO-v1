/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Particle.GuideProvider;
using SwiftPSO.Core.Random;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Core.Particle.VelocityProvider
{
    public class ExtendedVelocityProvider : IVelocityProvider
    {
        public IGuideProvider LocalGuideProvider { get; set; }
        public IGuideProvider NeighbourhoodGuideProvider { get; set; }
        public IGuideProvider AdditionalGuideProvider { get; set; }

        public ExtendedVelocityProvider()
        {
            LocalGuideProvider = new PBestGuideProvider();
            NeighbourhoodGuideProvider = new NBestGuideProvider();
            AdditionalGuideProvider = new CenterBasedGuideProvider();
        }

        /// <summary>
        /// Calculate the velocity as an unbounded vector using the standard inertia weight model.
        /// </summary>
        /// <param name="particle">The particle to calculate velocity.</param>
        /// <returns>An unbounded Vector containing the velocity.</returns>
        public void UpdateVelocity(IParticle particle)
        {
            ExtendedParticle sp = particle as ExtendedParticle;
            //TODO: update previous velocity
            int dim = sp.Position.Length;
            double w = sp.Parameters.InertiaWeight.Parameter;
            double c1 = sp.Parameters.CognitiveAcceleration.Parameter;
            double c2 = sp.Parameters.SocialAcceleration.Parameter;
            double c3 = sp.Parameters.ExtendedAcceleration.Parameter;
            double lambda = sp.Parameters.Lambda;

            Vector velocity = sp.Velocity;
            Vector pos = sp.Position;
            Vector lGuide = LocalGuideProvider.GetGuide(sp);
            Vector nGuide = NeighbourhoodGuideProvider.GetGuide(sp);
            Vector aGuide = AdditionalGuideProvider.GetGuide(sp);

            for (int i = 0; i < dim; i++)
            {
                double inertiaTerm = w * sp.Velocity[i];
                double cognitiveTerm = c1 * RandomProvider.NextDouble() * (lGuide[i] - pos[i]);
                double socialTerm = lambda * c2 * RandomProvider.NextDouble() * (nGuide[i] - pos[i]);
                double additionalTerm = (1 - lambda) * c3 * RandomProvider.NextDouble() * (aGuide[i] - pos[i]);
                velocity[i] = inertiaTerm + cognitiveTerm + socialTerm + additionalTerm;
            }
        }
    }
}
