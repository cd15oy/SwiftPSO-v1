/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;

namespace SwiftPSO.Core.Particle.PersonalBestUpdate
{
    public class StandardPersonalBestUpdate : IPersonalBestUpdate
    {
        public void UpdatePersonalBest(IParticle particle, IProblem problem)
        {
            particle.StagnationCounter++; //update stagnation counter - will be reset on update!

            if (particle.Fitness.CompareTo(particle.BestFitness) <= 0) return;

            particle.BestFitness = particle.Fitness.Clone();
            particle.FlagBestPosition();
            //particle.BestPosition = particle.Position.Clone();
            particle.StagnationCounter = 0;
        }
    }
}