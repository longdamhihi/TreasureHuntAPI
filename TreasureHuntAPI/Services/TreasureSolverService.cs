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
            var treasurePositions = new Dictionary<int, List<(int x, int y)>>();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int chestNumber = a[i][j];

                    if (!treasurePositions.ContainsKey(chestNumber))
                        treasurePositions[chestNumber] = new List<(int, int)>();

                    treasurePositions[chestNumber].Add((i, j));
                }
            }

            var currentPositions = new List<(int x, int y)> { (0, 0) };
            double totalCost = 0;

            for (int chest = 1; chest <= p; chest++)
            {
                double minDist = double.MaxValue;
                List<(int x, int y)> nextPositions = treasurePositions[chest];

                foreach (var curr in currentPositions)
                {
                    foreach (var next in nextPositions)
                    {
                        double dist = Math.Sqrt(
                            Math.Pow(curr.x - next.x, 2) + Math.Pow(curr.y - next.y, 2));
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
