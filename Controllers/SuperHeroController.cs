using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("id")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            SuperHero hero = await _context.SuperHeroes.FindAsync(id);

            if(hero != null)
                return Ok(hero);
            return BadRequest("Hero not found");
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> Post(SuperHero hero)
        {
            await _context.SuperHeroes.AddAsync(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> ChangeHero(SuperHero hero)
        {
            SuperHero dbHero = await _context.SuperHeroes.FindAsync(hero.Id);

            if (dbHero == null)
                return BadRequest("Hero not found");

            dbHero.Id = hero.Id;
            dbHero.Name = hero.Name;
            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.Place = hero.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(SuperHero hero)
        {
            SuperHero dbHero = await _context.SuperHeroes.FindAsync(hero.Id);

            if (dbHero == null)
                return BadRequest("Hero not found");

            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
