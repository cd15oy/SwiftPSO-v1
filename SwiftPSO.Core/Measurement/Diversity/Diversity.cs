/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;
using SwiftPSO.Core.Types;
using SwiftPSO.Core.Utility.Center;
using SwiftPSO.Core.Utility.Distance;

namespace SwiftPSO.Core.Measurement.Diversity
{
    public class Diversity : IMeasurement<Real>
    {
        public ICenter Center { get; set; }
        public IDistance DistanceMeasure { get; set; }

        public Diversity()
        {
            Center = new SpatialCenter();
            DistanceMeasure = new EuclideanDistance();
        }

        public Real GetValue(IAlgorithm algorithm)
        {
            SinglePopulationPSO pso = algorithm as SinglePopulationPSO;

            double distance = 0;
            Vector center = Center.GetCenter(pso.Particles);

            for (int i = 0; i < pso.Particles.Count; i++)
            {
                distance += DistanceMeasure.Measure(center, pso.Particles[i].Position);
            }

            return new Real(distance / pso.SwarmSize);
        }
    }
}