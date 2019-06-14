/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Fitness;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Core.Problem
{
    public abstract class AbstractProblem : IProblem
    {
        private int _fitnessEvaluations;

        public int FitnessEvaluations
        {
            get => _fitnessEvaluations;
            private set => _fitnessEvaluations = value;
        }

        public Domain ProblemDomain { get; set; }

        public int Dimensions => ProblemDomain.Dimension;

        protected AbstractProblem()
        {
            _fitnessEvaluations = 0;
        }

        public IFitness Evaluate(Vector solution)
        {
            //Interlocked.Increment(ref _fitnessEvaluations); //TODO: should this be threadsafe? Should only be incremented by a single thread
            _fitnessEvaluations++;
            return CalculateFitness(solution);
        }

        protected abstract IFitness CalculateFitness(Vector solution);
    }
}