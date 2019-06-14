/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */

namespace SwiftPSO.Core.Random.Distributions
{
    public interface IDistribution
    {
        double Sample();

        //double Sample(params double[] parameters);
    }
}
