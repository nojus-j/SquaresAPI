using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SquaresAPI.Models;

namespace SquaresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        static private List<Point> points = new List<Point>
        {
            new Point
            {
                Id = 0,
                X = -1,
                Y = 1
            },
            new Point
            {
                Id = 1,
                X = 1,
                Y = 1
            },
            new Point
            {
                Id = 2,
                X = 1,
                Y = -1
            },
            new Point
            {
                Id = 3,
                X = -1,
                Y = -1
            }
        };

        [HttpGet]
        public ActionResult<List<Point>> GetPoints()
        {
            return Ok(points);
        }

        [HttpGet("{id}")]
        public ActionResult<Point> GetPointById(int id) 
        {
            var point = points.FirstOrDefault(x => x.Id == id);
            if (point == null) 
            {
                return NotFound();
            }
            return Ok(point);
        }

        [HttpPost]
        public ActionResult<Point> AddPoint(Point newPoint)
        {
            if (newPoint == null)
            {
                return BadRequest();
            }

            points.Add(newPoint);
            return CreatedAtAction(nameof(GetPointById), new {id=newPoint.Id}, newPoint);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePoint(int id)
        {
            var point = points.FirstOrDefault(x => x.Id == id);
            if (point == null)
            {
                return NotFound();
            }

            points.Remove(point);
            return NoContent();
        }
    }
}
