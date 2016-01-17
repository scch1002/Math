using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathInterpreter.LinearAlgebra
{
    public abstract class LinearSystem : Symbol
    {
        public abstract override Symbol Evaluate();
    }
}
