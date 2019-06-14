/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Core.Particle.VelocityInitialization
{
    public class ConstantVelocityInitialization : IVelocityInitialization
    {
        private readonly double _value;

        public ConstantVelocityInitialization(double value)
        {
            _value = value;
        }

        public void Initialize(IParticle particle, IProblem problem)
        {
            particle.Velocity = Vector.Fill(_value, problem.Dimensions);
        }
    }
}