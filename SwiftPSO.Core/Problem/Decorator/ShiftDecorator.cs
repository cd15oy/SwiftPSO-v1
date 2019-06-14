/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using SwiftPSO.Core.ControlParameter;
using SwiftPSO.Core.Types;

namespace SwiftPSO.Core.Problem.Decorator
{

    public class ShiftDecorator : IContinuousFunction
    {
        public IContinuousFunction Function { get; set; }
        public IControlParameter HorizontalShift { get; set; }
        public IControlParameter VerticalShift { get; set; }

        public ShiftDecorator(IContinuousFunction function) : this(function, new ConstantControlParameter(0), new ConstantControlParameter(0))
        { }

        public ShiftDecorator(IContinuousFunction function, IControlParameter horizontalShift, IControlParameter verticalShift)
        {
            Function = function;
            HorizontalShift = horizontalShift;
            VerticalShift = verticalShift;
        }

        public double Evaluate(Vector input)
        {
            return Function.Evaluate(input - HorizontalShift.Parameter) + VerticalShift.Parameter; //TODO: why is horizontal shift subtraction?
        }
    }
}
