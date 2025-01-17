using Microsoft.AspNetCore.Mvc;
using Restap.Models;

namespace Restap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpeakerController : Controller
{
    private readonly DataContext _context;

    public SpeakerController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Speaker>> GetTests(string? name = null)
    {
        var query = _context.Tests!.AsQueryable();

        if (name != null)
            query = query.Where(x => x.Name != null && x.Name.ToUpper().Contains(name.ToUpper()));

        return query.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<TextReader> GetTest(int id)
    {
        var test = _context.Tests!.Find(id);

        if (test == null)
        {
            return NotFound();
        }

        return Ok(test);
    }

    [HttpPut("{id}")]
    public IActionResult PutTest(int id, Speaker speaker)
    {
        var dbTest = _context.Tests!.AsNoTracking().FirstOrDefault(x => x.Id == speaker.Id);
        if (id != speaker.Id || dbTest == null)
        {
            return NotFound();
        }

        _context.Update(speaker);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    public ActionResult<Test> PostTest(Test test)
    {
        var dbExercise = _context.Tests!.Find(test.Id);
        if (dbExercise == null)
        {
            _context.Add(test);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTest), new { Id = test.Id }, test);
        }
        else
        {
            return Conflict();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTest(int id)
    {
        var test = _context.Tests!.Find(id);
        if (test == null)
        {
            return NotFound();
        }

        _context.Remove(test);
        _context.SaveChanges();

        return NoContent();
    }
}
