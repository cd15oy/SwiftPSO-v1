/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Particle;
using SwiftPSO.Core.Types;
using System;
using System.Collections.Generic;

namespace SwiftPSO.Core.Utility.Center
{
    /// <summary>
    /// Calculate the center as the average position of all particles.
    /// </summary>
    public class SpatialCenter : ICenter
    {
        public Vector GetCenter(List<IParticle> particles)
        {
            if (particles.Count == 0) throw new ArgumentException("Must have at least 1 particle to calculate center.");
            int num = particles.Count;
            int dimensions = particles[0].Position.Length;

            double[] average = new double[dimensions];
            for (int i = 0; i < num; i++) //start at 1 to skip first particle
            {
                for (int j = 0; j < dimensions; j++)
                {
                    average[j] += particles[i].Position[j];
                }
            }

            for (int i = 0; i < num; i++)
            {
                average[i] /= num;
            }

            return new Vector(average);
        }
    }
}