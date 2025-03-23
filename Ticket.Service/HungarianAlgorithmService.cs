using System;
using System.Linq;

namespace Ticket.Service
{
    public static class HungarianAlgorithmService
    {
        public static int[] Solve(int[,] costMatrix)
        {
            int rowCount = costMatrix.GetLength(0);
            int colCount = costMatrix.GetLength(1);
            int size = Math.Max(rowCount, colCount);

            int[,] matrix = new int[size, size];
            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                    matrix[i, j] = costMatrix[i, j];

            int[] labelsRow = new int[size];
            int[] labelsCol = new int[size];
            int[] matchRow = Enumerable.Repeat(-1, size).ToArray();
            int[] matchCol = Enumerable.Repeat(-1, size).ToArray();

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    labelsRow[i] = Math.Min(labelsRow[i], matrix[i, j]);

            for (int i = 0; i < size; i++)
            {
                bool[] seenRow = new bool[size];
                bool[] seenCol = new bool[size];
                while (!FindMatch(i, matrix, labelsRow, labelsCol, matchRow, matchCol, seenRow, seenCol))
                {
                    int delta = int.MaxValue;

                    for (int x = 0; x < size; x++)
                        if (seenRow[x]) 
                            for (int y = 0; y < size; y++)
                                if (!seenCol[y]) 
                                    delta = Math.Min(delta, labelsRow[x] + labelsCol[y] - matrix[x, y]);

                    for (int x = 0; x < size; x++)
                    {
                        if (seenRow[x]) labelsRow[x] -= delta;
                        if (seenCol[x]) labelsCol[x] += delta;
                    }
                }
            }

            int[] result = new int[rowCount];
            for (int i = 0; i < rowCount; i++)
                result[i] = matchRow[i] < colCount ? matchRow[i] : -1;

            return result;
        }

        private static bool FindMatch(int x, int[,] matrix, int[] labelsRow, int[] labelsCol,
                                      int[] matchRow, int[] matchCol, bool[] seenRow, bool[] seenCol)
        {
            seenRow[x] = true;
            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                if (seenCol[y]) continue;

                int slack = labelsRow[x] + labelsCol[y] - matrix[x, y];

                if (slack == 0)
                {
                    seenCol[y] = true;
                    if (matchCol[y] == -1 || FindMatch(matchCol[y], matrix, labelsRow, labelsCol, matchRow, matchCol, seenRow, seenCol))
                    {
                        matchRow[x] = y;
                        matchCol[y] = x;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
