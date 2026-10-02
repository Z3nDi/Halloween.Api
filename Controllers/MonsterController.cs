using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Halloween.Api.Models;
using Halloween.Api.Data;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace Halloween.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonstersController : ControllerBase
    {
        private readonly MonsterDbContext _context;
        public MonstersController(MonsterDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Monster>>> GetMonsters()
        {
            return await _context.Monsters.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Monster>> GetMonster(int id)
        {
            var monster = await _context.Monsters.FindAsync(id);
            if (monster == null)
            {
                return NotFound();
            }

            return monster;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonster(int id, Monster monster)
        {
            if (monster is null)
            {
                return BadRequest();
            }

            if (id != monster.Id)
            {
                return BadRequest("ID в URL не збігається з ID в тілі запиту.");
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            _context.Entry(monster).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonsterExists(id))
                {
                    return NotFound($"Монстра з ID {id} не знайдено.");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<Monster>> PostMonster(Monster monster)
        {
            if (monster is null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            _context.Monsters.Add(monster);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMonster), new { id = monster.Id }, monster);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonster(int id)
        {
            var monster = await _context.Monsters.FindAsync(id);
            if (monster == null)
            {
                return NotFound();
            }

            _context.Monsters.Remove(monster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Допоміжний метод для перевірки існування монстра
        private bool MonsterExists(int id)
        {
            return _context.Monsters.Any(e => e.Id == id);
        }
    }
}