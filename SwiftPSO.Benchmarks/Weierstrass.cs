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
    public class Weierstrass : IContinuousFunction
    {
        public double A { get; private set; }
        public double B { get; private set; }
        public double KMax { get; private set; }
        private double _constant;

        public Weierstrass() : this(0.5, 3, 20)
        { }

        public Weierstrass(double a, double b, double kMax)
        {
            A = a;
            B = b;
            KMax = kMax;
            ComputeConstant();
        }

        public double Evaluate(Vector input)
        {
            double sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for (int k = 0; k <= KMax; k++)
                {
                    sum += Math.Pow(A, k) * Math.Cos(2 * Math.PI * Math.Pow(B, k) * (input[i] + 0.5));
                }
            }

            return sum - input.Length * _constant;
        }

        private void ComputeConstant()
        {
            _constant = 0;

            for (int k = 0; k <= KMax; k++)
            {
                _constant += Math.Pow(A, k) * Math.Cos(2 * Math.PI * Math.Pow(B, k) * 0.5);
            }
        }
    }
}