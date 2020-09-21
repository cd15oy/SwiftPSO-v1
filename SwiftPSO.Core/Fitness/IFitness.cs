/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using System;

namespace SwiftPSO.Core.Fitness
{
    public interface IFitness : IComparable<IFitness>
    {
        double Value { get; }

        IFitness Clone();

        IFitness GetNew(double value); //Returns a new fitness object of the underlying type, having the provided value
    }
}