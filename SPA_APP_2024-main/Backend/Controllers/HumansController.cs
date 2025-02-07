using backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HumansController : ControllerBase
    {
        private readonly DataContext context;
        public HumansController(DataContext c)
        {
            context = c;
        }
        [HttpGet]
        public IActionResult GetHumans()
        {
            var humans = context.HumansList!.AsQueryable();
            return Ok(humans);

        }
        [HttpPost]
        public IActionResult Create([FromBody] Humans e)
        {
            var dbHuman = context.HumansList?.Find(e.Id);
            if (dbHuman == null)
            {
                context.HumansList?.Add(e);
                context.SaveChanges();
                return CreatedAtAction(nameof(GetHumans), new { e.Id }, e);
            }
            return Conflict();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int? id, [FromBody] Humans e)
        {
            var dbHuman = context.HumansList!.AsNoTracking().FirstOrDefault(humanInDB => humanInDB.Id == e.Id);
            if (id != e.Id || dbHuman == null) return NotFound();
            context.Update(e);
            context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var humanToDelete = context.HumansList?.Find(id);
            if (humanToDelete == null) return NotFound();
            context.HumansList?.Remove(humanToDelete);
            context.SaveChanges();
            return NoContent();
        }

    }
}