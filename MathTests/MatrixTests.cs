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
            var matrix = new double[3, 3]
            {
                {3, 5, 9 },
                {8, 1, 5 },
                {9, 2, 9 }
            };

            var result = Matrix.EchelonForm(matrix);

            Assert.AreEqual(result[1, 0], 0);
            Assert.AreEqual(result[2, 0], 0);
            Assert.AreEqual(result[2, 1], 0);
        }

        [TestMethod]
        public void Matrix_ReducedEchelonForm()
        {
            var matrixA = new double[3, 3] { { 3, 4, 5 },
                                            { 12, 5, 3 },
                                            { 89, 1, 9 }};

            var result = Matrix.ReducedEchelonForm(matrixA);

            Assert.AreEqual(1, result[0, 0]);
            Assert.AreEqual(0, result[0, 1]);
            Assert.AreEqual(0, result[0, 2]);
            Assert.AreEqual(0, result[1, 0]);
            Assert.AreEqual(1, result[1, 1]);
            Assert.AreEqual(0, result[1, 2]);
            Assert.AreEqual(0, result[2, 0]);
            Assert.AreEqual(0, result[2, 1]);
            Assert.AreEqual(1, result[2, 2]);
        }

        [TestMethod]
        public void Matrix_LinearTransformation()
        {
            var matrixA = new double[4, 4] { { 5, 3, 8, 80 },
                                        { 7, 1, 9, 1 },
                                        { 50, 21, 1, 35 },
                                        { 6, 4, 56, 96 }};
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
            var matrixA = new double[4, 4] { { 5, 3, 8, 80 },
                                        { 7, 1, 9, 1 },
                                        { 50, 21, 1, 35 },
                                        { 6, 4, 56, 96 }};
            var matrixB = new double[4, 4] { { 6, 3, 0, 0 },
                                        { 7, 1, 0, 0 },
                                        { 9, 0, 1, 0 },
                                        { 4, 2, 0, 0 }};
            var matrixC = new double[4, 4] { { 443, 178, 8, 0 },
                                        { 134, 24, 9, 0 },
                                        { 596, 241, 1, 0 },
                                        { 952, 214, 56, 0 }};

            var matrix_Result = Matrix.MatrixMultiply(matrixA, matrixB);

            for (var row = 0; row < matrixA.Rank; row++)
            {
                for (var column = 0; column < matrixA.GetLength(row); column++)
                {
                    Assert.AreEqual(matrixC[row, column], matrix_Result[row, column]);
                }
            }
        }
    }
}
