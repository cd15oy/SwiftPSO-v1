/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Types;
using SwiftPSO.Core.Utility;
using System;

namespace SwiftPSO.Core.Problem.Decorator
{
    public class RotationDecorator : IContinuousFunction
    {
        public IContinuousFunction Function { get; set; }

        private RotationType _rotationType;
        public RotationType RotationType
        {
            get
            {
                return _rotationType;
            }
            set //ensure that the matrix is not considered initialized if we switch the type.
            {
                if (value != _rotationType) _initialized = false;
                _rotationType = value;
            }
        }

        public Matrix/*<double>*/ RotationMatrix { get; private set; }
        public int Condition { get; set; }

        private bool _initialized;

        public RotationDecorator(IContinuousFunction function)
        {
            Function = function;
            RotationMatrix = null;
            RotationType = RotationType.Orthonormal;
            Condition = 1;
            _initialized = false;
        }

        public double Evaluate(Vector input)
        {
            if (!_initialized || input.Length != RotationMatrix.Rows)
            {
                InitializeMatrix(input.Length);
            }

            Vector rotated = new Vector(input.Length); //Vector.Fill(0, input.Length);
            //performs left multiplication of vector * Matrix -> xA
            for (int j = 0; j < input.Length; j++)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    rotated[j] += input[i] * RotationMatrix[i, j];
                }
            }

            return Function.Evaluate(rotated);
            /*Vector<double> input2 = Vector<double>.Build.DenseOfEnumerable(input);
            Vector<double> rotated2 = input2 * RotationMatrix;
            return Function.Evaluate(new Vector(rotated2.AsArray()));*/

        }

        private void InitializeMatrix(int size)
        {
            switch (RotationType)
            {
                case RotationType.Identity:
                    RotationMatrix = MatrixGenerator.Identity(size);
                    break;
                case RotationType.Orthonormal:
                    RotationMatrix = MatrixGenerator.RandomOrthogonal(size);
                    break;
                case RotationType.LinearTransform:
                    RotationMatrix = MatrixGenerator.RandomLinearTransformation(size, Condition);
                    break;
                default:
                    throw new ArgumentException("Unknown matrix type.");
            }

            _initialized = true;
        }
    }

    public enum RotationType
    {
        Identity,
        Orthonormal,
        LinearTransform
    }
}
