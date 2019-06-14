/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Types;
using System;
using System.Threading;

namespace SwiftPSO.Core.Random
{
    public class RandomProvider
    {
        private static int _seed = Environment.TickCount;

        //Threadstatic improves performance over threadlocal by ~33%. However, it only initializes for the initial thread, thus lazy initialization is needed
        [ThreadStatic] private static System.Random _random;

        private static System.Random RandomObject => _random ?? (_random = new System.Random(Interlocked.Increment(ref _seed)));


        /// <summary>
        /// Return a random double value in [0, 1)
        /// </summary>
        /// <returns></returns>
        public static double NextDouble()
        {
            return RandomObject.NextDouble();
        }

        /// <summary>
        /// Return a random double value within the range of the supplied Bounds.
        /// </summary>
        /// <remarks>Cannot produce values equal to the upper bound.</remarks>
        /// <param name="bounds">Bounds for the random value.</param>
        /// <returns></returns>
        public static double NextDouble(Bounds bounds)
        {
            return RandomObject.NextDouble() * bounds.Range + bounds.LowerBound;
        }

        /// <summary>
        /// Return a random non-negative integer value.
        /// </summary>
        /// <returns></returns>
        public static int NextInt()
        {
            return RandomObject.Next();
        }

        /// <summary>
        /// Return an integer value within the range specified by min and max.
        /// </summary>
        /// <param name="min">The (inclusive) minimum value.</param>
        /// <param name="max">The (inclusive) maximum value.</param>
        /// <returns></returns>
        public static int NextInt(int min, int max)
        {
            return RandomObject.Next(min, max + 1);
        }
    }
}