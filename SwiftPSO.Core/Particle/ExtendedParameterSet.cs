/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.ControlParameter;
using SwiftPSO.Core.Random;

namespace SwiftPSO.Core.Particle
{
    public class ExtendedParameterSet
    {
        public IControlParameter InertiaWeight { get; set; }
        public IControlParameter CognitiveAcceleration { get; set; }
        public IControlParameter SocialAcceleration { get; set; }
        public IControlParameter ExtendedAcceleration { get; set; }
        public double Lambda { get; set; }

        public ExtendedParameterSet() : this(0.729844, 1.49618, 1.49618, 1.49618, RandomProvider.NextDouble())
        { }

        public ExtendedParameterSet(double w, double c1, double c2, double c3, double lambda) : this(new ConstantControlParameter(w), new ConstantControlParameter(c1),
            new ConstantControlParameter(c2), new ConstantControlParameter(c3), lambda)
        { }

        public ExtendedParameterSet(IControlParameter w, IControlParameter c1, IControlParameter c2, IControlParameter c3, double lambda)
        {
            InertiaWeight = w;
            CognitiveAcceleration = c1;
            SocialAcceleration = c2;
            ExtendedAcceleration = c3;
            Lambda = lambda;
        }
    }
}
