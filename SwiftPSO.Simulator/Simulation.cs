/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;
using SwiftPSO.Core.Measurement;
using System;
using System.Data;

namespace SwiftPSO.Simulator
{
    /// <summary>
    /// Represents a simulation that can be executed by the Executor.
    /// </summary>
    public class Simulation
    {
        /// <summary>
        /// The algorithm to execute.
        /// </summary>
        public IAlgorithm Algorithm { get; private set; }

        /// <summary>
        /// A collection of measurements to record.
        /// </summary>
        public MeasurementSuite MeasurementSuite { get; private set; }

        /// <summary>
        /// A table of measurement information that is populated when the algorithm executes.
        /// </summary>
        public DataTable MeasureTable { get; private set; }

        /// <summary>
        /// Create a new simulation consisting of an algorithm and a list of measurements.
        /// </summary>
        /// <param name="algorithm">The algorithm to execute.</param>
        /// <param name="measurements">A collection of measurements to record on the provided algorithm.</param>
        public Simulation(IAlgorithm algorithm, MeasurementSuite measurements)
        {
            Algorithm = algorithm;
            MeasurementSuite = measurements;
        }

        /// <summary>
        /// Create a new simulation given a function producing an algorithm and list of measurements.
        /// </summary>
        /// <param name="algorithmGenerator">A function that produces an <see cref="IAlgorithm"/></param>
        /// <param name="measurements">A List of <see cref="IMeasurement{T}"/> to be recorded.</param>
        /// <returns></returns>
        public static Simulation Create(Func<IAlgorithm> algorithmGenerator, MeasurementSuite measurements)
        {
            return new Simulation(algorithmGenerator(), measurements);
        }

        /// <summary>
        /// Create the table of measurements and execute the algorithm.
        /// </summary>
        /// <param name="recordInitialMeasurements">A value indicating whether measurements should be recorded after initialization, but before the first iteration.</param>
        public void Run(bool recordInitialMeasurements = true)
        {
            MeasureTable = new DataTable("Measurements");

            DataColumn column = new DataColumn()
            {
                ColumnName = "Iteration",
                DataType = typeof(int)
            };
            MeasureTable.Columns.Add(column);

            foreach (var kvp in MeasurementSuite.Measurements)
            {
                //TODO: a bit hacky, but it works
                column = new DataColumn
                {
                    ColumnName = kvp.Key,
                    DataType = MeasurementSuite.GetDataType(kvp.Key)
                };

                MeasureTable.Columns.Add(column);
            }

            if (recordInitialMeasurements) Algorithm.InitializationComplete += RecordMeasurements;
            Algorithm.IterationComplete += RecordMeasurements;
            Algorithm.PerformInitialization();
            Algorithm.Run();
        }

        /// <summary>
        /// An event handler that captures each measurement during the algorithm's execution.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecordMeasurements(object sender, IAlgorithm algorithm)
        {
            if (algorithm.Iteration % MeasurementSuite.Resolution != 0) return;

            //TODO: what if final iteration is not a multiple of resolution? Always print final iteration - omit if last row matches?
            DataRow row = MeasureTable.NewRow();
            row[0] = algorithm.Iteration; //first column is always the iteration

            int index = 1; //start index at 1 as column 0 is the iteration

            foreach (var measurement in MeasurementSuite.Measurements.Values)
            {
                row[index] = measurement.GetValue(algorithm);
                index++;
            }

            MeasureTable.Rows.Add(row);
        }
    }
}