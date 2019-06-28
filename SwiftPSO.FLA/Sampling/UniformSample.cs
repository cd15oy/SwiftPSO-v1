/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Types;

namespace SwiftPSO.FLA.Sampling
{
    public class UniformSample
    {
        public Vector[] Sample(int samples, Domain domain)
        {
            Vector[] result = new Vector[samples];
            for (int i = 0; i < samples; i++)
            {
                Vector position = new Vector(domain.Dimension);

                position.Randomize(domain.DimensionBounds);

                result[i] = position.Clone(); //TODO: is clone necessary?
            }

            return result;
        }
    }
}
