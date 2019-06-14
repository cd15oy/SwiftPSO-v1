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
    public class StandardInitialization : IInitialization
    {
        public StandardParameterSet Parameters { get; set; }

        public StandardInitialization() : this(new StandardParameterSet())
        { }

        public StandardInitialization(StandardParameterSet parameters)
        {
            Parameters = parameters;
        }

        public List<IParticle> Initialize(IProblem problem, int number)
        {
            List<IParticle> particles = new List<IParticle>(number);
            for (int i = 0; i < number; i++)
            {
                StandardParticle particle = new StandardParticle();
                particle.Parameters = Parameters;
                particle.Initialize(problem);
                particles.Add(particle);
            }

            return particles;
        }
    }
}