/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Types;

namespace SwiftPSO.Core.Utility.Distance
{
    public interface IDistance
    {
        double Measure(Vector x, Vector y);

        //double Measure(IEnumerable<Numeric> x, IEnumerable<Numeric> y);
    }
}