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
    public class EuclideanDistance : IDistance
    {
        public double Measure(Vector x, Vector y)
        {
            if (x.Length != y.Length) throw new ArgumentException("Cannot measure Euclidean distance for vectors with different lengths.");

            double distance = 0;

            for (int i = 0; i < x.Length; i++)
            {
                double temp = x[i] - y[i];
                distance += temp * temp;
            }

            return Math.Sqrt(distance);
        }
    }
}