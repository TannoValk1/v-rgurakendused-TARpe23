﻿using ITB2203Application.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITB2203Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionsController : ControllerBase
{
    private readonly DataContext _context;

    public SessionsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Session>> GetSessions(string? name = null)
    {
        var query = _context.Sessions!.AsQueryable();

        if (name != null)
            query = query.Where(x => x.MovieId != null);


        return query.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<TextReader> GetSession(int id)
    {
        var session = _context.Sessions!.Find(id);

        if (session == null)
        {
            return NotFound();
        }

        return Ok(session);
    }

    [HttpPut("{id}")]
    public IActionResult PutSession(int id, Session session)
    {
        var dbSession = _context.Sessions!.AsNoTracking().FirstOrDefault(x => x.Id == session.Id);
        if (id != session.Id || dbSession == null)
        {
            return NotFound();
        }

        _context.Update(session);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    public ActionResult<Session> PostSession(Session session)
    {
        var dbExercise = _context.Tests!.Find(session.Id);
        if (dbExercise == null)
        {
            _context.Add(session);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSession), new { Id = session.Id }, session);
        }
        else
        {
            return Conflict();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSession(int id)
    {
        var session = _context.Sessions!.Find(id);
        if (session == null)
        {
            return NotFound();
        }

        _context.Remove(session);
        _context.SaveChanges();

        return NoContent();
    }
}
