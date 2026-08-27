using System.Net;
using System.Text.Json.Serialization;

namespace SquaresAPI.Models
{
    // I used a bit of AI for help to give me an idea of how I should structure my whole project.
    // From a couple of options I decided to create three classes

    // decided to use globally unique identifiers because I never used them before and wanted to gain some experience.
    // The gains for using Guid is not useful in this project
    public class Point
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        [JsonIgnore]
        public Guid PointListId { get; set; }
    }
    public class PointList
    {
        public Guid Id {  get; set; } = Guid.NewGuid();
        public List<Point> Points { get; set; } = new List<Point>();
    }
    public class SquareResponse
    {
        public Guid ListId { get; set;  }
        public int TotalSquares { get; set; }
        public List<List<Point>> Squares { get; set; } = new();
    }
}
