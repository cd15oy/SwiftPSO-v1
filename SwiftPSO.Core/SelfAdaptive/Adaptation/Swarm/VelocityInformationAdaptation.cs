/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;
using SwiftPSO.Core.ControlParameter;
using SwiftPSO.Core.Particle;
using SwiftPSO.Core.Types;
using System;

namespace SwiftPSO.Core.SelfAdaptive.Adaptation.Swarm
{
    /// <summary>
    /// This strategy adapts the inertia weight control parameter.
    /// </summary>
    public class VelocityInformationAdaptation : SwarmAdaptation
    {
        /// <summary>
        /// The change in inertia weight.
        /// </summary>
        public double Delta { get; set; }
        /// <summary>
        /// The minimum inertia weight
        /// </summary>
        public double Minimum { get; set; }
        /// <summary>
        /// The maximum inertia weight.
        /// </summary>
        public double Maximum { get; set; }

        private double _initialVelocity;

        public VelocityInformationAdaptation() : this(0.1, 0.3, 0.9)
        { }

        public VelocityInformationAdaptation(double delta, double min, double max)
        {
            Delta = delta;
            Minimum = min;
            Maximum = max;
        }

        public override void Initialization(PSO algorithm)
        {
            base.Initialization(algorithm);
            Domain domain = algorithm.Problem.ProblemDomain;
            _initialVelocity = domain[0].Range / 2; //TODO: this assumes bounds are always equal for all dimensions.
        }

        public override void Adapt(PSO algorithm)
        {
            double avgVelocity = AverageAbsoluteVelocity(algorithm);
            double idealVelocity = IdealVelocity(algorithm);

            for (int i = 0; i < algorithm.SwarmSize; i++)
            {
                StandardParticle particle = algorithm.Particles[i] as StandardParticle;
                double newInertia;
                if (avgVelocity >= idealVelocity)
                {
                    newInertia = Math.Max(particle.Parameters.InertiaWeight.Parameter - Delta, Minimum);
                }
                else
                {
                    newInertia = Math.Min(particle.Parameters.InertiaWeight.Parameter + Delta, Maximum);
                }

                particle.Parameters.InertiaWeight = new ConstantControlParameter(newInertia);
            }
        }

        private double AverageAbsoluteVelocity(PSO algorithm)
        {
            double sum = 0;
            //find the average absolute velocity
            for (int i = 0; i < algorithm.SwarmSize; i++)
            {
                Vector velocity = (Vector)algorithm.Particles[i].Velocity;

                //sum the elements of this particles velocity.
                for (int j = 0; j < velocity.Length; j++)
                {
                    sum += Math.Abs(velocity[j]);
                }
            }

            return sum / (algorithm.SwarmSize * algorithm.Problem.Dimensions);
        }

        private double IdealVelocity(PSO algorithm)
        {
            double factor = algorithm.CalculateCompletion() / 0.95;

            return _initialVelocity * ((1 + Math.Cos(factor * Math.PI)) / 2);
        }
    }
}
