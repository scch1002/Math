using MathInterpreter.Arithmetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathInterpreter.Arithmetic
{
    public class Division : Numeric
    {
        private readonly Numeric _leftOperand;
        private readonly Numeric _rightOperand;

        public Division(Numeric leftOperand, Numeric rightOperand)
        {
            _leftOperand = leftOperand;
            _rightOperand = rightOperand;
        }

        public override Symbol Evaluate()
        {
            var leftOperandValue = _leftOperand.Evaluate();
            var rightOperandValue = _rightOperand.Evaluate();

            double leftOperandNumeric = 0;
            double rightOperandNumeric = 0;

            if (double.TryParse(leftOperandValue.ToString(), out leftOperandNumeric) &&
                double.TryParse(rightOperandValue.ToString(), out rightOperandNumeric))
            {
                return new Number((leftOperandNumeric / rightOperandNumeric).ToString());
            }

            return new Number(leftOperandValue + "/" + rightOperandValue);
        }
    }
}
