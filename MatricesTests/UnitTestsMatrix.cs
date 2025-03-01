using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Матричный_калькулятор;

namespace MatricesTests
{
    [TestClass]
    public class UnitTestsMatrix

    {
        public bool AreEqual(double[,] matrix1, double[,] matrix2)
        {
            if (matrix1.GetLength(0) != matrix2.GetLength(0) || matrix1.GetLength(1) != matrix2.GetLength(1))
            {
                return false;
            }

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    if (matrix1[i, j] != matrix2[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        [TestMethod]
        public void CorrectAddMatrcesCalculation()
        {
            MatrixOperations matrixOperations = new MatrixOperations();

            double[,] Amatrix = new double[,]
            {
                {1,2,3},
                {4,5,6}
            };
            double[,] Bmatrix = new double[,]
            {
                {7,8,9},
                {10,11,12}
            };
            double[,] CorrectResult = new double[,]
            {
                {8,10,12},
                {14,16,18}
            };
            double[,] ExpectedResult =matrixOperations.AddMatrices(Amatrix, Bmatrix);
            
            Assert.IsTrue(AreEqual(CorrectResult, ExpectedResult), "Матрицы складываются неправильно");
        }

        [TestMethod]
        public void DifferentSizesAddMatrcesCalculation()
        {
            MatrixOperations matrixOperations = new MatrixOperations();

            double[,] Amatrix = new double[,]
            {
                {1,2},
                {4,5}
            };

            double[,] Bmatrix = new double[,]
            {
                {7,8,9},
                {10,11,12}
            };

            double[,] ExpectedResult = matrixOperations.AddMatrices(Amatrix, Bmatrix);

            Assert.ThrowsException<NullReferenceException>(() => AreEqual(null, ExpectedResult), "Матрицы складываются неправильно");

        }


        [TestMethod]
        public void ZeroMatrixAddMatrcesCalculation()
        {
            MatrixOperations matrixOperations = new MatrixOperations();

            double[,] Amatrix = new double[,]
            {
                {1,2,3},
                {4,5,6}
            };
            double[,] Bmatrix = new double[,]
            {
                {0,0,0},
                {0,0,0}
            };
            double[,] CorrectResult = new double[,]
            {
                {1,2,3},
                {4,5,6}
            };
            double[,] ExpectedResult = matrixOperations.AddMatrices(Amatrix, Bmatrix);

            Assert.IsTrue(AreEqual(CorrectResult, ExpectedResult), "Матрицы складываются неправильно");
        }
        [TestMethod]
        public void FractionalMatricesAddMatrcesCalculation()
        {
            MatrixOperations matrixOperations = new MatrixOperations();

            double[,] Amatrix = new double[,]
            {
                {1.5,2.3,-3.64},
                {43.1,0.3,612.3}
            };
            double[,] Bmatrix = new double[,]
            {
                {3.14,32,2.2},
                {80.543,10.1,0.12}
            };
            double[,] CorrectResult = new double[,]
            {
                {4.64,34.3,-1.44},
                {123.64,10.4,612.42}
            };
            double[,] ExpectedResult = matrixOperations.AddMatrices(Amatrix, Bmatrix);

            Assert.IsTrue(AreEqual(CorrectResult, ExpectedResult), "Матрицы складываются неправильно");
        }

        [TestMethod]
        public void NegativeNumbersAddMatrcesCalculation()
        {
            MatrixOperations matrixOperations = new MatrixOperations();

            double[,] Amatrix = new double[,]
            {
                {-1,-2,-3},
                {-4,-5,-6}
            };
            double[,] Bmatrix = new double[,]
            {
                {-7,-8,-9},
                {-10,-11,-12}
            };
            double[,] CorrectResult = new double[,]
            {
                {-8,-10,-12},
                {-14,-16,-18}
            };
            double[,] ExpectedResult = matrixOperations.AddMatrices(Amatrix, Bmatrix);

            Assert.IsTrue(AreEqual(CorrectResult, ExpectedResult), "Матрицы складываются неправильно");
        }

        [TestMethod]
        public void CorrectMultiplyMatrices()
        {
            MatrixOperations matrixOperations = new MatrixOperations();

            double[,] Amatrix = new double[,]
            {
                {1,2,3},
                {4,5,6}
            };
            double[,] Bmatrix = new double[,]
            {
                {7,8,},
                {9,10,},
                {11,12}
            };
            double[,] CorrectResult = new double[,]
            {
                {58,64},
                {139,154}
            };
            double[,] ExpectedResult = matrixOperations.MultiplyMatrices(Amatrix, Bmatrix);

            Assert.IsTrue(AreEqual(CorrectResult, ExpectedResult), "Матрицы умножаются неправильно");
        }
        [TestMethod]
        public void DifferentSizesMultiplyMatrices()
        {
            MatrixOperations matrixOperations = new MatrixOperations();

            double[,] Amatrix = new double[,]
            {
                  {1, 2, 3},
                  {4, 5, 6}
            };

            double[,] Bmatrix = new double[,]
            {
                 {7, 8},
                 {9, 10}
            };

            double[,] ExpectedResult = matrixOperations.MultiplyMatrices(Amatrix, Bmatrix);


            Assert.ThrowsException<NullReferenceException>(() => AreEqual(null, ExpectedResult), "Матрицы умножаются неправильно");

        }

        [TestMethod]
        public void ZeroMultiplyMatrices()
        {
            MatrixOperations matrixOperations = new MatrixOperations();

            double[,] Amatrix = new double[,]
            {
                {0,0,0},
                {0,0,0}
            };
            double[,] Bmatrix = new double[,]
            {
                {7,8,},
                {9,10,},
                {11,12}
            };
            double[,] CorrectResult = new double[,]
            {
                {0,0},
                {0,0}
            };
            double[,] ExpectedResult = matrixOperations.MultiplyMatrices(Amatrix, Bmatrix);

            Assert.IsTrue(AreEqual(CorrectResult, ExpectedResult), "Матрицы умножаются неправильно");
        }
        [TestMethod]
        public void NegativeMatrixMultiplyMatrices()
        {
            MatrixOperations matrixOperations = new MatrixOperations();

            double[,] Amatrix = new double[,]
            {
                {-1,-2,-3},
                {-4,-5,-6}
            };
            double[,] Bmatrix = new double[,]
            {
                {7,8,},
                {9,10,},
                {11,12}
            };
            double[,] CorrectResult = new double[,]
            {
                {-58,-64},
                {-139,-154}
            };
            double[,] ExpectedResult = matrixOperations.MultiplyMatrices(Amatrix, Bmatrix);

            Assert.IsTrue(AreEqual(CorrectResult, ExpectedResult), "Матрицы умножаются неправильно");
        }

        [TestMethod]
        public void CorrectMultiplyByScalar()
        {
            MatrixOperations matrixOperations = new MatrixOperations();

            double[,] Matrix = new double[,]
            {
                {1,2},
                {3,4}
            };
            
            double[,] CorrectResult = new double[,]
            {
                {4,8},
                {12,16}
            };
            int scalar = 4;

            double[,] ExpectedResult = matrixOperations.MultiplyMatrixByScalar(Matrix, scalar);

            Assert.IsTrue(AreEqual(CorrectResult, ExpectedResult), "Матрицы умножаются неправильно");
        }
        [TestMethod]
        public void ScalaraIsZeroMultiplyByScalar()
        {
            MatrixOperations matrixOperations = new MatrixOperations();

            double[,] Matrix = new double[,]
            {
                {1,2},
                {3,4}
            };

            double[,] CorrectResult = new double[,]
            {
                {0,0},
                {0,0}
            };
            int scalar = 0;

            double[,] ExpectedResult = matrixOperations.MultiplyMatrixByScalar(Matrix, scalar);

            Assert.IsTrue(AreEqual(CorrectResult, ExpectedResult), "Матрицы умножаются неправильно");
        }

        [TestMethod]
        public void NegativeScalarMultiplyByScalar()
        {
            MatrixOperations matrixOperations = new MatrixOperations();

            double[,] Matrix = new double[,]
            {
                {1,2},
                {3,4}
            };

            double[,] CorrectResult = new double[,]
            {
                {-3,-6},
                {-9,-12}
            };
            int scalar = -3;

            double[,] ExpectedResult = matrixOperations.MultiplyMatrixByScalar(Matrix, scalar);

            Assert.IsTrue(AreEqual(CorrectResult, ExpectedResult), "Матрицы умножаются неправильно");
        }

    }
}
