/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm.Initialization;
using SwiftPSO.Core.Iteration;
using SwiftPSO.Core.Particle;
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Topology;
using System.Collections.Generic;

namespace SwiftPSO.Core.Algorithm
{
    public class PSO : SinglePopulationPSO
    {
        public IIteration<PSO> IterationStrategy { get; set; }
        public IInitialization Initialization { get; set; }

        public PSO()
        {
            IterationStrategy = new SynchronousIteration();
            Initialization = new StandardInitialization();
        }

        protected override void InitializeAlgorithm(IProblem problem)
        {
            base.InitializeAlgorithm(problem);
            Particles = Initialization.Initialize(problem, SwarmSize);

            //calculate fitness for each particle
            foreach (IParticle particle in Particles)
            {
                particle.CalculateFitness(Problem);
            }

            //initialize each neighbourhood best - only after fitness has been calculated for all particles
            foreach (IParticle particle in Particles)
            {
                particle.NeighbourhoodBest = TopologyHelper.GetNeighbourhoodBest(Particles, particle, Topology);
            }
        }

        protected override void AlgorithmIteration()
        {
            IterationStrategy.PerformIteration(this);
        }

        public override OptimizationSolution GetBestSolution()
        {
            IParticle best = TopologyHelper.GetBestParticle(Particles);
            return new OptimizationSolution(best.BestPosition, best.BestFitness);
        }

        public override IEnumerable<OptimizationSolution> GetSolutions()
        {
            List<OptimizationSolution> solutions = new List<OptimizationSolution>(SwarmSize);
            foreach (IParticle particle in Particles)
            {
                solutions.Add(new OptimizationSolution(particle.BestPosition, particle.BestFitness));
            }

            return solutions;
        }
    }
}