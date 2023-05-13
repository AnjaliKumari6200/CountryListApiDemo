using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryItemsController : ControllerBase
    {
        private readonly CountryContext _context;

        public CountryItemsController(CountryContext context)
        {
            _context = context;
        }

        // GET: api/CountryItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryItems>>> GetCountryItems()
        {
          if (_context.CountryItems == null)
          {
              return NotFound();
          }
            return await _context.CountryItems.ToListAsync();
        }

        // GET: api/CountryItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryItems>> GetCountryItems(long id)
        {
          if (_context.CountryItems == null)
          {
              return NotFound();
          }
            var countryItems = await _context.CountryItems.FindAsync(id);

            if (countryItems == null)
            {
                return NotFound();
            }

            return countryItems;
        }

        // PUT: api/CountryItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryItems(long id, CountryItems countryItems)
        {
            if (id != countryItems.ID)
            {
                return BadRequest();
            }

            _context.Entry(countryItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryItemsExists(id))
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

        // POST: api/CountryItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CountryItems>> PostCountryItems(CountryItems countryItems)
        {
          if (_context.CountryItems == null)
          {
              return Problem("Entity set 'CountryContext.CountryItems'  is null.");
          }
            _context.CountryItems.Add(countryItems);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountryItems", new { id = countryItems.ID }, countryItems);
        }

        // DELETE: api/CountryItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryItems(long id)
        {
            if (_context.CountryItems == null)
            {
                return NotFound();
            }
            var countryItems = await _context.CountryItems.FindAsync(id);
            if (countryItems == null)
            {
                return NotFound();
            }

            _context.CountryItems.Remove(countryItems);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryItemsExists(long id)
        {
            return (_context.CountryItems?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
