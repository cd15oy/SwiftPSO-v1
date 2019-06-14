/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Types;
using System;

namespace SwiftPSO.Benchmarks
{
    public class Schaffer6 : IContinuousFunction
    {
        public double Evaluate(Vector input)
        {
            double sum = 0;

            for (int i = 0; i < input.Length - 1; i++)
            {
                double xi = input[i];
                double xj = input[i + 1];

                double xi2 = xi * xi;
                double xj2 = xj * xj;

                double sinSquare = Math.Sin(Math.Sqrt((100 * xi2) + xj2));
                sinSquare *= sinSquare;

                double squareVal = xi2 - (2 * xi * xj) + xj2;
                squareVal *= squareVal;

                sum += 0.5 + ((sinSquare - 0.5) / (1 + (0.001 * squareVal)));
            }

            return sum;
        }
    }
}