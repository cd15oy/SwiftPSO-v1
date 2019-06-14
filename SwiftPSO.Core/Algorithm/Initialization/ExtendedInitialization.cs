/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Particle;
using SwiftPSO.Core.Problem;
using System.Collections.Generic;

namespace SwiftPSO.Core.Algorithm.Initialization
{
    public class ExtendedInitialization : IInitialization
    {
        public ExtendedParameterSet Parameters { get; set; }

        public ExtendedInitialization() : this(new ExtendedParameterSet())
        { }

        public ExtendedInitialization(ExtendedParameterSet parameters)
        {
            Parameters = parameters;
        }

        public List<IParticle> Initialize(IProblem problem, int number)
        {
            List<IParticle> particles = new List<IParticle>(number);
            for (int i = 0; i < number; i++)
            {
                ExtendedParticle particle = new ExtendedParticle();
                particle.Parameters = Parameters;
                particle.Initialize(problem);
                particles.Add(particle);
            }

            return particles;
        }
    }
}
