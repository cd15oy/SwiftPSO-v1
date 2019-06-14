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
    public class FunctionOptimizationProblem : AbstractProblem
    {
        public IContinuousFunction Function { get; set; }

        public IObjective Objective { get; set; }

        public FunctionOptimizationProblem()
        {
            Objective = new Minimize();
        }

        protected override IFitness CalculateFitness(Vector solution)
        {
            return Objective.Evaluate(Function.Evaluate(solution));
        }
    }
}