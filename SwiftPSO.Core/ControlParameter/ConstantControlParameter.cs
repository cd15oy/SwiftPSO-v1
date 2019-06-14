/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
namespace SwiftPSO.Core.ControlParameter
{
    public class ConstantControlParameter : IControlParameter
    {
        public double Parameter { get; set; }

        public ConstantControlParameter() : this(0)
        { }

        public ConstantControlParameter(double parameter)
        {
            Parameter = parameter;
        }

        public override string ToString()
        {
            return Parameter.ToString();
        }
    }
}