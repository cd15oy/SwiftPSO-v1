/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;
using SwiftPSO.Core.Measurement;
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Types;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SwiftPSO.Simulator
{
    public class SimulationExecutor
    {
        /// <summary>
        /// Execute a <see cref="Simulation"/> a set number of times for each <see cref="IProblem"/> in the provided <see cref="ProblemSuite"/>. 
        /// The output files will be placed in '{outputDirectory}/{description}-{measurement}.csv.
        /// </summary>
        /// <param name="algorithmGenerator">A function that accepts and <see cref="IProblem"/> and produces an instance of the algorithm to be run.</param>
        /// <param name="problemSuite">A collection of <see cref="IProblem"/> builders that will be built and executed.</param>
        /// <param name="measurements">A collection of measurements to be recorded during the simulations.</param>
        /// <param name="runs">The number of times the simulation will be executed.</param>
        /// <param name="description">A description used for naming the output file.</param>
        /// <param name="outputDirectory">The directory to store output.</param>
        public void Execute(Func<IProblem, IAlgorithm> algorithmGenerator, ProblemSuite problemSuite, MeasurementSuite suite, int runs, string description, string outputDirectory)
        {
            foreach (var kvp in problemSuite.Problems)
            {
                Execute(() => algorithmGenerator(kvp.Value()), suite, runs, description, Path.Combine(outputDirectory, kvp.Key));
            }
        }

        /// <summary>
        /// Execute a <see cref="Simulation"/> a set number of times. The output files will be placed in '{outputDirectory}/{description}-{measurement}.csv.
        /// </summary>
        /// <param name="algorithmGenerator">A function producing an instance of the algorithm to be run.</param>
        /// <param name="measurements">A collection of measurements to be recorded during the simulations.</param>
        /// <param name="runs">The number of times the simulation will be executed.</param>
        /// <param name="description">A description used for naming the output file.</param>
        /// <param name="outputDirectory">The directory to store output.</param>
        public void Execute(Func<IAlgorithm> algorithmGenerator, MeasurementSuite measurements, int runs, string description, string outputDirectory)
        {
            Task<Simulation>[] tasks = new Task<Simulation>[runs];

            int completed = 0;
            Console.Write($"Starting {runs} instances of {description}: ");
            ProgressBar progress = new ProgressBar();
            progress.Report(0);
            for (int i = 0; i < runs; i++)
            {
                //use LongRunning to get dedicated threads for each simulation, which are needed for the Instance 
                tasks[i] = Task<Simulation>.Factory.StartNew(() => CreateSimulation(algorithmGenerator, measurements), TaskCreationOptions.LongRunning);
                //report progress as completed/runs
                tasks[i].ContinueWith(x => progress.Report((double)Interlocked.Increment(ref completed) / runs));
            }

            Task.WaitAll(tasks);
            progress.Report(1); //force the progress bar to read 100%
            Console.WriteLine(); //force a new line after the simulation completes.

            WriteOutput(tasks, description, outputDirectory);
        }

        /// <summary>
        /// Creates a <see cref="Simulation"/> object using the provided algorithm generator function and list of measurements.
        /// </summary>
        /// <returns>A <see cref="Simulation"/> object that is to be executed.</returns>
        private Simulation CreateSimulation(Func<IAlgorithm> algorithmGenerator, MeasurementSuite measurements)
        {
            IAlgorithm algorithm = algorithmGenerator();
            Simulation simulation = new Simulation(algorithm, measurements);
            simulation.Run();

            return simulation;
        }

        /// <summary>
        /// Writes the output of the simulations to a file.
        /// </summary>
        /// <param name="tasks"></param>
        private void WriteOutput(Task<Simulation>[] tasks, string description, string outputDirectory)
        {
            Directory.CreateDirectory(outputDirectory);

            //used to get row info.
            DataTable measureTableInformation = tasks[0].Result.MeasureTable; //TODO: getting measurement info is a bit hacky.

            //this assumes all tasks have the same measurements, which should be true since they are created by the same function.
            MeasurementSuite measurementSuite = tasks[0].Result.MeasurementSuite;

            //for (var index = 0; index < measurements.Count; index++)
            foreach (var kvp in measurementSuite.Measurements)
            {
                IMeasurement<IMeasurable> measurement = kvp.Value;
                string measureName = kvp.Key;
                string filename = $"{description}-{measureName}.csv";

                using (TextWriter writer = new StreamWriter(Path.Combine(outputDirectory, filename)))
                {
                    writer.Write($"Iteration, {string.Join(", ", Enumerable.Range(1, tasks.Length).Select(x => $"Run {x}"))}");
                    //cycle through each captured iteration, which should be consistent for all simulations
                    for (int i = 0; i < measureTableInformation.Rows.Count; i++)
                    {
                        writer.WriteLine(); //write a new line before each record
                        writer.Write(measureTableInformation.Rows[i][0]); //write the iteration number
                        for (int j = 0; j < tasks.Length; j++)
                        {
                            writer.Write($", {tasks[j].Result.MeasureTable.Rows[i][measureName]}");
                        }
                    }
                }
            }
        }
    }
}
