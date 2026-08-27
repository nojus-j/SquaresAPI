using SquaresAPI.Models;

namespace SquaresAPI.Services
{
    // I was learning about interface segregation so I decided to implement one here.
    // It makes it easier to swap out an algorith if I would need to in the future.
    public interface ISquareFinderService
    {
        List<List<Point>> FindSquares(IEnumerable<Point> points);
    }
    
    // as the task said I didn't try to invent the wheel so I just looked up the best algorithm
    // for finding the squares in a 2D plane. In this case I used Pair-Based Edge Verification Algorithm
    public class SquareFinderService : ISquareFinderService
    {
        public List<List<Point>> FindSquares(IEnumerable<Point> points)
        {
            List<Point> pointList = points.DistinctBy(p => (p.X, p.Y)).ToList();
            HashSet<(int X, int Y)> pointSet = new HashSet<(int X, int Y)>(pointList.Select(p => (p.X, p.Y)));
            HashSet<string> seenSquareKeys = new HashSet<string>();
            List<List<Point>> result = new List<List<Point>>();

            for (int i = 0; i < pointList.Count; i++)
            {
                for (int j = i + 1; j < pointList.Count; j++)
                {
                    Point p1 = pointList[i];
                    Point p2 = pointList[j];

                    int dx = p2.X - p1.X;
                    int dy = p2.Y - p1.Y;

                    CheckAndAddSquare(p1, p2, (p1.X - dy, p1.Y + dx), (p2.X - dy, p2.Y + dx), pointSet, seenSquareKeys, result);
                    CheckAndAddSquare(p1, p2, (p1.X + dy, p1.Y - dx), (p2.X + dy, p2.Y - dx), pointSet, seenSquareKeys, result);
                }
            }
            return result;
        }

        private void CheckAndAddSquare(Point p1, Point p2, (int X, int Y) p3, (int X, int Y) p4,
            HashSet<(int X, int Y)> pointSet, HashSet<string> seenSquarekeys, List<List<Point>> result)
        {
            if (pointSet.Contains(p3) && pointSet.Contains(p4))
            {
                List<Point> square = new List<Point>
                {
                    p1, p2,
                    new Point { X = p4.X, Y = p4.Y },
                    new Point { X = p3.X, Y = p3.Y }
                };

                string key = string.Join(";", square.Select(p => $"{p.X},{p.Y}").OrderBy(k => k));

                if (seenSquarekeys.Add(key))
                {
                    result.Add(square);
                }
            }
        }
    }
}
