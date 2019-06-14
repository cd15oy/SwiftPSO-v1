/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using System;

namespace SwiftPSO.Core.Types
{
    public class Matrix
    {
        private double[][] _elements;

        public double this[int row, int column]
        {
            get => _elements[row][column];
            set => _elements[row][column] = value;
        }

        public int Rows => _elements.Length;
        public int Columns => _elements.GetLength(0);

        public Matrix(int rows, int columns)
        {
            _elements = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                _elements[i] = new double[columns];
            }
        }

        public Matrix(int rows, int columns, Func<int, int, double> initializer)
        {
            _elements = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                _elements[i] = new double[columns];
                for (int j = 0; j < columns; j++)
                {
                    _elements[i][j] = initializer(i, j);
                }
            }
        }

        public Matrix(Matrix copy)
        {
            _elements = new double[copy.Rows][];
            for (int i = 0; i < copy.Rows; i++)
            {
                _elements[i] = new double[copy.Columns];
                for (int j = 0; j < copy.Columns; j++)
                {
                    _elements[i][j] = copy[i, j];
                }
            }
        }

        public Matrix Clone()
        {
            return new Matrix(this);
        }

        /// <summary>
        /// Create an identity matrix of provided size. An identity matrix is a square matrix with
        /// diagonal entries set to 1 and all other entries are 0.
        /// </summary>
        /// <param name="size">The size of the identity matrix.</param>
        /// <returns>An nxn identity matrix.</returns>
        public static Matrix Identity(int size)
        {
            Matrix result = new Matrix(size, size);
            for (int i = 0; i < size; i++)
            {
                result[i, i] = 1;
            }

            return result;
        }

        /// <summary>
        /// Perform GramSchmidt orthogonalization.
        /// This modifies the matrix
        /// </summary>
        public (Matrix, Matrix) GramSchmidt()
        {
            Matrix q = Clone();
            Matrix r = new Matrix(q.Columns, q.Columns);
            Factorize(q, r);

            return (q, r);
        }

        public static Matrix operator *(Matrix a, Matrix b)//TODO: optimize multiplication
        {
            //TODO: assert sizes
            Matrix c = new Matrix(a.Rows, b.Columns);
            for (int i = 0; i < c.Rows; i++)
            {
                for (int j = 0; j < c.Columns; j++)
                {
                    c[i, j] = 0;
                    for (int k = 0; k < a.Columns; k++)
                    {
                        c[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return c;
        }

        /// <summary>
        /// Factorize the matrix q. 
        /// </summary>
        /// <remarks>q will be overwritten.</remarks>
        /// <param name="q"></param>
        /// <param name="r"></param>
        private void Factorize(Matrix q, Matrix r)
        {
            for (int k = 0; k < q.Columns; k++)
            {
                double norm = 0;
                for (int i = 0; i < q.Rows; i++)
                {
                    norm += q[i, k] * q[i, k];
                }

                norm = Math.Sqrt(norm);
                if (norm == 0) throw new ArgumentException("Matrix not rank deficient");

                r[k, k] = norm;
                for (int i = 0; i < q.Rows; i++)
                {
                    q[i, k] /= norm;
                }

                for (int j = k + 1; j < q.Columns; j++)
                {
                    int k1 = k;
                    int j1 = j;

                    double dot = 0;
                    for (int index = 0; index < q.Rows; index++)
                    {
                        dot += q[index, k1] * q[index, j1];
                    }

                    r[k, j] = dot;
                    for (int i = 0; i < q.Rows; i++)
                    {
                        q[i, j] -= (q[i, k] * dot);
                    }
                }
            }
        }
    }
}
