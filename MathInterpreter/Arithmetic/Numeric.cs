using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathInterpreter.Arithmetic
{
    public abstract class Numeric : Symbol
    {
        public abstract override Symbol Evaluate();
    }
}
