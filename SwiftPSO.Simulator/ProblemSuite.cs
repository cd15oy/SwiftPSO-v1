/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.ControlParameter;
using SwiftPSO.Core.Problem;
using SwiftPSO.Core.Problem.Decorator;
using SwiftPSO.Core.Types;
using System;
using System.Collections.Generic;

namespace SwiftPSO.Simulator
{
    public class ProblemSuite
    {
        public IReadOnlyDictionary<string, Func<IProblem>> Problems => _problems as IReadOnlyDictionary<string, Func<IProblem>>;
        public Func<IProblem> this[string name] => _problems[name];
        public int Count => _problems.Count;

        private Dictionary<string, Func<IProblem>> _problems;

        public ProblemSuite()
        {
            _problems = new Dictionary<string, Func<IProblem>>();
        }

        public void AddFunction(Func<IContinuousFunction> function, string domain, string name)
        {
            AddFunction(function, new Domain(domain), name);
        }

        //TODO: must ensure that call to the Func creates its own instance of the problem
        public void AddFunction(Func<IContinuousFunction> function, Domain domain, string name)
        {
            _problems.Add(name, () => new FunctionOptimizationProblem()
            {
                Function = function(),
                ProblemDomain = domain
            });
        }

        public void AddRotatedFunction(Func<IContinuousFunction> function, RotationType rotationType, Domain domain, string name)
        {
            RotationDecorator rotated() => new RotationDecorator(function())
            {
                RotationType = rotationType
            };

            AddFunction(rotated, domain, name);
        }

        public void AddShiftedFunction(Func<IContinuousFunction> function, IControlParameter horizontalShift, IControlParameter verticalShift,
            Domain domain, string name)
        {
            ShiftDecorator shifted() => new ShiftDecorator(function(), horizontalShift, verticalShift);

            AddFunction(shifted, domain, name);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="function"></param>
        /// <param name="horizontalShift"></param>
        /// <param name="verticalShift"></param>
        /// <param name="rotationType"></param>
        /// <param name="condition">The condition number to be used for linear-transformation rotation. If any other type of rotation is used, condition should be set to 1.</param>
        /// <param name="domain"></param>
        /// <param name="name"></param>
        public void AddShiftedRotatedFunction(Func<IContinuousFunction> function, IControlParameter horizontalShift, IControlParameter verticalShift,
            RotationType rotationType, int condition, Domain domain, string name)
        {
            RotationDecorator rotated() => new RotationDecorator(function())
            {
                RotationType = rotationType,
                Condition = condition
            };

            ShiftDecorator shifted() => new ShiftDecorator(rotated(), horizontalShift, verticalShift);

            AddFunction(shifted, domain, name);
        }

        public bool Remove(string name)
        {
            return _problems.Remove(name);
        }

    }
}
