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
    public class ProgressiveRandomWalk
    {
        public Vector[] Walk(int steps, Domain problemDomain, BitArray startingZone, double[] stepSizes)
        {
            if (problemDomain.Dimension != startingZone.Count) throw new ArgumentException("Problem bounds and starting zone have different dimensions.");
            if (problemDomain.Dimension != stepSizes.Length) throw new ArgumentException("Problem bounds and step sizes have different dimensions.");

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
                Vector v = new Vector(problemDomain.Dimension);
                for (int d = 0; d < problemDomain.Dimension; d++)
                {
                    double r = RandomProvider.NextDouble(0, stepSizes[d]);
                    if (startingZone[d]) r = -r;

                    v[d] = result[i - 1][d] + r;

                    //mirror infeasible positions and flip startingZone bit
                    if (v[d] > problemDomain.DimensionBounds[d].UpperBound)
                    {
                        v[d] = problemDomain.DimensionBounds[d].UpperBound - (v[d] - problemDomain.DimensionBounds[d].UpperBound);
                        startingZone[d] = !startingZone[d];
                    }
                    else if (v[d] < problemDomain.DimensionBounds[d].LowerBound)
                    {
                        v[d] = problemDomain.DimensionBounds[d].LowerBound + (problemDomain.DimensionBounds[d].LowerBound - v[d]);
                        startingZone[d] = !startingZone[d];
                    }
                }

                result[i] = v.Clone();
            }

            return result;
        }
    }
}
