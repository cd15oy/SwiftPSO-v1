/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Fitness;
using SwiftPSO.Core.Types;
using System;

namespace SwiftPSO.Core.Problem
{
    public class OptimizationSolution : IComparable<OptimizationSolution>
    {
        public Vector Position { get; }
        public IFitness Fitness { get; }

        public OptimizationSolution(Vector position, IFitness fitness)
        {
            Position = position;
            Fitness = fitness;
        }

        public int CompareTo(OptimizationSolution other)
        {
            return Fitness.CompareTo(other.Fitness);
        }

        public override string ToString()
        {
            return Position.ToString();
        }
    }
}