using System;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            int[][] figures = new int[][] 
            {
                new int[] { 5, 6 },
                new int[] { -3, -4 } 
            };
            int[][] references = new int[][]
            {
                new int[] { 3, 2 },
                new int[] { -1, -2 },
                new int[] { 9 , 4 },
                new int[] { -4, 0 },
                new int[] { 4, -1 },
                new int[] { 0, -3 },
                new int[] { 0, 5 },
                new int[] { -2, 2 },
                new int[] { 1, -3 },
                new int[] { -3, 3 }
            };
            double[,] distances = new double[references.Length, figures.Length];
            int vectorLength;
            double sum;
            for (int i = 0; i < references.Length; i++)
            {
                for (int j = 0; j < figures.Length; j++)
                {
                    vectorLength = references[i].Length;
                    sum = 0;
                    for (int k = 0; k < vectorLength; k++)
                    {
                        sum += Math.Pow(figures[j][k] - references[i][k], 2);
                    }
                    distances[i, j] = Math.Round(Math.Sqrt(sum), 4);
                }
            }
            for (int i = 0; i < distances.GetUpperBound(0) + 1; i++)
            {
                Console.Write("References: [");
                for (int j = 0; j < references[i].Length; j++)
                {
                    Console.Write($"{references[i][j]}, ");
                }
                Console.Write("\b\b], ");
                Console.Write($"distances: [{distances[i, 0]}, {distances[i, 1]}], ");
                if (distances[i, 0] < distances[i, 1])
                    Console.Write("знаходиться у класi \u03CE1.\n");
                else Console.Write("знаходиться у класi \u03CE2.\n");
            }
            Console.ReadKey();
        }
    }
}
