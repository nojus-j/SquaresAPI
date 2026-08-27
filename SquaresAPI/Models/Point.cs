using System.Net;

namespace SquaresAPI.Models
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public class PointList
    {
        public Guid Id {  get; set; } = Guid.NewGuid();
        public List<Point> Points { get; set; } = new();
    }
    public class SquareResponse
    {
        public Guid ListId { get; set;  }
        public int TotalSquares { get; set; }
        public List<List<Point>> Squares { get; set; } = new();
    }
}
