/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Termination;
using System;
using System.Collections.Generic;
namespace SwiftPSO.Core.Algorithm
{
    public interface IAlgorithm
    {
        int Iteration { get; }
        ITerminationCriterion TerminationCriterion { get; set; }
        IProblem Problem { get; set; }

        event EventHandler<IAlgorithm> InitializationComplete;
        event EventHandler<IAlgorithm> IterationComplete;
        event EventHandler<IAlgorithm> AlgorithmComplete;

        void PerformInitialization();

        void PerformIteration();

        OptimizationSolution GetBestSolution();

        IEnumerable<OptimizationSolution> GetSolutions();

        double CalculateCompletion();

        void Run();
    }
}