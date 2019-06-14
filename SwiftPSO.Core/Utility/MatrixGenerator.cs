/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using MathNet.Numerics.LinearAlgebra;
using SwiftPSO.Core.ControlParameter;
using SwiftPSO.Core.Random;
using SwiftPSO.Core.Random.Distributions;
using SwiftPSO.Core.Types;
using System;

namespace SwiftPSO.Core.Utility
{
    public static class MatrixGenerator
    {
        /// <summary>
        /// Build a random square identity matrix.
        /// </summary>
        /// <param name="size">The size of the matrix.</param>
        /// <returns></returns>
        /*public static Matrix<double> Identity(int size)
        {
            return Matrix<double>.Build.DenseIdentity(size);
        } */

        /// <summary>
        /// Build a random square matrix where each entry is within the specified bounds.
        /// </summary>
        /// <param name="size">The size of the matrix.</param>
        /// <param name="bounds">The bounds for each entry.</param>
        /// <returns></returns>
        /*public static Matrix<double> Random(int size, Bounds bounds)
        {
            MatrixBuilder<double> matrixBuilder = Matrix<double>.Build;
            Matrix<double> matrix = matrixBuilder.Dense(size, size, (i, j) => RandomProvider.NextDouble(bounds));

            return matrix;
        } */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="minValue">THe (inclusive) lower bound.</param>
        /// <param name="maxValue">The (inclusive) upper bound.</param>
        /// <returns></returns>
        public static Matrix<double> Random(int size, int minValue, int maxValue)
        {
            MatrixBuilder<double> matrixBuilder = Matrix<double>.Build;
            Matrix<double> matrix = matrixBuilder.Dense(size, size, (i, j) => RandomProvider.NextInt(minValue, maxValue));

            return matrix;
        }

        /// <summary>
        /// Build a random square orthogonal matrix.
        /// </summary>
        /// <param name="size">The size of the matrix.</param>
        /// <returns></returns>
        /*public static Matrix<double> RandomOrthogonal(int size)
        {
            GaussianDistribution dist = new GaussianDistribution();
            MatrixBuilder<double> matrixBuilder = Matrix<double>.Build;
            Matrix<double> matrix = matrixBuilder.Dense(size, size, (i, j) => dist.Sample());

            GramSchmidt<double> gs = matrix.GramSchmidt();
            return gs.Q;
        } */

        public static Matrix Identity(int size)
        {
            return Matrix.Identity(size);
        }

        public static Matrix RandomOrthogonal(int size)
        {
            GaussianDistribution dist = new GaussianDistribution();
            Matrix m = new Matrix(size, size, (i, j) => dist.Sample());
            (Matrix q, Matrix r) = m.GramSchmidt();
            return q;
        }

        /// <summary>
        /// Generate a random square linear transformation matrix.
        /// </summary>
        /// <param name="size">The size of the matrix.</param>
        /// <param name="condition">The condition number.</param>
        /// <returns></returns>
        /*public static Matrix<double> RandomLinearTransformation(int size, int condition)
        {
            UniformDistribution distribution = new UniformDistribution(new ConstantControlParameter(1), new ConstantControlParameter(size + 1));

            Matrix<double> p = RandomOrthogonal(size);
            Matrix<double> q = RandomOrthogonal(size);

            //create a blank nxn matrix
            Matrix<double> n = Matrix<double>.Build.Dense(size, size, (i,j) => i == j ? Math.Pow(condition, (distribution.Sample() - 1) / size) : 0.0);

            return p * n * q;
        } */

        public static Matrix RandomLinearTransformation(int size, int condition)
        {
            UniformDistribution distribution = new UniformDistribution(new ConstantControlParameter(1), new ConstantControlParameter(size + 1));
            Matrix p = RandomOrthogonal(size);
            Matrix q = RandomOrthogonal(size);

            Matrix n = new Matrix(size, size, (i, j) => i == j ? Math.Pow(condition, (distribution.Sample() - 1) / size) : 0.0);

            return p * n * q;
        }
    }
}
