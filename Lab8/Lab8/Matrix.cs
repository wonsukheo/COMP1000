using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8
{
    public static class Matrix
    {
        public static int DotProduct(int[] v1, int[] v2)
        {
            int dotProduct = 0;

            if (v1.Length < 1)
            {
                return 0;
            }

            for (int i = 0; i < v1.Length; i++)
            {
                dotProduct += v1[i] * v2[i];
            }

            return dotProduct;
        }

        public static int[,] Transpose(int[,] matrix)
        {
            int row = matrix.GetLength(0);
            int column = matrix.GetLength(1);

            int[,] transposed = new int[column, row];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    transposed[j, i] = matrix[i, j];
                }
            }

            return transposed;
        }

        public static int[,] GetIdentityMatrix(int size)
        {
            int[,] identityMatrix = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                identityMatrix[i, i] = 1;
            }

            return identityMatrix;
        }

        public static int[] GetRowOrNull(int[,] matrix, int row)
        {
            int col = matrix.GetLength(1);
            int[] rowMatrix = new int[col];

            if (row < 0 || row >= matrix.GetLength(0))
            {
                return null;
            }

            for (int i = 0; i < col; i++)
            {
                rowMatrix[i] = matrix[row, i];
            }

            return rowMatrix;
        }

        public static int[] GetColumnOrNull(int[,] matrix, int col)
        {
            int row = matrix.GetLength(0);
            int[] colMatrix = new int[row];

            if (col < 0 || col >= matrix.GetLength(1))
            {
                return null;
            }

            for (int i = 0; i < row; i++)
            {
                colMatrix[i] = matrix[i, col];
            }

            return colMatrix;
        }

        public static int[] MultiplyMatrixVectorOrNull(int[,] matrix, int[] vector)
        {
            if (matrix.GetLength(1) != vector.Length)
            {
                return null;
            }

            int[,] transposeMatrix = Transpose(matrix);
            int row = transposeMatrix.GetLength(0); // 4
            int col = transposeMatrix.GetLength(1); // 3

            int[] result = new int[col];

            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    result[i] += vector[j] * transposeMatrix[j, i];
                }
            }

            return result;
        }

        public static int[] MultiplyVectorMatrixOrNull(int[] vector, int[,] matrix)
        {
            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);

            if (row != vector.Length)
            {
                return null;
            }

            int[] result = new int[col];

            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    result[i] += vector[j] * matrix[j, i];
                }
            }

            return result;
        }

        public static int[,] MultiplyOrNull(int[,] multiplicandMatrix, int[,] multiplierMatrix)
        {
            int aRow = multiplicandMatrix.GetLength(0);
            int aCol = multiplicandMatrix.GetLength(1);
            int bRow = multiplierMatrix.GetLength(0);
            int bCol = multiplierMatrix.GetLength(1);

            if (aCol != bRow)
            {
                return null;
            }

            int[,] result = new int[aRow, bCol];

            for (int i = 0; i < aRow; i++)
            {
                for (int j = 0; j < bCol; j++)
                {
                    for (int k = 0; k < aCol; k++)
                    {
                        result[i, j] += multiplicandMatrix[i, k] * multiplierMatrix[k, j];
                    }
                }
            }

            return result;
        }
    }
}
