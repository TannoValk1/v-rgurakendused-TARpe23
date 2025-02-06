using Microsoft.AspNetCore.Mvc;
using Restap.Models;
using System.Collections.Generic;
using System.Linq;

namespace Restap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly DataContext _context;

        public EventsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetEvents(string? name = null)
        {
            var query = _context.Events!.AsQueryable();

            if (name != null)
                query = query.Where(x => x.Name != null && x.Name.ToUpper().Contains(name.ToUpper()));

            return query.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<TextReader> GetEvent(int id)
        {
            var test = _context.Events!.Find(id);

            if (test == null)
            {
                return NotFound();
            }

            return Ok(test);
        }

        [HttpPut("{id}")]
        public IActionResult PutEvent(int id, Event test)
        {
            var dbTest = _context.Events!.AsNoTracking().FirstOrDefault(x => x.Id == test.Id);
            if (id != test.Id || dbTest == null)
            {
                return NotFound();
            }

            _context.Update(test);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public ActionResult<Event> PostEvent(Event test)
        {
            var dbExercise = _context.Events!.Find(test.Id);
            var sp = _context.Speakers!.Find(test.SpeakerId);
            if (sp == null)
            {
                return NotFound();
            }
            if (sp.Id == null)
            {
                return NotFound();
            }
            if (dbExercise == null)
            {
                _context.Add(test);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetEvent), new { Id = test.Id }, test);
            }
            else
            {
                return Conflict();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var test = _context.Events!.Find(id);
            if (test == null)
            {
                return NotFound();
            }

            _context.Remove(test);
            _context.SaveChanges();

            return NoContent();
        }
    }
}