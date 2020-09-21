/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using System.Globalization;

namespace SwiftPSO.Core.Fitness
{
    public class MaximizationFitness : IFitness
    {
        public double Value { get; }

        public MaximizationFitness(double value)
        {
            Value = value;
        }

        public IFitness Clone()
        {
            return new MaximizationFitness(Value);
        }

        public IFitness GetNew(double value) {
            return new MaximizationFitness(value);
        }

        /// <summary>
        /// Compare this fitness instance to another IFitness.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>A positive value to indicate this is a better fitness, a negative value to indicate this is a
        /// worse fitness, or zero if the two instances are equal</returns>
        public int CompareTo(IFitness other)
        {
            return other is InferiorFitness ? 1 : Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.CurrentCulture);
        }
    }
}