using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SquaresAPI.Models;
using SquaresAPI.Services;
using System.Collections.Concurrent;

namespace SquaresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        private static readonly ConcurrentDictionary<Guid, PointList> Storage = new();
        private readonly ISquareFinderService _squareFinder;

        public PointsController(ISquareFinderService squareFinder)
        {
            _squareFinder = squareFinder;
        }

        [HttpPost]
        public ActionResult<PointList> ImportList(List<Point> points)
        {
            PointList newList = new PointList
            {
                Points = points.DistinctBy(p => (p.X, p.Y)).ToList()
            };

            Storage[newList.Id] = newList;
            return CreatedAtAction(nameof(GetList), new { id = newList.Id }, newList);

        }

        [HttpGet("{id}")]
        public ActionResult<PointList> GetList(Guid id)
        {
            if (!Storage.TryGetValue(id, out var list))
                return NotFound();

            return Ok(list);
        }

        [HttpPost("{id}/points")]
        public IActionResult AddPoint(Guid id, [FromBody]Point point)
        {
            if (!Storage.TryGetValue(id, out var list))
            {
                return NotFound();
            }

            if (!list.Points.Any(p => p.X == point.X && p.Y == point.Y))
            {
                list.Points.Add(point);
            }

            return Ok(list);
        }

        [HttpDelete("{id}/points")]
        public IActionResult DeletePoint(Guid id, [FromBody]Point point)
        {
            if (!Storage.TryGetValue(id, out var list))
            {
                return NotFound();
            }

            list.Points.RemoveAll(p => p.X == point.X && p.Y == point.Y);

            return Ok(list);
        }

        [HttpGet("{id}/squares")]
        public ActionResult<SquareResponse> GetSquares(Guid id)
        {
            if (!Storage.TryGetValue(id, out var list))
            {
                return NotFound();
            }

            var squares = _squareFinder.FindSquares(list.Points);

            return Ok(new SquareResponse
            {
                ListId = id,
                TotalSquares = squares.Count,
                Squares = squares
            });
        }
   
    }
}
