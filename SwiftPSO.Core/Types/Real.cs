/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Random;
using System;
using System.Globalization;

namespace SwiftPSO.Core.Types
{
    public class Real : INumeric
    {
        public int IntValue => (int)Math.Round(DoubleValue); //TODO: is rounding acceptable?

        public double DoubleValue { get; set; }

        public Bounds Bounds { get; }

        public Real() : this(0.0)
        { }

        public Real(double value) : this(value, new Bounds())
        { }

        public Real(double value, Bounds bounds)
        {
            DoubleValue = value;
            Bounds = bounds;
        }

        public int CompareTo(INumeric other)
        {
            return DoubleValue.CompareTo(other.DoubleValue);
        }

        public override string ToString()
        {
            return DoubleValue.ToString(CultureInfo.CurrentCulture);
        }

        public void Randomize()
        {
            DoubleValue = RandomProvider.NextDouble(Bounds);
        }

        public Real Clone()
        {
            return new Real(DoubleValue, Bounds);
        }

        public bool IsInsideBounds()
        {
            return Bounds.IsInsideBounds(DoubleValue);
        }

        #region Operators
        /// <summary>
        /// Return the sum of two Real values. The bounds are copied from the first operand.
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        public static Real operator +(Real op1, Real op2)
        {
            return new Real(op1.DoubleValue + op2.DoubleValue, op1.Bounds);
        }

        /// <summary>
        /// Return the sum of a Real value and a double value. The bounds are copied from the first operand.
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        public static Real operator +(Real op1, double op2)
        {
            return new Real(op1.DoubleValue + op2, op1.Bounds);
        }

        /// <summary>
        /// Return the difference of two Real values. The bounds are copied from the first operand.
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        public static Real operator -(Real op1, Real op2)
        {
            return new Real(op1.DoubleValue - op2.DoubleValue, op1.Bounds);
        }

        /// <summary>
        /// Return the difference of a Real value and a double value. The bounds are copied from the first operand.
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        public static Real operator -(Real op1, double op2)
        {
            return new Real(op1.DoubleValue - op2, op1.Bounds);
        }

        /// <summary>
        /// Return the product of two Real values. The bounds are copied from the first operand.
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        public static Real operator *(Real op1, Real op2)
        {
            return new Real(op1.DoubleValue * op2.DoubleValue, op1.Bounds);
        }

        /// <summary>
        /// Return the product of a Real value and a double value. The bounds are copied from the first operand.
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        public static Real operator *(Real op1, double op2)
        {
            return new Real(op1.DoubleValue * op2, op1.Bounds);
        }

        /// <summary>
        /// Return the quotient of two Real values. 
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        /// <exception cref="DivideByZeroException">If the value of the divisor is 0.</exception>
        public static Real operator /(Real op1, Real op2)
        {
            if (op2.DoubleValue.CompareTo(0) == 0) throw new DivideByZeroException();
            return new Real(op1.DoubleValue / op2.DoubleValue, op1.Bounds);
        }

        /// <summary>
        /// Return the quotient of a Real value and a double. 
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        /// <exception cref="DivideByZeroException">If the value of the divisor is 0.</exception>
        public static Real operator /(Real op1, double op2)
        {
            if (op2.CompareTo(0) == 0) throw new DivideByZeroException();
            return new Real(op1.DoubleValue / op2, op1.Bounds);
        }

        #endregion Operators
    }
}