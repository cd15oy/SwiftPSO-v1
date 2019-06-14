/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using System;

namespace SwiftPSO.Core.Types
{
    public interface INumeric : IComparable<INumeric>, IMeasurable
    {
        int IntValue { get; }
        double DoubleValue { get; }
    }
}