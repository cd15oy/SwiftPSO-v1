/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.Algorithm;
using SwiftPSO.Core.Fitness;
using SwiftPSO.Core.Particle.Behaviour;
using SwiftPSO.Core.Particle.PersonalBestUpdate;
using SwiftPSO.Core.Particle.PositionInitialization;
using SwiftPSO.Core.Particle.VelocityInitialization;
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Core.Particle
{
    public abstract class AbstractParticle : IParticle
    {
        public Vector Position { get; set; }

        public Vector Velocity { get; set; }

        private IFitness _fitness;
        public IFitness Fitness
        { //TODO: examine if this works without a clone
            get => _fitness;
            set
            {
                PreviousFitness = _fitness;
                _fitness = value;
            }
        }

        public Vector BestPosition { get; protected set; }
        public IFitness BestFitness { get; set; }
        public IParticle NeighbourhoodBest { get; set; }
        public IPersonalBestUpdate PersonalBestUpdate { get; set; }
        public int StagnationCounter { get; set; }
        protected IPositionInitialization PositionInitialization { get; set; }
        protected IVelocityInitialization VelocityInitialization { get; set; }
        public Vector PreviousPosition { get; protected set; }
        protected IFitness PreviousFitness { get; private set; }
        public IBehaviour Behaviour { get; set; }

        protected AbstractParticle()
        {
            PositionInitialization = new RandomPositionInitialization();
            VelocityInitialization = new ConstantVelocityInitialization(0);
            PersonalBestUpdate = new BoundedPersonalBestUpdate();
            Behaviour = new StandardBehaviour();
        }

        public virtual void Initialize(IProblem problem)
        {
            Position = new Vector(problem.Dimensions);
            Velocity = new Vector(problem.Dimensions);
            Fitness = InferiorFitness.GetInstance();
            BestFitness = InferiorFitness.GetInstance();

            PositionInitialization.Initialize(this, problem);
            BestPosition = Position.Clone(); //TODO: alternate PBestInitialization?
            VelocityInitialization.Initialize(this, problem);

            PreviousPosition = Position.Clone();
            StagnationCounter = 0;
            NeighbourhoodBest = this;
        }

        public virtual void CalculateFitness(IProblem problem)
        {
            Fitness = problem.Evaluate(Position);
            PersonalBestUpdate.UpdatePersonalBest(this, problem);
        }

        public virtual void PerformIteration(IAlgorithm algorithm)
        {
            Behaviour.PerformIteration(this, algorithm.Problem);
        }

        public void UpdatePreviousPosition()
        {
            for (int i = 0; i < Position.Length; i++)
            {
                PreviousPosition[i] = Position[i];
            }
        }

        public void FlagBestPosition()
        {
            for (int i = 0; i < Position.Length; i++)
            {
                BestPosition[i] = Position[i];
            }
        }
    }
}