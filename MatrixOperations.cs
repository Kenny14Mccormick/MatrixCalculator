using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Матричный_калькулятор
{
    public class MatrixOperations
    {
        public double[,] AddMatrices(double[,] matrix1, double[,] matrix2)
        {
            int rows1 = matrix1.GetLength(0);
            int columns1 = matrix1.GetLength(1);
            int rows2 = matrix2.GetLength(0);
            int columns2 = matrix2.GetLength(1);

            try
            {
                // Проверить, что размеры матриц совпадают
                if (rows1 != rows2 || columns1 != columns2)
                {
                    throw new Exception("Размеры матриц не совпадают.");
                }

                double[,] resultMatrix = new double[rows1, columns1];
                for (int i = 0; i < rows1; i++)
                {
                    for (int j = 0; j < columns1; j++)
                    {
                        resultMatrix[i, j] = Math.Round(matrix1[i, j] + matrix2[i, j], 2);
                    }
                }

                return resultMatrix;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public double[,] SubtractMatrices(double[,] matrix1, double[,] matrix2)
        {
            int rows1 = matrix1.GetLength(0);
            int columns1 = matrix1.GetLength(1);
            int rows2 = matrix2.GetLength(0);
            int columns2 = matrix2.GetLength(1);

            try
            {

                if (rows1 != rows2 || columns1 != columns2)
                {
                    throw new Exception("Размеры матриц не совпадают");
                }
                double[,] resultMatrix = new double[rows1, columns1];

                for (int i = 0; i < rows1; i++)
                {
                    for (int j = 0; j < columns1; j++)
                    {
                        resultMatrix[i, j] = Math.Round(matrix1[i, j] - matrix2[i, j], 2);
                    }
                }

                return resultMatrix;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public double[,] MultiplyMatrices(double[,] matrix1, double[,] matrix2)
        {
            int rows1 = matrix1.GetLength(0);
            int columns1 = matrix1.GetLength(1);
            int rows2 = matrix2.GetLength(0);
            int columns2 = matrix2.GetLength(1);

            try
            {
                if (columns1 != rows2)
                {
                    throw new Exception("Количество столбцов первой матрицы должно быть равно количеству строк второй матрицы для умножения.");
                }

                double[,] resultMatrix = new double[rows1, columns2];

                for (int i = 0; i < rows1; i++)
                {
                    for (int j = 0; j < columns2; j++)
                    {
                        for (int k = 0; k < columns1; k++)
                        {
                            resultMatrix[i, j] += Math.Round(matrix1[i, k] * matrix2[k, j],2);
                        }
                    }
                }

                return resultMatrix;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;

            }

        }
        public double[,] TransposeMatrix(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            double[,] transposedMatrix = new double[columns, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    transposedMatrix[j, i] = matrix[i, j];
                }
            }

            return transposedMatrix;
        }
        public double[,] MultiplyMatrixByScalar(double[,] matrix, double scalar)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            double[,] resultMatrix = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    resultMatrix[i, j] = matrix[i, j] * scalar;
                }
            }

            return resultMatrix;
        }

       
        

    }
}
