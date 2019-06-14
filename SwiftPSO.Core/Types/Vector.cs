/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Random;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SwiftPSO.Core.Types
{
    public class Vector : IEnumerable<double>, IMeasurable
    {
        private double[] _components;

        public double this[int index]
        {
            get => _components[index];
            set => _components[index] = value;
        }

        public int Length => _components.Length;

        /// <summary>
        /// Construct a new <see cref="Vector"/> with the specified components.
        /// </summary>
        /// <remarks>The components are not copied.</remarks>
        /// <param name="components">The components used to construct the new <see cref="Vector"/>.</param>
        public Vector(double[] components)
        {
            _components = components;
        }

        /// <summary>
        /// Construct a new <see cref="Vector"/> with the specified size.
        /// </summary>
        /// <param name="size">The number of components the new <see cref="Vector"/> can hold.</param>
        public Vector(int size)
        {
            _components = new double[size];
        }

        public Vector Clone()
        {
            return new Vector(_components.Clone() as double[]); //TODO: does this clone?
        }

        /// <summary>
        /// Construct a new vector using a slice of the elements in this Vector.
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public Vector Slice(int startIndex, int endIndex)
        {
            int elements = endIndex - startIndex;
            double[] slice = new double[elements];
            Array.Copy(_components, startIndex, slice, 0, elements);

            return new Vector(slice);
        }

        /// <summary>
        /// Randomize the values in the Vector by calling the <see cref="Real.Randomize"/> method on each component.
        /// </summary>
        public void Randomize(Bounds[] bounds)
        {
            if (bounds.Length != _components.Length) throw new ArgumentException("Bounds must be the same length as the vector");

            for (int i = 0; i < _components.Length; i++)
            {
                _components[i] = RandomProvider.NextDouble(bounds[i]);
            }
        }


        /// <summary>
        /// Calculate the L2 norm (i.e., magnitude) of the vector.
        /// </summary>
        /// <returns></returns>
        public double Norm()
        {
            double norm = 0;
            for (int i = 0; i < _components.Length; i++)
            {
                norm += _components[i] * _components[i];
            }

            return Math.Sqrt(norm);
        }

        /// <summary>
        /// Normalize a vector.
        /// </summary>
        /// <returns>The normalized vector.</returns>
        public Vector Normalize()
        {
            double norm = Norm();
            //if norm is 0, then vector is the normal vector, which is its own norm
            return norm.CompareTo(0.0) == 0 ? this : this / norm; //Note: division makes a copy

        }

        public double Dot(Vector other)
        {
            if (Length != other.Length) throw new ArithmeticException("Cannot perform dot product on vectors with differing dimensions.");

            double result = 0;
            for (int i = 0; i < Length; i++)
            {
                result += _components[i] * other[i];
            }

            return result;
        }

        public Vector Project(Vector other)
        {
            return this * (Dot(other) / other.Dot(other));
        }

        /// <summary>
        /// Return a new <see cref="Vector"/> with the specified function applied to each element.
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        public Vector Map(Func<double, double> function)
        {
            double[] result = new double[_components.Length];
            for (int i = 0; i < _components.Length; i++)
            {
                result[i] = function(_components[i]);
            }

            return new Vector(result);
        }

        /// <summary>
        /// Verify if each component of the vector is within the specified <see cref="Bounds"/>[].
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public bool IsInsideBounds(Bounds[] bounds)
        {
            for (int i = 0; i < _components.Length; i++)
            {
                if (!bounds[i].IsInsideBounds(_components[i])) return false;
            }
            return true;
        }

        /// <summary>
        /// Create a new vector of specified size using the value provided for each of the components.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Vector Fill(double value, int size)
        {
            double[] components = new double[size];
            for (int i = 0; i < size; i++)
            {
                components[i] = value;
            }

            return new Vector(components);
        }

        public override string ToString()
        {
            return $"[{string.Join(" ", _components)}]";
        }

        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)_components).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<double>)_components).GetEnumerator();
        }

        #region Operators

        /// <summary>
        /// Performs component-wise addition of two Vectors.
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Vector operator +(Vector op1, Vector op2)
        {
            if (op1.Length != op2.Length) throw new ArgumentException($"Cannot add vectors of different lengths. op1 has length {op1.Length} and op2 has length {op2.Length}");

            double[] result = new double[op1.Length];

            for (int i = 0; i < op1.Length; i++)
            {
                result[i] = op1[i] + op2[i];
            }

            return new Vector(result);
        }

        /// <summary>
        /// Adds a constant value to each component of the Vector.
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Vector operator +(Vector op1, double op2)
        {
            double[] result = new double[op1.Length];

            for (int i = 0; i < op1.Length; i++)
            {
                result[i] = op1[i] + op2;
            }

            return new Vector(result);
        }

        /// <summary>
        /// Performs component-wise subtraction of two Vectors.
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Vector operator -(Vector op1, Vector op2)
        {
            if (op1.Length != op2.Length) throw new ArgumentException($"Cannot subtract vectors of different lengths. op1 has length {op1.Length} and op2 has length {op2.Length}");

            double[] result = new double[op1.Length];

            for (int i = 0; i < op1.Length; i++)
            {
                result[i] = op1[i] - op2[i];
            }

            return new Vector(result);
        }

        /// <summary>
        /// Subtracts a constant value from each component of the Vector.
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Vector operator -(Vector op1, double op2)
        {
            double[] result = new double[op1.Length];

            for (int i = 0; i < op1.Length; i++)
            {
                result[i] = op1[i] - op2;
            }

            return new Vector(result);
        }

        /// <summary>
        /// Performs component-wise multiplication of two Vectors.
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Vector operator *(Vector op1, Vector op2)
        {
            if (op1.Length != op2.Length) throw new ArgumentException($"Cannot subtract vectors of different lengths. op1 has length {op1.Length} and op2 has length {op2.Length}");

            double[] result = new double[op1.Length];

            for (int i = 0; i < op1.Length; i++)
            {
                result[i] = op1[i] * op2[i];
            }

            return new Vector(result);
        }

        /// <summary>
        /// Performs element-wise multiplication of a Vector and a scalar.
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Vector operator *(Vector op1, double op2)
        {
            double[] result = new double[op1.Length];

            for (int i = 0; i < op1.Length; i++)
            {
                result[i] = op1[i] * op2;
            }

            return new Vector(result);
        }

        /// <summary>
        /// Performs element-wise division of a Vector by a scalar.
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Vector operator /(Vector op1, double op2)
        {
            double[] result = new double[op1.Length];

            for (int i = 0; i < op1.Length; i++)
            {
                result[i] = op1[i] / op2;
            }

            return new Vector(result);
        }

        #endregion Operators
    }
}