/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;
using SwiftPSO.Core.Measurement;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Core.Termination
{
    public class MeasurementTermination : ITerminationCriterion
    {
        public IValueTermination Criterion { get; set; }
        public IMeasurement<INumeric> Measurement { get; set; }
        public double Target { get; set; }

        public MeasurementTermination() : this(new Iterations(), new MaximumValue(), 5000)
        { }

        public MeasurementTermination(IMeasurement<INumeric> measurement, IValueTermination criterion, double target)
        {
            Measurement = measurement;
            Criterion = criterion;
            Target = target;
        }

        public bool Terminate(IAlgorithm algorithm)
        {
            return Criterion.Terminate(Measurement.GetValue(algorithm).DoubleValue, Target);
        }

        public double CalculateCompletion(IAlgorithm algorithm)
        {
            return Criterion.CalculateCompletion(Measurement.GetValue(algorithm).DoubleValue, Target);
        }
    }
}