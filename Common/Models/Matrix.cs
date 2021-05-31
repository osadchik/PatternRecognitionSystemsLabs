using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Common.Models
{
    [Serializable]
    public class MatrixException : Exception
    {
        protected MatrixException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public MatrixException() { }
    }
    public class Matrix : ICloneable
    {
        private readonly int rows, columns;
        private readonly double[,] array;
        /// <summary>
        /// Number of rows.
        /// </summary>
        public int Rows
        {
            get => rows;
        }

        /// <summary>
        /// Number of columns.
        /// </summary>
        public int Columns
        {
            get => columns;
        }

        /// <summary>
        /// Gets an array of floating-point values that represents the elements of this Matrix.
        /// </summary>
        public double[,] Array
        {
            get => array;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Matrix(int rows, int columns)
        {
            if (rows <= 0) throw new ArgumentOutOfRangeException("rows");
            if (columns <= 0) throw new ArgumentOutOfRangeException("columns");
            this.rows = rows;
            this.columns = columns;
            array = new double[rows, columns];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class with the specified elements.
        /// </summary>
        /// <param name="array">An array of floating-point values that represents the elements of this Matrix.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Matrix(double[,] array)
        {
            if (array == null) throw new ArgumentNullException("array");
            rows = array.GetLength(0);
            columns = array.GetLength(1);
            this.array = array;

        }

        /// <summary>
        /// Allows instances of a Matrix to be indexed just like arrays.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <exception cref="ArgumentException"></exception>
        public double this[int row, int column]
        {
            get
            {
                if (array.GetLength(0) < row || row < 0) throw new ArgumentException("Index is out of range", "row");
                if (array.GetLength(1) < column || column < 0) throw new ArgumentException("Index is out of range", "column");
                return array[row, column];
            }
            set 
            {
                if (array.GetLength(0) < row || row < 0) throw new ArgumentException("Index is out of range", "row");
                if (array.GetLength(1) < column || column < 0) throw new ArgumentException("Index is out of range", "column");
                array[row, column] = value; 
            }
        }

        public Matrix Transpose()
        {
            double[,] result = new double[columns, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[j, i] = array[i, j];
                }
            }

            return new Matrix(result);
        }

        /// <summary>
        /// Creates a deep copy of this Matrix.
        /// </summary>
        /// <returns>A deep copy of the current object.</returns>
        public object Clone()
        {
            return new Matrix(array);
        }

        /// <summary>
        /// Adds two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is sum of two matrices.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 == null) throw new ArgumentNullException("matrix1");
            if (matrix2 == null) throw new ArgumentNullException("matrix2");
            if (matrix1.rows != matrix2.rows || matrix1.columns != matrix2.columns) throw new MatrixException();
                double[,] result = new double[matrix1.rows, matrix2.columns];
                for (int i = 0; i < matrix1.rows; i++)
                {
                    for (int j = 0; j < matrix1.columns; j++)
                    {
                        result[i, j] = matrix1[i, j] + matrix2[i, j];
                    }
                }
                return new Matrix(result);
        }

        /// <summary>
        /// Subtracts two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is subtraction of two matrices</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 == null) throw new ArgumentNullException("matrix1");
            if (matrix2 == null) throw new ArgumentNullException("matrix2");
            if (matrix1.rows != matrix2.rows || matrix1.columns != matrix2.columns) throw new MatrixException();
            double[,] result = new double[matrix1.rows, matrix2.columns];
            for (int i = 0; i < matrix1.rows; i++)
            {
                for (int j = 0; j < matrix1.columns; j++)
                {
                    result[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }
            return new Matrix(result);
        }

        /// <summary>
        /// Multiplies two matrices.
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>New <see cref="Matrix"/> object which is multiplication of two matrices.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MatrixException"></exception>
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 == null) throw new ArgumentNullException("matrix1");
            if (matrix2 == null) throw new ArgumentNullException("matrix2");
            if (matrix1.columns != matrix2.rows) throw new MatrixException();
            double[,] result = new double[matrix1.rows, matrix2.columns];
            for (int i = 0; i < matrix1.rows; i++)
            {
                for (int j = 0; j < matrix2.columns; j++)
                {
                    for (int k = 0; k < matrix1.columns; k++)
                    {
                        result[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }
            return new Matrix(result);
        }

        public static Matrix operator *(Matrix matrix, double number)
        {
            if (matrix == null) throw new ArgumentNullException("matrix");
            if (number == 0) throw new InvalidOperationException();
            double[,] result = new double[matrix.rows, matrix.columns];
            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.columns; j++)
                {
                    result[i, j] = matrix[i, j] * number;
                }
            }
            return new Matrix(result);
        }

        public static Matrix operator /(Matrix matrix, double number)
        {
            if (matrix == null) throw new ArgumentNullException("matrix");
            if (number == 0) throw new InvalidOperationException();
            double[,] result = new double[matrix.rows, matrix.columns];
            for (int i = 0; i < matrix.rows; i++)
            {
                for (int j = 0; j < matrix.columns; j++)
                {
                    result[i, j] = matrix[i, j] / number; 
                }
            }
            return new Matrix(result);
        }

        public Matrix Inverse()
        {
            int n = array.GetLength(0);

            double[,] xirtaM = new double[n, n];
            double[,] helper = new double[n, n];
            for (int i = 0; i < n; i++)
                xirtaM[i, i] = 1;

            double[,] Matrix_Big = new double[n, 2 * n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    Matrix_Big[i, j] = array[i, j];
                    Matrix_Big[i, j + n] = xirtaM[i, j];
                }

            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < 2 * n; i++)
                    Matrix_Big[k, i] = Matrix_Big[k, i] / array[k, k];
                for (int i = k + 1; i < n; i++)
                {
                    double K = Matrix_Big[i, k] / Matrix_Big[k, k];
                    for (int j = 0; j < 2 * n; j++)
                        Matrix_Big[i, j] = Matrix_Big[i, j] - Matrix_Big[k, j] * K; 
                }
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        helper[i, j] = Matrix_Big[i, j];
            }

            for (int k = n - 1; k > -1; k--)
            {
                for (int i = 2 * n - 1; i > -1; i--)
                    Matrix_Big[k, i] = Matrix_Big[k, i] / helper[k, k];
                for (int i = k - 1; i > -1; i--)
                {
                    double K = Matrix_Big[i, k] / Matrix_Big[k, k];
                    for (int j = 2 * n - 1; j > -1; j--)
                        Matrix_Big[i, j] = Matrix_Big[i, j] - Matrix_Big[k, j] * K;
                }
            }

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    xirtaM[i, j] = Matrix_Big[i, j + n];

            return new Matrix(xirtaM);
        }

        /// <summary>
        /// Tests if <see cref="Matrix"/> is identical to this Matrix.
        /// </summary>
        /// <param name="obj">Object to compare with. (Can be null)</param>
        /// <returns>True if matrices are equal, false if are not equal.</returns>
        /// <exception cref="InvalidCastException">Thrown when object has wrong type.</exception>
        /// <exception cref="MatrixException">Thrown when matrices are incomparable.</exception>
        public override bool Equals(object obj)
        {
            return obj is Matrix && array.Rank == (obj as Matrix).array.Rank && Enumerable.Range(0, array.Rank)
                .All(dimension => array.GetLength(dimension) == (obj as Matrix).array.GetLength(dimension))
                && array.Cast<double>().SequenceEqual((obj as Matrix).array.Cast<double>());
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(array);
        }
    }
}
