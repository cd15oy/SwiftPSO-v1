/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Particle;
using SwiftPSO.Core.Types;
using System.Collections.Generic;

namespace SwiftPSO.Core.Utility.Center
{
    public interface ICenter
    {
        Vector GetCenter(List<IParticle> particles);
    }
}