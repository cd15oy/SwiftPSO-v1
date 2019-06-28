/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Random;
using SwiftPSO.Core.Types;
using System;
using System.Collections;

namespace SwiftPSO.FLA.Sampling
{
    public class ManhattanProgressiveRandomWalk
    {
        public Vector[] Walk(int steps, Domain problemDomain, BitArray startingZone, double stepSize)
        {
            if (problemDomain.Dimension != startingZone.Count) throw new ArgumentException("Problem bounds and starting zone have different dimensions.");

            Vector[] result = new Vector[steps + 1];

            //generate initial position within the starting zone
            Vector initialPos = new Vector(problemDomain.Dimension);
            for (int d = 0; d < initialPos.Length; d++)
            {
                double r = RandomProvider.NextDouble(0, problemDomain.DimensionBounds[d].Range / 2);
                if (startingZone[d]) initialPos[d] = problemDomain.DimensionBounds[d].UpperBound - r;
                else initialPos[d] = problemDomain.DimensionBounds[d].LowerBound + r;
            }

            //generate random dimension, set to min or max
            int rd = RandomProvider.NextInt(0, problemDomain.Dimension - 1);
            if (startingZone[rd]) initialPos[rd] = problemDomain.DimensionBounds[rd].UpperBound;
            else initialPos[rd] = problemDomain.DimensionBounds[rd].LowerBound;

            result[0] = initialPos;

            //perform the walk
            for (int i = 1; i <= steps; i++)
            {
                rd = RandomProvider.NextInt(0, problemDomain.Dimension - 1); //generate a random dimension to perform a step
                Vector v = new Vector(problemDomain.Dimension);
                for (int d = 0; d < problemDomain.Dimension; d++)
                {
                    if (d == rd) //perform a step in the random dimension
                    {
                        int inc = startingZone[d] ? -1 : 1; //decide step direction based on startingZone

                        v[d] = result[i - 1][d] + (inc * stepSize);

                        if (!problemDomain.DimensionBounds[d].IsInsideBounds(v[d]))
                        {
                            v[d] = result[i - 1][d] - (inc * stepSize);
                            startingZone[d] = !startingZone[d];
                        }
                    }
                    else   //for all other dimensions, position does not change
                    {
                        v[d] = result[i - 1][d];
                    }
                }

                result[i] = v.Clone();
            }

            return result;
        }
    }
}
