using System;
using Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTests
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void Matrix_EchelonForm()
        {
            var matrix = new double[3][]
            {
                new double[] {3, 5, 9 },
                new double[] {8, 1, 5 },
                new double[] {9, 2, 9 }
            };

            var result = Matrix.EchelonForm(matrix);

            Assert.AreEqual(0, result[1][0]);
            Assert.AreEqual(0, result[2][0]);
            Assert.AreEqual(0, result[2][1]);

            var matrixDependent = new double[3][]
            {
                new double[] { 3, 9, 2 },
                new double[] { 3, 9, 8 },
                new double[] { 3, 9, 6 }
            };

            var dependentResult = Matrix.EchelonForm(matrixDependent);

            Assert.AreEqual(0, dependentResult[1][0]);
            Assert.AreEqual(0, dependentResult[1][1]);
            Assert.AreEqual(0, dependentResult[2][0]);
            Assert.AreEqual(0, dependentResult[2][1]);
            Assert.AreEqual(0, dependentResult[2][2]);
        }

        [TestMethod]
        public void Matrix_ReducedEchelonForm()
        {
            var matrixA = new double[3][] {
                new double [] { 3, 4, 5 },
                new double [] { 12, 5, 3 },
                new double [] { 89, 1, 9 }};

            var result = Matrix.ReducedEchelonForm(matrixA);

            Assert.AreEqual(1, result[0][0]);
            Assert.AreEqual(0, result[0][1]);
            Assert.AreEqual(0, result[0][2]);
            Assert.AreEqual(0, result[1][0]);
            Assert.AreEqual(1, result[1][1]);
            Assert.AreEqual(0, result[1][2]);
            Assert.AreEqual(0, result[2][0]);
            Assert.AreEqual(0, result[2][1]);
            Assert.AreEqual(1, result[2][2]);
        }

        [TestMethod]
        public void Matrix_LinearTransformation()
        {
            var matrixA = new double[4][] {
                new double [] { 5, 3, 8, 80 },
                new double [] { 7, 1, 9, 1 },
                new double [] { 50, 21, 1, 35 },
                new double [] { 6, 4, 56, 96 }};
            var vector = new double[] { 6, 7, 9, 4 };
            var transformationResultExpected = new double[] { 443, 134, 596, 952 };

            var transformation_Result = Matrix.LinearTranformation(matrixA, vector);

            for(int column = 0; column < 4; column++)
            {
                Assert.AreEqual(transformationResultExpected[column], transformation_Result[column]);
            }
        }

        [TestMethod]
        public void Matrix_Multiply()
        {
            var matrixA = new double[4][] { 
                new double [] { 5, 3, 8, 80 },
                new double [] { 7, 1, 9, 1 },
                new double [] { 50, 21, 1, 35 },
                new double [] { 6, 4, 56, 96 }};
            var matrixB = new double[4][] {
                new double [] { 6, 3, 0, 0 },
                new double [] { 7, 1, 0, 0 },
                new double [] { 9, 0, 1, 0 },
                new double [] { 4, 2, 0, 0 }};
            var matrixC = new double[4][] {
                new double [] { 443, 178, 8, 0 },
                new double [] { 134, 24, 9, 0 },
                new double [] { 596, 241, 1, 0 },
                new double [] { 952, 214, 56, 0 }};

            var matrix_Result = Matrix.MatrixMultiply(matrixA, matrixB);

            for (var row = 0; row < matrixC.GetLength(0); row++)
            {
                for (var column = 0; column < matrixC[row].GetLength(0); column++)
                {
                    Assert.AreEqual(matrixC[row][column], matrix_Result[row][column]);
                }
            }
        }

        [TestMethod]
        public void Matrix_LinearlyIndependent_More_Columns_Then_Rows()
        {
            var matrix = new double[3][];
            matrix[0] = new double[4];
            matrix[1] = new double[4];
            matrix[2] = new double[4];
            var independent = Matrix.IsLinearlyIndependent(matrix);
            Assert.AreEqual(false, independent);
        }

        [TestMethod]
        public void Matrix_LinearlyIndependent_Column_Of_All_Zeros()
        {
            var matrix = new double[4][]
            {
                new double [] { 4, 0, 3, 4 },
                new double [] { 8, 0, 89, 5 },
                new double [] { 7, 0, 78, 4 },
                new double [] { 65, 0, 63, 5 }
            };
            var independent = Matrix.IsLinearlyIndependent(matrix);
            Assert.AreEqual(false, independent);
        }

        [TestMethod]
        public void Matrix_LinearlyIndependent_AugmentedMatrixTest()
        {
            var matrixIndependent = new double[4][]
            {
                new double [] { 6, 45, 36, 8 },
                new double [] { 7, 21, 6, 9 },
                new double [] { 12, 3, 8, 9 },
                new double [] { 21, 6, 41, 6 }
            };

            var independent = Matrix.IsLinearlyIndependent(matrixIndependent);
            Assert.AreEqual(true, independent);

            var matrixDependent = new double[3][]
            {
                new double [] { 3, 9, 2 },
                new double [] { 3, 9, 8 },
                new double [] { 3, 9, 6 }
            };

            independent = Matrix.IsLinearlyIndependent(matrixDependent);
            Assert.AreEqual(false, independent);
        }

        [TestMethod]
        public void Matrix_Transpose()
        {
            var matrix_2X2 = new double[2][]
            {
                new double [] { 1, 2 },
                new double [] { 3, 4 },

            };
        }
    }
}
