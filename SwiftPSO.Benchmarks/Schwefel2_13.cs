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
    public class Schwefel2_13 : IContinuousFunction
    {
        private readonly Bounds _optimumBounds;
        private Vector _optimum;
        private bool _initialized;
        private Matrix<double> _a;
        private Matrix<double> _b;

        private Vector _aRow;
        private Vector _bRow;

        public Schwefel2_13()
        {
            _initialized = false;
            _optimumBounds = new Bounds(-Math.PI, Math.PI);

        }

        public double Evaluate(Vector input)
        {
            if (!_initialized)
            {
                Initialize(input.Length);
                _initialized = true;
            }

            double sum = 0;

            //calculate sum for the b rows
            for (int i = 0; i < input.Length; i++)
            {
                _bRow[i] = 0;
                for (int j = 0; j < input.Length; j++)
                {
                    _bRow[i] += _a[i, j] * Math.Sin(input[j]) + _b[i, j] * Math.Cos(input[j]); //TODO: ensure we are using i,j correctly.
                }

                double temp = _aRow[i] - _bRow[i];
                sum += temp * temp;
            }

            return sum;
        }

        private void Initialize(int dimension)
        {
            //TODO: this could be simplified into only two loops!
            _a = MatrixGenerator.Random(dimension, -100, 100);
            _b = MatrixGenerator.Random(dimension, -100, 100);
            _aRow = new Vector(dimension);
            _bRow = new Vector(dimension);
            _optimum = new Vector(dimension);

            for (int i = 0; i < dimension; i++)
            {
                _optimum[i] = RandomProvider.NextDouble(_optimumBounds);
            }

            //initialize the sum for the a rows
            for (int i = 0; i < dimension; i++)
            {
                _aRow[i] = 0;
                for (int j = 0; j < dimension; j++)
                {
                    _aRow[i] += (_a[i, j] * Math.Sin(_optimum[j]) + _b[i, j] * Math.Cos(_optimum[j])); //TODO: ensure we are using i,j correctly.
                }
            }
        }
    }
}
