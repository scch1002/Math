using MathInterpreter.Arithmetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathInterpreter.LinearAlgebra
{
    public class Matrix : LinearSystem
    {
        private readonly Numeric[][] _matrix;

        public Matrix(Numeric[][] matrix)
        {
            _matrix = matrix;
        }
                
        public override Symbol Evaluate()
        {
            var result = InitilizeMatrix(_matrix.GetLength(0), _matrix.First().GetLength(0));

            for (int row = 0; row < _matrix.GetLength(0); row++)
            {
                for(int column = 0; column < _matrix[row].GetLength(0); column++)
                {
                    result[row][column] = (Numeric)_matrix[row][column].Evaluate();
                }
            }

            return new Matrix(result);
        }

        private static Numeric[][] InitilizeMatrix(int rows, int columns)
        {
            var matrix = new Numeric[rows][];
            for (int row = 0; row < rows; row++)
            {
                matrix[row] = new Numeric[columns];
            }
            return matrix;
        }
    }
}
