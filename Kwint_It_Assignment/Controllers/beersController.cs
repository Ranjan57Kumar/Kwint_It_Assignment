using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kwint_It_Assignment;

namespace Kwint_It_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class beersController : ControllerBase
    {
        private readonly beersContext _context;

        public beersController(beersContext context)
        {
            _context = context;
        }


        // GET /api/beers?gtAlcoholByVolume=5.0&ltAlcoholByVolume=8.0
        [HttpGet]
        public ActionResult<IEnumerable<beer>> GetBeers(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume)
        {
            IQueryable<beer> query = _context.beers;

            if (gtAlcoholByVolume != null)
                query = query.Where(b => b.PercentageAlcoholByVolume > gtAlcoholByVolume);

            if (ltAlcoholByVolume != null)
                query = query.Where(b => b.PercentageAlcoholByVolume < ltAlcoholByVolume);

            return query.ToList();
        }

        // GET: api/beers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<beer>> Getbeer(int id)
        {
            var beer = await _context.beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            return beer;
        }


        // PUT: api/beers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putbeer(int id, beer beer)
        {
            if (id != beer.Id)
            {
                return BadRequest();
            }

            _context.Entry(beer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!beerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/beers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<beer>> Postbeer(beer beer)
        {
            _context.beers.Add(beer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getbeer", new { id = beer.Id }, beer);
        }


        // DELETE: api/beers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletebeer(int id)
        {
            var beer = await _context.beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            _context.beers.Remove(beer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool beerExists(int id)
        {
            return _context.beers.Any(e => e.Id == id);
        }
    }
}
