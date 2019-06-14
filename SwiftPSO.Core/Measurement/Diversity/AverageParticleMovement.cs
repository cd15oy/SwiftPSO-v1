/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;
using SwiftPSO.Core.Types;
using System;

namespace SwiftPSO.Core.Measurement.Diversity
{
    public class AverageParticleMovement : IMeasurement<Real>
    {
        //TODO: implement maximum value

        public Real GetValue(IAlgorithm algorithm)
        {
            SinglePopulationPSO pso = algorithm as SinglePopulationPSO;

            int particles = pso.SwarmSize;

            double sum = 0;
            for (int i = 0; i < particles; i++)
            {
                Vector pos = pso.Particles[i].Position;
                Vector prevPos = pso.Particles[i].PreviousPosition;

                //calculate the difference and norm in one loop
                double norm = 0;
                for (int j = 0; j < pos.Length; j++)
                {
                    double dist = pos[j] - prevPos[j];
                    norm += dist * dist;
                }

                sum += Math.Sqrt(norm);
            }

            return new Real(sum / particles);
        }
    }
}