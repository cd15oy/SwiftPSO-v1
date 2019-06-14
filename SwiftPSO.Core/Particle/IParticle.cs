/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;
using SwiftPSO.Core.Fitness;
using SwiftPSO.Core.Particle.Behaviour;
using SwiftPSO.Core.Particle.PersonalBestUpdate;
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Core.Particle
{
    public interface IParticle
    {
        Vector Position { get; set; }
        Vector Velocity { get; set; }
        IFitness Fitness { get; }
        Vector BestPosition { get; }
        IFitness BestFitness { get; set; }
        IParticle NeighbourhoodBest { get; set; }
        IPersonalBestUpdate PersonalBestUpdate { get; set; }
        Vector PreviousPosition { get; }
        IBehaviour Behaviour { get; set; }

        int StagnationCounter { get; set; }

        void Initialize(IProblem problem);

        void CalculateFitness(IProblem problem);

        void PerformIteration(IAlgorithm algorithm);

        void UpdatePreviousPosition();

        void FlagBestPosition();
    }
}