using CountriesManager.WebApI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CountriesManager.Infrasture.DatabaseContext;
using Microsoft.AspNetCore.Cors;
using CountriesManager.Core.Entities;
namespace CountriesManager.WebApI.Controllers
{
    public class CountriesController : CustomControllerBase
    {
        private readonly AppDbContext _context;

        public CountriesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        //[Produces("application/xml")]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            var countries = await _context.Countries
             .OrderBy(temp => temp.CountryName).ToListAsync();
            return countries;
        }

        // GET: api/countries/5
        [HttpGet("{countryID}")]
        public async Task<ActionResult<Country>> GetCountry(Guid countryID)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(temp => temp.CountryId == countryID);

            if (country == null)
            {
                return Problem(detail: "Invalid CountryId", statusCode: 400, title: "Country Search");
                //return BadRequest();
            }

            return country;
        }

        // PUT: api/countries/5
        [HttpPut("{countryID}")]
        public async Task<IActionResult> PutCountry(Guid countryId, [Bind(nameof(Country.CountryId), nameof(Country.CountryName))] Country country)
        {
            if (countryId != country.CountryId)
            {
                return BadRequest(); //HTTP 400
            }

            var existingCountry = await _context.Countries.FindAsync(countryId);
            if (existingCountry == null)
            {
                return NotFound(); //HTTP 404
            }

            existingCountry.CountryName = country.CountryName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(countryId))
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

        // POST: api/countries
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry([Bind(nameof(Country.CountryId), nameof(Country.CountryName))] Country country)
        {
            if (_context.Countries == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cities'  is null.");
            }
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { CountryID = country.CountryId}, country); 
        }

        // DELETE: api/countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(Guid id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound(); //HTTP 404
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent(); //HTTP 200
        }

        private bool CountryExists(Guid id)
        {
            return (_context.Countries?.Any(e => e.CountryId == id)).GetValueOrDefault();
        }

    }
}
 
