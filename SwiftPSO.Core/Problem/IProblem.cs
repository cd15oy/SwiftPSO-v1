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
    public interface IProblem
    {
        int FitnessEvaluations { get; }

        Domain ProblemDomain { get; set; }

        int Dimensions { get; }

        IFitness Evaluate(Vector solution);
    }
}