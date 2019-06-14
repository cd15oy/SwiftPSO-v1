/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Factorization;
using SwiftPSO.Core.ControlParameter;
using System;

namespace SwiftPSO.Core.Particle
{
    public class StandardParameterSet
    {
        public IControlParameter InertiaWeight { get; set; }
        public IControlParameter CognitiveAcceleration { get; set; }
        public IControlParameter SocialAcceleration { get; set; }

        public StandardParameterSet() : this(0.729844, 1.49618, 1.49618)
        { }

        public StandardParameterSet(double w, double c1, double c2) : this(new ConstantControlParameter(w), new ConstantControlParameter(c1), new ConstantControlParameter(c2))
        { }

        public StandardParameterSet(IControlParameter w, IControlParameter c1, IControlParameter c2)
        {
            InertiaWeight = w;
            CognitiveAcceleration = c1;
            SocialAcceleration = c2;
        }

        /// <summary>
        /// Determine if the parameter set is theoretically stable.
        /// </summary>
        /// <returns></returns>
        public bool Stable()
        {
            double w = InertiaWeight.Parameter;
            double c1 = CognitiveAcceleration.Parameter;
            double c2 = SocialAcceleration.Parameter;

            if (w < -1 || w > 1) return false;

            //the numerator of the expression
            double num = 4 * (1 - w * w);

            //the numerator and denominator found in the denominator of the expression
            double numD = (c1 * c1 + c2 * c2) * (1 + w);
            double denD = 3 * ((c1 + c2) * (c1 + c2));

            double check = num / (1 - w + (numD / denD));
            //double check = (24 * (1 - w * w)) / (7 - 5 * w);

            //if (c1 < 0) return false; //TODO: do negative values restrict stability?
            //if (c2 < 0) return false;

            return c1 + c2 < check;
        }

        /// <summary>
        /// Calculate the spectral radius of the parameter set.
        /// </summary>
        /// <returns></returns>
        public double SpectralRadius() //TODO: test correctness
        {
            double w = InertiaWeight.Parameter;
            double c1 = CognitiveAcceleration.Parameter;
            double c2 = SocialAcceleration.Parameter;
            double ec1 = c1 / 2;
            double ec2 = c2 / 2;

            double ec12 = c1 * c1 / 3;
            double ec22 = c2 * c2 / 3;

            double el = 1 + w - ec1 - ec2;
            double el2 = 1 + w * w + ec12 + ec22 + 2 * w - 2 * (ec1 + ec2) - 2 * w * (ec1 + ec2) + 2 * ec1 * ec2;
            double elw = w + w * w - w * (ec1 + ec2);

            double[,] vals = {
                {el, -w, 0, 0, 0},
                {1, 0, 0, 0, 0},
                {0, 0, el2, w*w, -2*elw},
                {0, 0, 1, 0, 0},
                {0, 0, el, 0, -w}
            };

            var builder = Matrix<double>.Build;
            Matrix<double> M = builder.DenseOfArray(vals);

            Evd<double> evd = M.Evd();

            double max = double.MinValue;
            foreach (var eigenvalue in evd.EigenValues)
            {
                double val = Math.Abs(eigenvalue.Real);
                if (val > max) max = val;
            }

            return max;
        }
    }
}