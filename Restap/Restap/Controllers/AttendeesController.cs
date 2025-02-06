﻿using Microsoft.AspNetCore.Mvc;
using Restap.Models;
using System.Collections.Generic;
using System.Linq;

namespace Restap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendeesController : ControllerBase
    {
        private readonly DataContext _context;

        public AttendeesController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Attendee>> GetAttendees(string? name = null)
        {
            var query = _context.Attendees.AsQueryable();

            if (name != null)
                query = query.Where(x => x.Name != null && x.Name.ToUpper().Contains(name.ToUpper()));

            return query.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Attendee> GetAttendee(int id)
        {
            var attendee = _context.Attendees.Find(id);

            if (attendee == null)
            {
                return NotFound();
            }

            return Ok(attendee);
        }

        [HttpPut("{id}")]
        public IActionResult PutAttendee(int id, Attendee attendee)
        {
            if (id != attendee.Id)
            {
                return BadRequest();
            }

            _context.Entry(attendee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public ActionResult<Attendee> PostAttendee(Attendee attendee)
        {
            var dbAttendee = _context.Attendees.Find(attendee.Id);
            if (dbAttendee == null)
            {
                _context.Add(attendee);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetAttendee), new { id = attendee.Id }, attendee);
            }
            else
            {
                return Conflict();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAttendee(int id)
        {
            var attendee = _context.Attendees.Find(id);
            if (attendee == null)
            {
                return NotFound();
            }

            _context.Remove(attendee);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
