/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Types;
using System;

namespace SwiftPSO.Core.Utility.Distance
{
    public class MinkowskiDistance : IDistance
    {
        public int Alpha { get; set; }

        public MinkowskiDistance() : this(1)
        {
            //do nothing
        }

        public MinkowskiDistance(int alpha)
        {
            Alpha = alpha;
        }

        public double Measure(Vector x, Vector y)
        {
            if (x.Length != y.Length) throw new ArgumentException("Cannot measure Minkowski distance for vectors with different lengths.");
            if (Alpha < 1) throw new ArgumentException($"Minkowski distance requires an Alpha > 1. Supplied value: {Alpha}");

            double distance = 0;

            for (int i = 0; i < x.Length; i++)
            {
                distance += Math.Pow(Math.Abs(x[i] - y[i]), Alpha);
            }

            return Math.Pow(distance, 1.0 / Alpha); //use 1.0 to force conversion to double
        }
    }
}