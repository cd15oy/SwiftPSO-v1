/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Problem.Decorator;
using SwiftPSO.Core.Types;
using System;

namespace SwiftPSO.Benchmarks.CEC2005
{
    public class CEC2005Function : IContinuousFunction
    {
        public double Sigma { get; set; }
        public double Weight { get; set; }
        /// <summary>
        /// The scaling factor
        /// </summary>
        public double Lambda { get; set; }
        public double HorizontalShift { get; set; }
        //public double FMax { get; set; }
        /// <summary>
        /// Bias is the vertical shift
        /// </summary>
        public double Bias { get; set; }
        //public bool RandomShift { get; set; }

        public Vector Shifted { get; set; }

        public RotationType RotationType
        {
            get { return _rotationDecorator.RotationType; }
            set { _rotationDecorator.RotationType = value; }
        }

        public int Condition
        {
            get { return _rotationDecorator.Condition; }
            set { _rotationDecorator.Condition = value; }
        }

        private bool _initialized;
        private RotationDecorator _rotationDecorator;
        private Vector _shiftVector;
        private double _fMax;

        public CEC2005Function(IContinuousFunction function)
        {
            _initialized = false;
            _rotationDecorator = new RotationDecorator(function);
            Sigma = 1;
            Lambda = 1;
            HorizontalShift = 0;
            Bias = 0;
            //RandomShift = false; //TODO: implement random shift
        }

        public CEC2005Function(IContinuousFunction function, double sigma, double lambda, RotationType rotationType, double bias, double horizontalShift, int condition = 1)
        {
            _initialized = false;
            _rotationDecorator = new RotationDecorator(function);
            _rotationDecorator.RotationType = rotationType;
            Sigma = sigma;
            Lambda = lambda;
            Bias = bias;
            HorizontalShift = horizontalShift;
            _rotationDecorator.Condition = condition;
        }

        public double Evaluate(Vector input)
        {
            if (!_initialized)
            {
                _fMax = Math.Abs(_rotationDecorator.Evaluate(Vector.Fill(5, input.Length) / Lambda));
                _initialized = true;
            }

            return _rotationDecorator.Evaluate(Shifted / Lambda) / _fMax;
        }


        public void Shift(Vector input)
        {
            if (_shiftVector == null)
            {
                _shiftVector = Vector.Fill(HorizontalShift, input.Length);
            }
            Shifted = input - _shiftVector;
        }

    }
}
