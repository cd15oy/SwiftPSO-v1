/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using MathNet.Numerics.LinearAlgebra;
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Random;
using SwiftPSO.Core.Types;
using SwiftPSO.Core.Utility;
using System;

namespace SwiftPSO.Benchmarks
{
    public class Schwefel2_6 : IContinuousFunction
    {
        private Vector _optimum;
        private Matrix<double> _a;
        private Vector _b;
        private Vector _z;
        private bool _initialized;

        public Schwefel2_6()
        {
            _initialized = false;
        }

        public double Evaluate(Vector input)
        {
            if (!_initialized)
            {
                Initialize(input.Length);
                _initialized = true;
            }

            double max = double.MinValue;

            MultiplyA(input, _z);

            for (int i = 0; i < input.Length; i++)
            {
                double temp = Math.Abs(_z[i] - _b[i]);

                max = Math.Max(max, temp);
            }

            return max;
        }

        private void Initialize(int dimension)
        {
            _a = MatrixGenerator.Random(dimension, -500, 500); //TODO: verify det(A) not 0.
            _b = new Vector(dimension);
            _z = new Vector(dimension);
            _optimum = BuildOptimum(dimension);

            MultiplyA(_optimum, _b);
        }

        private Vector BuildOptimum(int dimension)
        {
            Vector result = new Vector(dimension);

            for (int i = 0; i < dimension; i++)
            {
                if ((i + 1) <= Math.Ceiling(dimension / 4.0)) //use (i+1) for 1-based indexing in the vector 
                {
                    result[i] = -100;
                }
                else if ((i + 1) >= Math.Floor((3 * dimension) / 4.0))
                {
                    result[i] = 100;
                }
                else
                {
                    result[i] = RandomProvider.NextInt(-100, 100);
                }
            }

            return result;
        }

        /// <summary>
        /// Multiply the input by the (internal) matrix, storing the results in result.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="result"></param>
        private void MultiplyA(Vector input, Vector result)
        {
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = 0;

                for (int j = 0; j < result.Length; j++)
                {
                    result[i] += _a[i, j] * input[j]; //TODO: ensure we are using i,j correctly.
                }
            }
        }
    }
}
