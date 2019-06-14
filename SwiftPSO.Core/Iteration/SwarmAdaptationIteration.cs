/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;
using SwiftPSO.Core.SelfAdaptive.Adaptation.Swarm;
using SwiftPSO.Core.SelfAdaptive.Detection.Swarm;

namespace SwiftPSO.Core.Iteration
{
    public class SwarmAdaptationIteration : IIteration<PSO>
    {
        public ISwarmDetection DetectionStrategy { get; set; }
        public SwarmAdaptation AdaptationStrategy { get; set; }
        public IIteration<PSO> Delegate { get; set; }

        public SwarmAdaptationIteration()
        {
            AdaptationStrategy = new DoNothingAdaptation();
            DetectionStrategy = new AlwaysTrueDetection();
            Delegate = new SynchronousIteration();
        }

        public void PerformIteration(PSO algorithm)
        {
            //perform initialization of the adaptation strategy on first iteration
            if (algorithm.Iteration == 0)
            {
                AdaptationStrategy.Initialization(algorithm);
            }

            //perform any pre-adaptation steps
            AdaptationStrategy.PreAdaptation(algorithm);

            //run the adaptation strategy, if the detection strategy returns true
            if (DetectionStrategy.Detect(algorithm))
            {
                AdaptationStrategy.Adapt(algorithm);
            }

            //perform the iteration using the delegate strategy
            Delegate.PerformIteration(algorithm);

            //perform any post-iteration steps
            AdaptationStrategy.PostIteration(algorithm);
        }
    }
}
