/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;

namespace SwiftPSO.Core.ControlParameter
{
    public class LinearlyVaryingControlParameter : IControlParameter
    {
        public double InitialValue { get; set; }
        public double FinalValue { get; set; }

        public LinearlyVaryingControlParameter(double intialValue, double finalValue)
        {
            InitialValue = intialValue;
            FinalValue = finalValue;
        }

        public double Parameter => InitialValue + (FinalValue - InitialValue) * AbstractAlgorithm.CurrentInstance.CalculateCompletion();

        public override string ToString()
        {
            return Parameter.ToString();
        }
    }
}
