/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Types;
using SwiftPSO.Core.Utility.Distance;
using SwiftPSO.FLA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SwiftPSO.FLA.Metrics
{
    public class NearestNeighborFeatures
    {
        private readonly EuclideanDistance distanceMeasure = new EuclideanDistance();

        private double cor(double[] x, double[] y, double aveX, double aveY) {
            double productSum = 0;
            double xSqrSum = 0;
            double ySqrSum = 0;
            for(int i = 0; i < x.Length; i++) {
                productSum += (x[i]*y[i]);
                xSqrSum += (x[i]*x[i]);
                ySqrSum += (y[i]*y[i]);
            }
            return (productSum - (x.Length*aveX*aveY))/(Math.Sqrt(xSqrSum-(x.Length*aveX*aveX))*Math.Sqrt(ySqrSum -(x.Length*aveY*aveY)));
        }

        public (double,double,double,double,double) Calculate(IEnumerable<OptimizationSolution> solutions, Bounds[] bounds)
        {
            OptimizationSolution[] sorted = solutions.OrderByDescending(x => x.Fitness).Select(x => Helpers.Normalize(x, bounds)).ToArray();

            Matrix dist = new Matrix(sorted.Length, sorted.Length);
            for(int i = 0; i < sorted.Length; i++) {
                dist[i,i] = 0.0;
                for(int j = i+1; j < sorted.Length; j++) {
                    dist[i,j] = distanceMeasure.Measure(sorted[i].Position, sorted[j].Position);
                    dist[j,i] = dist[i,j];
                }
            }

            //Now we need to find the nearest solution, and nearest better solution 
            int[] NearestNeighbor = new int[sorted.Length];
            int[] NearestBetter = new int[sorted.Length]; //Index of nearest neighbor and nearest better 

            for(int i = 0; i < sorted.Length; i++) {
                NearestBetter[i] = -1; //Assume no better, and we search for a better solution
                NearestNeighbor[i] = (i>0)? i-1 : i+1; //start with any arbitrary other solution;
                for(int j = 0; j < sorted.Length; j++) {
                    if(i == j) continue; //Don't compare with self 
                    if(dist[i,j] < dist[i,NearestNeighbor[i]]) NearestNeighbor[i] = j; //Find any sol thats closer 
                    if(sorted[i].Fitness.CompareTo(sorted[j].Fitness) > 0) 
                        if(NearestBetter[i] == -1 || dist[i,j] < dist[i,NearestBetter[i]])
                            NearestBetter[i] = j;
                }
            }

            //Get some summary stats on the nearest neighbors and nearest betters 
            double[] NearestNeighborDistances = new double[sorted.Length];
            double[] NearestBetterDistances = new double[sorted.Length]; 

            for(int i = 0; i < sorted.Length; i++) {
                NearestNeighborDistances[i] = dist[i,NearestNeighbor[i]];
                if(NearestBetter[i] == -1)
                    NearestBetterDistances[i] = 0.0;
                else
                    NearestBetterDistances[i] = dist[i, NearestBetter[i]];
            }

            double AveNND = NearestNeighborDistances.Average();
            double SDNND = NearestNeighborDistances.Sum(x => Math.Pow(x-AveNND,2));
            SDNND /= (NearestNeighborDistances.Length-1.0);

            double AveNBD = NearestBetterDistances.Average();
            double SDNBD = NearestBetterDistances.Sum(x => Math.Pow(x-AveNBD,2));
            SDNBD /= (NearestBetterDistances.Length - 1.0);

            double CorNNNB = cor(NearestNeighborDistances, NearestBetterDistances, AveNND, AveNBD);

            double[] qnnnb = new double[sorted.Length];
            for(int i = 0; i < sorted.Length; i++)
                if(NearestBetterDistances[i] != 0)
                    qnnnb[i] = NearestNeighborDistances[i]/NearestBetterDistances[i];
                else 
                    qnnnb[i] = NearestNeighborDistances[i]/0.0000000001;
                    
            double AveQNNNB = qnnnb.Average();
            double SDQNNNB = qnnnb.Sum(x => Math.Pow(x-AveQNNNB,2));
            SDQNNNB /= (qnnnb.Length -1);

            double[] nbInDeg = new double[sorted.Length];
            //for each point x, count the number of other points for which x is the nearest better
            for(int i = 0; i < nbInDeg.Length; i++) 
                nbInDeg[i] = 0;
            for(int i = 0; i < nbInDeg.Length; i++)
                if(NearestBetter[i] != -1)
                    nbInDeg[NearestBetter[i]]+= 1.0;

            double AveInDeg = nbInDeg.Average();
            double AveFit = sorted.Average(x => x.Fitness.Value);
            double InDegCor = -cor(nbInDeg, sorted.Select(x=>x.Fitness.Value).ToArray(), AveInDeg, AveFit);

            return (SDNND/SDNBD, AveNND/AveNBD, CorNNNB, SDQNNNB, InDegCor);

        }
    }
}
