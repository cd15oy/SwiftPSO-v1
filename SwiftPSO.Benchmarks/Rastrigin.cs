﻿/*
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
    public class Rastrigin : IContinuousFunction
    {
        public double Evaluate(Vector input)
        {
            double sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                sum += input[i] * input[i] - 10 * Math.Cos(2 * Math.PI * input[i]);
            }

            return 10 * input.Length + sum;
        }
    }
}
