﻿using ITB2203Application.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITB2203Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly DataContext _context;

    public TicketsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Ticket>> GetTickets(string? name = null)
    {
        var query = _context.Tickets!.AsQueryable();

        if (name != null)
            query = query.Where(x => x.SessionId != null);

        return query.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<TextReader> GetTicket(int id)
    {
        var ticket = _context.Tickets!.Find(id);

        if (ticket == null)
        {
            return NotFound();
        }

        return Ok(ticket);
    }

    [HttpPut("{id}")]
    public IActionResult PutTicket(int id, Ticket ticket)
    {
        var dbTicket = _context.Tickets!.AsNoTracking().FirstOrDefault(x => x.Id == ticket.Id);
        if (id != ticket.Id || dbTicket == null)
        {
            return NotFound();
        }

        _context.Update(ticket);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    public ActionResult<Ticket> PostTicket(Ticket ticket)
    {
        var dbExercise = _context.Tickets!.Find(ticket.Id);
        if (dbExercise == null)
        {
            _context.Add(ticket);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTicket), new { Id = ticket.Id }, ticket);
        }
        else
        {
            return Conflict();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTicket(int id)
    {
        var ticket = _context.Tickets!.Find(id);
        if (ticket == null)
        {
            return NotFound();
        }

        _context.Remove(ticket);
        _context.SaveChanges();

        return NoContent();
    }
}
