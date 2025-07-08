using System;

namespace TreasureHuntAPI.Services
{
    public interface ITreasureSolverService
    {
        double CalculateMinimalFuel(int n, int m, int p, int[][] a);
    }

    public class TreasureSolverService : ITreasureSolverService
    {
        public double CalculateMinimalFuel(int n, int m, int p, int[][] a)
        {
            // --- INPUT VALIDATIONS ---

            if (n <= 0 || n > 500)
                throw new ArgumentException("n must be between 1 and 500.");

            if (m <= 0 || m > 500)
                throw new ArgumentException("m must be between 1 and 500.");

            if (p <= 0 || p > n * m)
                throw new ArgumentException("p must be between 1 and n*m.");

            if (a == null || a.Length != n)
                throw new ArgumentException("Matrix rows do not match n.");

            for (int i = 0; i < n; i++)
            {
                if (a[i] == null || a[i].Length != m)
                    throw new ArgumentException($"Row {i} does not have {m} columns.");
            }

            var treasurePositions = new Dictionary<int, List<(int x, int y)>>();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int chestNumber = a[i][j];

                    if (chestNumber < 1 || chestNumber > p)
                        throw new ArgumentException($"Invalid chest number {chestNumber} at ({i + 1},{j + 1}). Must be between 1 and p.");

                    if (!treasurePositions.ContainsKey(chestNumber))
                        treasurePositions[chestNumber] = new List<(int, int)>();

                    treasurePositions[chestNumber].Add((i, j));
                }
            }

            // Ensure chest p exists
            if (!treasurePositions.ContainsKey(p))
                throw new ArgumentException($"Chest number {p} does not exist on the map.");

            var currentPositions = new List<(int x, int y)> { (0, 0) };
            double totalCost = 0;

            for (int chest = 1; chest <= p; chest++)
            {
                if (!treasurePositions.ContainsKey(chest))
                    throw new ArgumentException($"No island has chest {chest}.");

                double minDist = double.MaxValue;
                List<(int x, int y)> nextPositions = treasurePositions[chest];

                foreach (var curr in currentPositions)
                {
                    foreach (var next in nextPositions)
                    {
                        double dist = Math.Sqrt(
                            Math.Pow(curr.x - next.x, 2) +
                            Math.Pow(curr.y - next.y, 2));
                        if (dist < minDist)
                        {
                            minDist = dist;
                        }
                    }
                }

                totalCost += minDist;
                currentPositions = nextPositions;
            }

            return Math.Round(totalCost, 5);
        }
    }
}
