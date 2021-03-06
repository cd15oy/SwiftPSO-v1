﻿/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using System.Globalization;

namespace SwiftPSO.Core.Fitness
{
    public class MinimizationFitness : IFitness
    {
        public double Value { get; }

        public MinimizationFitness(double value)
        {
            Value = value;
        }

        public IFitness Clone()
        {
            return new MinimizationFitness(Value);
        }

        public IFitness GetNew(double Value) {
            return new MinimizationFitness(Value);
        }

        /// <summary>
        /// Compare this fitness instance to another IFitness.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>A positive value to indicate this is a better fitness, a negative value to indicate this is a
        /// worse fitness, or zero if the two instances are equal</returns>
        public int CompareTo(IFitness other)
        {
            return other is InferiorFitness ? 1 : -Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.CurrentCulture);
        }
    }
}