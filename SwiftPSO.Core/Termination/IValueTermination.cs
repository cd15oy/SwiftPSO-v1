/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
namespace SwiftPSO.Core.Termination
{
    public interface IValueTermination
    {
        bool Terminate(double value, double target);

        double CalculateCompletion(double value, double target);
    }
}