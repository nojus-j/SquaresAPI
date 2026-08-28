using SquaresAPI.Models;
using SquaresAPI.Services;
using Xunit;

namespace SquaresAPI.Tests
{
    public class SquareFinderServiceTests
    {
        private readonly SquareFinderService _service;
        public SquareFinderServiceTests()
        {
            _service = new SquareFinderService();
        }

        [Fact]
        public void FindSquares_ReturnZero_WhenLessThanFourPoints()
        {
            var points = new List<Point>
            {
                new() { X = 0, Y = 0 },
                new() { X = 0, Y = 2 },
                new() { X = 2, Y = 2 }
            };
        
            var result = _service.FindSquares(points);

            Assert.Empty(result);
        }

        [Fact]
        public void FindSquares_SingleAxisAlignedSquare()
        {
            var points = new List<Point>
            {
                new() { X = 0, Y = 0 },
                new() { X = 0, Y = 2 },
                new() { X = 2, Y = 2 },
                new() { X = 2, Y = 0 }
            };

            var result = _service.FindSquares(points);

            Assert.Single(result);
        }

        [Fact]
        public void FindSquares_DetectTiltedSquares()
        {
            var points = new List<Point>
            {
                new() { X = 1, Y = 0 },
                new() { X = 2, Y = 1 },
                new() { X = 1, Y = 2 },
                new() { X = 0, Y = 1 }
            };

            var result = _service.FindSquares(points);

            Assert.Single(result);
        }

        [Fact]
        public void FindSquares_NotDuplicateDuplicatePoints()
        {
            var points = new List<Point>
            {
                new() { X = 0, Y = 0 },
                new() { X = 0, Y = 0 },
                new() { X = 0, Y = 2 },
                new() { X = 2, Y = 2 },
                new() { X = 2, Y = 0 }
            };

            var result = _service.FindSquares(points);

            Assert.Single(result);
        }
    }
}