using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math
{
    public class Matrix
    {
        private static double[][] InitilizeMatrix(int rows, int columns)
        {
            var matrix = new double[rows][];
            for (int row = 0; row < rows; row++)
            {
                matrix[row] = new double[columns];
            }
            return matrix;
        }

        private static double[][] CopyMatrix(double[][] matrix)
        {
            var result = InitilizeMatrix(matrix.GetLength(0), matrix.First().GetLength(0));
            for (int copyRow = 0; copyRow < matrix.GetLength(0); copyRow++)
            {
                for (int copyColumn = 0; copyColumn < matrix[copyRow].GetLength(0); copyColumn++)
                {
                    result[copyRow][copyColumn] = matrix[copyRow][copyColumn];
                }
            }
            return result;
        }

        private static void RowReplacement(double[][] matrix, int rowNew, int rowWith, double scaler)
        {
            for(int column = 0; column < matrix[rowWith].GetLength(0); column++)
            {
                matrix[rowNew][column] += (matrix[rowWith][column] * scaler);
            }
        }

        private static void RowInterchange(double[][] matrix, int rowFrom, int rowTo)
        {
            double[] intermediate;
            intermediate = matrix[rowTo];
            matrix[rowTo] = matrix[rowFrom];
            matrix[rowFrom] = intermediate;
        }

        private static void RowScale(double[][] matrix, int row, double scaler)
        {
            for (int column = 0; column < matrix.First().GetLength(0); column++)
            {
                matrix[row][column] *= scaler;
            }
        }

        private static void RowToEchelonForm(double[][] matrix, int effectColumn, int startRow)
        { 
            if (matrix[startRow][effectColumn] == 0)
            {
                for(int row = startRow; row < matrix.GetLength(0); row++)
                {
                    if (matrix[row][effectColumn] != 0)
                    {
                        RowInterchange(matrix, row, startRow);
                        break;
                    }
                }
            }

            for(int row = startRow + 1; row < matrix.GetLength(0); row++)
            {
                if (matrix[row][effectColumn] != 0)
                {
                    RowReplacement(matrix, row, startRow, - matrix[row][effectColumn] / matrix[startRow][effectColumn]);
                }
            }
        }

        private static void RowToReducedEchelonForm(double[][] matrix, int effectColumn, int startRow)
        {
            RowToEchelonForm(matrix, effectColumn, startRow);

            for (int row = startRow - 1; row > -1; row--)
            {
                if (matrix[row][effectColumn] != 0)
                {
                    RowReplacement(matrix, row, startRow, -matrix[row][effectColumn] / matrix[startRow][effectColumn]);
                }
            }
        }

        private static void InterchangeZerosToBottom(double[][] matrix, int currentRow)
        {
            var subMatrix = new double[matrix.GetLength(0) - currentRow][];
            for(int row = 0; row < matrix.GetLength(0) - currentRow; row++)
            {
                subMatrix[row] = matrix[row + currentRow];
            }

            Array.Sort(subMatrix, MoreLeadingZerosThan);

            for (int row = 0; row < matrix.GetLength(0) - currentRow; row++)
            {
                matrix[row + currentRow] = subMatrix[row];
            }
        }

        private static int MoreLeadingZerosThan(double[] first, double[] second)
        {
            int firstCount = 0;
            int secondCount = 0;
            foreach(var value in first)
            {
                if (value != 0)
                {
                    break;
                }
                firstCount++;
            }
            foreach (var value in second)
            {
                if (value != 0)
                {
                    break;
                }
                secondCount++;
            }
            return firstCount < secondCount
                ? -1
                : firstCount == secondCount
                ? 0
                : 1;
        }

        //ref(A)
        public static double[][] EchelonForm(double[][] matrix)
        {
            var rows = matrix.GetLength(0);
            var columns = matrix.First().GetLength(0);

            var result = CopyMatrix(matrix);

            int pivotColumn;
            for(pivotColumn = 0; pivotColumn < rows; pivotColumn++)
            {
                RowToEchelonForm(result, pivotColumn, pivotColumn);
            }

            int zeroCount = 0;
            while(result[rows - 2][zeroCount] == 0)
            {
                zeroCount++;
            }

            if (zeroCount == pivotColumn - 1)
            {
                RowScale(result, pivotColumn - 1, matrix[pivotColumn - 2][pivotColumn - 1] / matrix[pivotColumn - 1][pivotColumn - 1]);
                RowReplacement(result, pivotColumn - 1, pivotColumn - 1, -1);
            }

            return result;
        }

        //rref(A)
        public static double[][] ReducedEchelonForm(double[][] matrix)
        {
            var rows = matrix.GetLength(0);
            var columns = matrix.First().GetLength(0);

            var result = CopyMatrix(matrix);

            int column = 0;
            for (int row = 0; row < rows; row++)
            {
                RowToReducedEchelonForm(result, column, row);
                column++;
            }

            column = 0;
            for (int row = 0; row < rows; row++)
            {
                RowScale(result, row, 1.0 / result[row][column]);
                column++;
            }

            return result;
        }

        //Ax=b
        public static double[] LinearTranformation(double[][] matrix, double[] vector)
        {
            var result = new double[vector.GetLength(0)];
            for(int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int column = 0; column < matrix[row].GetLength(0); column++)
                {
                    result[row] += matrix[row][column] * vector[column];
                }
            }
            return result;
        }

        //AB=C
        public static double[][] MatrixMultiply(double[][] matrixA, double[][] matrixB)
        {
            var rows = matrixA.GetLength(0);
            var columns = matrixB.First().GetLength(0);

            var matrix_Result = InitilizeMatrix(rows, columns);

            for (var row = 0; row < rows; row++)
            {
                var vector = new double[columns];
                for(int column = 0; column < columns; column++)
                {
                    vector[column] = matrixB[column][row];
                }
                var result = LinearTranformation(matrixA, vector);
                for (int column = 0; column < columns; column++)
                {
                    matrix_Result[column][row] = result[column];
                }
            }
            return matrix_Result;
        }

        //A^T
        public static double[][] MatrixTranspose(double[][] matrix)
        {
            return null;
        }
        
        //Linear Independent
        public static bool IsLinearlyIndependent(double[][] matrix)
        {
            // columns > rows
            if (matrix.First().GetLength(0) > matrix.GetLength(0))
            {
                return false;
            }

            // vector 0 present
            bool containsZeroVector = true;
            for (int column = 0; column < matrix.First().GetLength(0); column++)
            {
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    if (matrix[row][column] != 0)
                    {
                        containsZeroVector = false;
                        continue;
                    }
                }
                if (containsZeroVector)
                {
                    return false;
                }
                containsZeroVector = true;
            }

            // ref() check
            var result = CopyMatrix(matrix);

            var echelonForm = EchelonForm(result);

            var rowAllZeros = true;
            for(int row = 0; row < echelonForm.GetLength(0); row++)
            {
                for(int column = 0; column < echelonForm.First().GetLength(0); column++)
                {
                    if (echelonForm[row][column] != 0)
                    {
                        rowAllZeros = false;
                    }
                }
                if (rowAllZeros)
                {
                    return false;
                }
                rowAllZeros = true;
            }
            
            return true;
        }
    }
}
