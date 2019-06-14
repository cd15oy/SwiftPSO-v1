/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SwiftPSO.Simulator
{
    public class AlgorithmExecutor
    {
        public List<IAlgorithm> Execute(Func<IAlgorithm> algorithmGenerator, int runs, string description)
        {
            Task<IAlgorithm>[] tasks = new Task<IAlgorithm>[runs];

            int completed = 0;
            Console.Write($"Starting {runs} instances of {description}: ");
            ProgressBar progress = new ProgressBar();
            progress.Report(0);
            for (int i = 0; i < runs; i++)
            {
                //use LongRunning to get dedicated threads for each run, which are needed for the Instance 
                tasks[i] = Task<IAlgorithm>.Factory.StartNew(() => ExecuteAlgorithm(algorithmGenerator), TaskCreationOptions.LongRunning);
                //report progress as completed/runs
                tasks[i].ContinueWith(x => progress.Report((double)Interlocked.Increment(ref completed) / runs));
            }

            Task.WaitAll(tasks);
            progress.Report(1); //force the progress bar to read 100%
            Console.WriteLine(); //force a new line after the simulation completes.
            return tasks.Select(x => x.Result).ToList();
        }

        public IAlgorithm ExecuteAlgorithm(Func<IAlgorithm> algorithmGenerator)
        {
            IAlgorithm algorithm = algorithmGenerator();
            algorithm.Run();

            return algorithm;
        }
    }
}
