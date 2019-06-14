/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Types;

namespace SwiftPSO.FLA.Sampling
{
    public class UniformSample
    {
        public OptimizationSolution[] Sample(int samples, IProblem problem)
        {
            OptimizationSolution[] result = new OptimizationSolution[samples];
            for (int i = 0; i < samples; i++)
            {
                Vector position = new Vector(problem.Dimensions);

                position.Randomize(problem.ProblemDomain.DimensionBounds);

                //for (int d = 0; d < problem.Dimensions; d++)
                //{
                //    position[d] = RandomProvider.NextDouble(problem.ProblemDomain[d]);
                //}

                result[i] = new OptimizationSolution(position, problem.Evaluate(position));
            }

            return result;
        }
    }
}
