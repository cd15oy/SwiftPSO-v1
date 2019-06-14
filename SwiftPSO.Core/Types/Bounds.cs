/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using System;

namespace SwiftPSO.Core.Types
{
    public class Bounds
    {
        public double LowerBound { get; }
        public double UpperBound { get; }
        public double Range => Math.Abs(UpperBound - LowerBound);

        public Bounds() : this(double.MinValue, double.MaxValue)
        {
        }

        public Bounds(double lowerBound, double upperBound)
        {
            if (upperBound < lowerBound) throw new ArgumentException("Upper bound cannot be less than lower bound");

            LowerBound = lowerBound;
            UpperBound = upperBound;
        }

        public Bounds Clone()
        {
            return new Bounds(LowerBound, UpperBound);
        }

        public bool IsInsideBounds(double value)
        {
            return value.CompareTo(UpperBound) <= 0 && value.CompareTo(LowerBound) >= 0;
        }

        public override string ToString()
        {
            return $"({LowerBound}:{UpperBound})";
        }

    }
}