/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using System;

namespace SwiftPSO.Core.Termination
{
    public class MaximumValue : IValueTermination
    {
        private double _percentage;

        public MaximumValue()
        {
            _percentage = 0;
        }

        public bool Terminate(double value, double target)
        {
            return value >= target;
        }

        public double CalculateCompletion(double value, double target)
        {
            if (target == 0) throw new ArgumentException("Termination target can not be zero.");
            _percentage = Math.Max(_percentage, value / target);
            return Math.Max(Math.Min(_percentage, 1), 0); //TODO: this seems odd...
        }

    }
}