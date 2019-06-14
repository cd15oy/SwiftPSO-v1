/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Measurement;
using SwiftPSO.Core.Types;
using System;
using System.Collections.Generic;

namespace SwiftPSO.Simulator
{
    public class MeasurementSuite
    {
        public IReadOnlyDictionary<string, IMeasurement<IMeasurable>> Measurements => _measurements as IReadOnlyDictionary<string, IMeasurement<IMeasurable>>;
        public IMeasurement<IMeasurable> this[string name] => _measurements[name];
        public int Count => _measurements.Count;

        /// <summary>
        /// Measurements are only recorded every <see cref="Resolution"/> iterations.
        /// </summary>
        public int Resolution { get; set; }

        private Dictionary<string, IMeasurement<IMeasurable>> _measurements;

        public MeasurementSuite() : this(1)
        { }

        public MeasurementSuite(int resolution)
        {
            _measurements = new Dictionary<string, IMeasurement<IMeasurable>>();
            Resolution = resolution;
        }

        public void Add(IMeasurement<IMeasurable> measurement, string name)
        {
            _measurements.Add(name, measurement);
        }

        public bool Remove(string name)
        {
            return _measurements.Remove(name);
        }

        /// <summary>
        /// Return the concrete type of <see cref="INumeric"/> for a measurment by name.
        /// </summary>
        /// <param name="name">The name of the measurement.</param>
        /// <returns></returns>
        public Type GetDataType(string name)
        {
            return _measurements[name].GetType().GetInterfaces()[0].GenericTypeArguments[0];
        }
    }
}
