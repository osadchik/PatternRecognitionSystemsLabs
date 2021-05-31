using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    class BayesMethod
    {
        public IEnumerable<(Matrix, Matrix)> FindDecisiveFunction(IEnumerable<Claster> clasters)
        {
            List<Matrix> sampleMeanList = new List<Matrix>();
            List<Matrix> covariantMatrixList = new List<Matrix>();
            foreach (var claster in clasters)
            {
                var sampleMean = FindSampleMean(claster.Points);
                sampleMeanList.Add(new Matrix(new double[,] { { sampleMean.X, sampleMean.Y } }));
                var covariantMatrix = FindCovariantMatrix(claster.Points, sampleMean);
                covariantMatrixList.Add(covariantMatrix);
            }
            Matrix argumentCoefficientMatrix;
            Matrix freeCoefficient;
            List<(Matrix, Matrix)> functionVariables = new List<(Matrix, Matrix)>();
            for (int i = 0; i < covariantMatrixList.Count; i++)
            {
                argumentCoefficientMatrix = covariantMatrixList[i].Inverse() * sampleMeanList[i].Transpose();
                freeCoefficient = sampleMeanList[i] * 0.5 * covariantMatrixList[i].Inverse() * sampleMeanList[i].Transpose() * (-1);
                functionVariables.Add((argumentCoefficientMatrix, freeCoefficient));
            }
            List<(Matrix, Matrix)> result = new List<(Matrix, Matrix)>();
            for (int i = 0; i < functionVariables.Count - 1; i++)
            {
                for (int j = i + 1; j < functionVariables.Count; j++)
                {
                    result.Add((FormatResult(functionVariables[i].Item1 - functionVariables[j].Item1),
                        FormatResult(functionVariables[i].Item2 - functionVariables[j].Item2)));
                }
            }
            return result; 
        }

        Point FindSampleMean(IEnumerable<Point> images)
        {
            double resultX = 0, resultY = 0;
            int count = images.Count();
            foreach (var image in images)
            {
                resultX += image.X;
                resultY += image.Y;
            }
            return new Point(resultX / count, resultY / count);
        }

        Matrix FindCovariantMatrix(IEnumerable<Point> points, Point sampleMean)
        {
            var result = new Matrix(new double[2, 2]);
            foreach (var image in points)
            {
                var imageAsMatrix = new Matrix(new double[,] { { image.X }, { image.Y } });
                var transponedMatrix = imageAsMatrix.Transpose();
                var multiplicationResult = imageAsMatrix * transponedMatrix;
                result += multiplicationResult;
            }
            result /= points.Count();
            var sampleMeanAsMatrix = new Matrix(new double[,] { { sampleMean.X }, { sampleMean.Y} });
            sampleMeanAsMatrix = sampleMeanAsMatrix * sampleMeanAsMatrix.Transpose();
            result -= sampleMeanAsMatrix;
            return result;
        }

        Matrix FormatResult(Matrix matrix)
        {
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    matrix[i, j] = Math.Round(matrix[i, j], 3);
                }
            }
            return matrix;
        }
    }
}
