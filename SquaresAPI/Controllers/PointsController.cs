using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SquaresAPI.Models;
using SquaresAPI.Services;
using System.Collections.Concurrent;
using SquaresAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace SquaresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ISquareFinderService _squareFinder;
        public PointsController(AppDbContext context, ISquareFinderService squareFinder)
        {
            _context = context;
            _squareFinder = squareFinder;
        }

        [HttpPost]
        public async Task<ActionResult<PointList>> ImportList([FromBody] List<Point> points)
        {
            var pointList = new PointList
            {
                Points = points.DistinctBy(p => (p.X, p.Y)).ToList()
            };

            _context.PointLists.Add(pointList);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetList), new { id = pointList.Id }, pointList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PointList>> GetList(Guid id)
        {
            var list = await _context.PointLists
                .Include(l => l.Points)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        [HttpPost("{id}/points")]
        public async Task<IActionResult> AddPoint(Guid id, [FromBody]Point point)
        {
            var list = await _context.PointLists
                .Include(l => l.Points)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (list == null)
            {
                return NotFound();
            }

            if (!list.Points.Any(p => p.X == point.X && p.Y == point.Y))
            {
                point.PointListId = id;
                _context.Points.Add(point);
                await _context.SaveChangesAsync();
            }

            return Ok(list);
        }

        [HttpDelete("{id}/points")]
        public async Task<IActionResult> DeletePoint(Guid id, [FromBody]Point point)
        {
            var list = await _context.PointLists
                .Include(l => l.Points)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (list == null)
            {
                return NotFound();
            }

            var pointToRemove = list.Points.FirstOrDefault(p => p.X == point.X && p.Y == point.Y);
            if (pointToRemove != null)
            {
                _context.Points.Remove(pointToRemove);
                await _context.SaveChangesAsync();
            }

            return Ok(list);
        }

        [HttpGet("{id}/squares")]
        public async Task<ActionResult<SquareResponse>> GetSquares(Guid id)
        {
            var list = await _context.PointLists
                .Include(l => l.Points)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (list == null)
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
