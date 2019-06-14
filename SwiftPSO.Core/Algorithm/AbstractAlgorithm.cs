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
using System.Threading;

namespace SwiftPSO.Core.Algorithm
{
    public abstract class AbstractAlgorithm : IAlgorithm
    {
        private bool _initialized;

        public int Iteration { get; private set; }
        public ITerminationCriterion TerminationCriterion { get; set; }
        public IProblem Problem { get; set; }

        public event EventHandler<IAlgorithm> InitializationComplete; //TODO: this is an odd way of passing event args
        public event EventHandler<IAlgorithm> IterationComplete;
        public event EventHandler<IAlgorithm> AlgorithmComplete;

        private static ThreadLocal<IAlgorithm> _instance;

        /// <summary>
        /// Gets or sets the currently running instance.
        /// </summary>
        public static IAlgorithm CurrentInstance
        {
            get
            {
                return _instance.Value;
            }
            set
            {
                _instance.Value = value;
            }
        }

        protected AbstractAlgorithm()
        {
            _instance = new ThreadLocal<IAlgorithm>();
            _initialized = false;
            TerminationCriterion = new MeasurementTermination();
        }

        /// <summary>
        /// Main execution of the algorithm.
        /// </summary>
        public void Run()
        {
            //TODO: ensure we have a termination criteria and problem
            _instance.Value = this;
            if (!_initialized)
            {
                PerformInitialization();
            }

            //_instance.Value = this;
            while (!TerminationCriterion.Terminate(this))
            {
                //_instance.Value = this; //TODO: set instance for multipopulation?
                PerformIteration();
                IterationComplete?.Invoke(this, this);
                //_instance.Value = null;
            }

            AlgorithmComplete?.Invoke(this, this);
            _instance.Value = null;
        }

        /// <summary>
        /// Perform general initialization.
        /// </summary>
        public void PerformInitialization()
        {
            //_instance.Value = this; //TODO: set instance for multipopulation?
            Iteration = 0;
            InitializeAlgorithm(Problem);
            _initialized = true;
            InitializationComplete?.Invoke(this, this);
            // _instance.Value = null;
        }

        /// <summary>
        /// Perform initialization of the algorithm.
        /// </summary>
        /// <param name="problem"></param>
        protected virtual void InitializeAlgorithm(IProblem problem)
        {
            //by deault, do nothing
        }

        /// <summary>
        /// Perform a general iteration.
        /// </summary>
        public void PerformIteration()
        {
            AlgorithmIteration();
            Iteration++;
        }

        public double CalculateCompletion()
        {
            return TerminationCriterion.CalculateCompletion(this);
        }

        /// <summary>
        /// Perform an iteration of the algorithm.
        /// </summary>
        protected abstract void AlgorithmIteration();

        public abstract OptimizationSolution GetBestSolution();

        public abstract IEnumerable<OptimizationSolution> GetSolutions();
    }
}