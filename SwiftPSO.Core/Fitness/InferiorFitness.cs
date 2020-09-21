/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */

namespace SwiftPSO.Core.Fitness
{
    public class InferiorFitness : IFitness
    {
        private InferiorFitness()
        {
            //do nothing, only exists to hide constructor
        }

        private static readonly InferiorFitness Instance = new InferiorFitness();

        public double Value => double.NaN;

        public int CompareTo(IFitness other)
        {
            return this == other ? 0 : -1;
        }

        public static IFitness GetInstance()
        {
            return Instance;
        }

        public IFitness Clone()
        {
            return Instance;
        }

        public IFitness GetNew(double value) {
            return Instance;
        }

    }
}