using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math
{
    public class Matrix
    {
        private static void RowReplacement(double[,] matrix, int rowNew, int rowWith, double scaler)
        {
            for(int column = 0; column < matrix.GetLength(1); column++)
            {
                matrix[rowNew, column] += (matrix[rowWith, column] * scaler);
            }
        }

        private static void RowInterchange(double[,] matrix, int rowFrom, int rowTo)
        {
            double intermediate = 0;
            for(int column = 0; column < matrix.GetLength(1); column++)
            {
                intermediate = matrix[rowTo, column];
                matrix[rowTo, column] = matrix[rowFrom, column];
                matrix[rowFrom, column] = intermediate;
            }
        }

        private static void RowScale(double[,] matrix, int row, double scaler)
        {
            for (int column = 0; column < matrix.GetLength(1); column++)
            {
                matrix[row, column] *= scaler;
            }
        }

        private static void RowToEchelonForm(double[,] matrix, int effectColumn, int startRow)
        { 
            if (matrix[startRow, effectColumn] == 0)
            {
                for(int row = startRow; row < matrix.GetLength(0); row++)
                {
                    if (matrix[row, effectColumn] != 0)
                    {
                        RowInterchange(matrix, row, startRow);
                        break;
                    }
                }
            }

            for(int row = startRow + 1; row < matrix.GetLength(0); row++)
            {
                if (matrix[row, effectColumn] != 0)
                {
                    RowScale(matrix, row, 1.0/matrix[row, effectColumn]);
                    RowScale(matrix, row, matrix[startRow, effectColumn]);
                    RowReplacement(matrix, row, startRow, -1);
                }
            }
        }

        private static void RowToReducedEchelonForm(double[,] matrix, int effectColumn, int startRow)
        {
            RowToEchelonForm(matrix, effectColumn, startRow);

            for (int row = startRow - 1; row > -1; row--)
            {
                if (matrix[row, effectColumn] != 0)
                {
                    RowScale(matrix, row, 1.0 / matrix[row, effectColumn]);
                    RowScale(matrix, row, matrix[startRow, effectColumn]);
                    RowReplacement(matrix, row, startRow, -1);
                }
            }
        }

        //ref(A)
        public static double[,] EchelonForm(double[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var columns = matrix.GetLength(1);

            var result = new double[rows, columns];

            for(int copyRow = 0; copyRow < rows; copyRow++)
            {
                for(int copyColumn = 0; copyColumn < columns; copyColumn++)
                {
                    result[copyRow, copyColumn] = matrix[copyRow, copyColumn];
                }
            }

            int column = 0;
            for(int row = 0; row < rows; row++)
            {
                RowToEchelonForm(result, column, row);
                column++;
            }
            
            return result;
        }

        //rref(A)
        public static double[,] ReducedEchelonForm(double[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var columns = matrix.GetLength(1);

            var result = new double[rows, columns];

            for (int copyRow = 0; copyRow < rows; copyRow++)
            {
                for (int copyColumn = 0; copyColumn < columns; copyColumn++)
                {
                    result[copyRow, copyColumn] = matrix[copyRow, copyColumn];
                }
            }

            int column = 0;
            for (int row = 0; row < rows; row++)
            {
                RowToReducedEchelonForm(result, column, row);
                column++;
            }

            column = 0;
            for (int row = 0; row < rows; row++)
            {
                RowScale(result, row, 1.0 / result[row, column]);
                column++;
            }

            return result;
        }

        //Ax=b
        public static double[] LinearTranformation(double[,] matrix, double[] vector)
        {
            var result = new double[vector.GetLength(0)];
            for(int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int column = 0; column < matrix.GetLength(1); column++)
                {
                    result[row] += matrix[row, column] * vector[column];
                }
            }
            return result;
        }

        //AB=C
        public static double[,] MatrixMultiply(double[,] matrixA, double[,] matrixB)
        {
            var rows = matrixA.GetLength(0);
            var columns = matrixB.GetLength(1);

            double[,] matrix_Result = new double[rows, columns];

            for(var row = 0; row < rows; row++)
            {
                var vector = new double[columns];
                for(int column = 0; column < columns; column++)
                {
                    vector[column] = matrixB[column, row];
                }
                var result = LinearTranformation(matrixA, vector);
                for (int column = 0; column < columns; column++)
                {
                    matrix_Result[column, row] = result[column];
                }
            }
            return matrix_Result;
        }
    }
}
