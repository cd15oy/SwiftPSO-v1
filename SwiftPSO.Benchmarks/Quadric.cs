/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Benchmarks
{
    public class Quadric : IContinuousFunction
    {
        public double Evaluate(Vector input)
        {
            double sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                double temp = 0;
                for (int j = 0; j <= i; j++)
                {
                    temp += input[j];
                }
                sum += temp * temp;
            }

            return sum;
        }
    }
}