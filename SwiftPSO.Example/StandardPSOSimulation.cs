/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Benchmarks;
using SwiftPSO.Core.Algorithm;
using SwiftPSO.Core.Measurement;
using SwiftPSO.Core.Measurement.Diversity;
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Types;
using SwiftPSO.Simulator;
using System;
using System.Diagnostics;

namespace SwiftPSO.Example
{
    class StandardPSOSimulation
    {
        readonly int dimensions = 30;
        readonly int runs = 50;

        static void Main(string[] args)
        {
            new StandardPSOSimulation().Run();
        }

        private void Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //define the problems to optimize
            ProblemSuite problemSuite = new ProblemSuite();
            problemSuite.AddFunction(() => new AbsoluteValue(), new Domain(-100, 100, dimensions), "Absolute Value");
            problemSuite.AddFunction(() => new Spherical(), new Domain(-5.12, 5.12, dimensions), "Spherical");
            problemSuite.AddFunction(() => new Rastrigin(), new Domain(-5.12, 5.12, dimensions), "Rastrigin");

            //we can also add shifted and rotated functions quite easily!
            //problemSuite.AddRotatedFunction(() => new Ackley(), RotationType.Orthonormal, new Domain(-32.768, 32.768, dimensions), "Rotated Ackley");
            //problemSuite.AddShiftedFunction(() => new Spherical(), new ConstantControlParameter(-450), new ConstantControlParameter(10), new Domain(-5.12, 5.12, dimensions), "Shifted Spherical")

            //define the measurement suite to use
            MeasurementSuite measurementSuite = new MeasurementSuite();
            measurementSuite.Add(new Fitness(), "Fitness");
            measurementSuite.Add(new Diversity(), "Diversity");
            measurementSuite.Add(new AverageParticleMovement(), "Average Particle Movement");
            measurementSuite.Add(new BoundViolations(), "Bound Violations");

            //create the executor and have it execute the simulations
            SimulationExecutor executor = new SimulationExecutor();

            //defining a Func<IProblem, IAlgorithm> is the simplest way to go as it allows directly passing your problem suite
            executor.Execute((p) => CreatePSO(p), problemSuite, measurementSuite, runs, "PSO", "Output");

            //alternatively, you can define a Func<IAlgorithm> and manually execute each simulation
            /*foreach (var kvp in problemSuite.Problems)
            {
                run a simulation by explicitly creating a Func<IAlgorithm>
                executor.Execute(() => CreatePSO(kvp.Value), measurementSuite, sims, "PSO", Path.Combine("Output", kvp.Key));

                alternatively, you can pass a Func<IAlgorithm> if you wish
                executor.Execute(CreatePSOFunc(problem), measurements, sims, "PSO", Path.Combine("Output", kvp.Key));
            } */
            stopwatch.Stop();
            Console.WriteLine($"Elapsed Time: {stopwatch.Elapsed}");
        }

        /// <summary>
        /// Create a standard <see cref="PSO"/> algorithm given the optimization problem.
        /// </summary>
        /// <param name="problem">The optimization problem.</param>
        /// <returns>A new instance of the <see cref="PSO"/> algorithm.</returns>
        private PSO CreatePSO(IProblem problem)
        {
            return new PSO
            {
                Problem = problem
            };
        }

        /// <summary>
        /// Create a function that produces a standard <see cref="PSO"/> for the given problem.
        /// </summary>
        /// <param name="problem">The optimization problem.</param>
        /// <returns>A function that produces the <see cref="PSO"/> algorithm.</returns>
        private Func<PSO> CreatePSOFunc(IProblem problem)
        {
            return () => new PSO
            {
                Problem = problem
            };
        }
    }
}
