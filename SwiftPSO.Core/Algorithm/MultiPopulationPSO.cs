/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using System.Collections.Generic;

namespace SwiftPSO.Core.Algorithm
{
    public abstract class MultiPopulationPSO : AbstractAlgorithm
    {
        private List<SinglePopulationPSO> _populations;
        public IReadOnlyCollection<SinglePopulationPSO> Populations => _populations.AsReadOnly();

        public MultiPopulationPSO() : base()
        {
            _populations = new List<SinglePopulationPSO>();
        }

        public void SetAlgorithms(List<SinglePopulationPSO> populations)
        {
            _populations = populations;
        }

        public void AddAlgorithm(SinglePopulationPSO pso)
        {
            _populations.Add(pso);
        }

        public bool RemoveAlgorithm(SinglePopulationPSO pso)
        {
            return _populations.Remove(pso);
        }
    }
}
