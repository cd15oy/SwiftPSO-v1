/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using System;

namespace SwiftPSO.Core.Termination
{
    public class MinimumValue : IValueTermination
    {
        private double _percentage;
        private double _maxValue;

        public MinimumValue()
        {
            _percentage = 0.0;
            _maxValue = double.MinValue;
        }

        public bool Terminate(double value, double target)
        {
            return value <= target;
        }

        public double CalculateCompletion(double value, double target)
        {
            _maxValue = Math.Max(value, _maxValue);
            _percentage = Math.Max(_percentage, 1.0 - (value - target) / (_maxValue - target));

            return Math.Max(Math.Min(_percentage, 1.0), 0.0); //TODO: seems a bit odd..
        }
    }
}