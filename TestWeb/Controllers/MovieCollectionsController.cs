using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWeb;
using TestWeb.Models;

namespace TestWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieCollectionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovieCollectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MovieCollections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieCollection>>> GetMovieCollection()
        {
            return await _context.MovieCollection.Include(mc => mc.Movie).Include(mc => mc.MovieStatus).ToListAsync();
        }

        // GET: api/MovieCollections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieCollection>> GetMovieCollection(int id)
        {
            var movieCollection = await _context.MovieCollection.FindAsync(id);

            if (movieCollection == null)
            {
                return NotFound();
            }

            return movieCollection;
        }

        // PUT: api/MovieCollections/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieCollection(int id, MovieCollection movieCollection)
        {
            if (id != movieCollection.MovieCollectionId)
            {
                return BadRequest();
            }

            _context.Entry(movieCollection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieCollectionExists(id))
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

        // POST: api/MovieCollections
        [HttpPost]
        public async Task<ActionResult<MovieCollection>> PostMovieCollection(MovieCollection movieCollection)
        {
            _context.MovieCollection.Add(movieCollection);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                return null;
            }
            
            return CreatedAtAction("GetMovieCollection", new { id = movieCollection.MovieCollectionId }, movieCollection);
        }

        // DELETE: api/MovieCollections/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieCollection>> DeleteMovieCollection(int id)
        {
            var movieCollection = await _context.MovieCollection.FindAsync(id);
            if (movieCollection == null)
            {
                return NotFound();
            }

            _context.MovieCollection.Remove(movieCollection);
            await _context.SaveChangesAsync();

            return movieCollection;
        }

        private bool MovieCollectionExists(int id)
        {
            return _context.MovieCollection.Any(e => e.MovieCollectionId == id);
        }
    }
}
