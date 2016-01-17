using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathInterpreter.Arithmetic
{
    public class Number : Numeric
    {
        private readonly string _value;

        public Number(string value)
        {
            _value = value;
        }

        public override Symbol Evaluate()
        {
            return this;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}
