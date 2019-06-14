/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */

namespace SwiftPSO.Core.Types
{
    public class Integer : INumeric
    {
        public int IntValue { get; set; }

        public double DoubleValue => IntValue;

        public Integer(int value)
        {
            IntValue = value;
        }

        public int CompareTo(INumeric other)
        {
            return IntValue.CompareTo(other.IntValue);
        }

    }
}