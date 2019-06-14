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
    public class Rosenbrock : IContinuousFunction
    {
        public double Evaluate(Vector input)
        {
            double sum = 0;

            for (int i = 0; i < input.Length - 1; i++)
            {
                double a = input[i];
                double b = input[i + 1];

                double x = b - a * a;
                double y = a - 1;

                sum += (100 * x * x) + (y * y);
            }

            return sum;
        }
    }
}
