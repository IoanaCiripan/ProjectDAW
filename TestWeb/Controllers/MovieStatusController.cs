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
    public class MovieStatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovieStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MovieStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieStatus>>> GetMovieStatus()
        {
            return await _context.MovieStatus.ToListAsync();
        }

        // GET: api/MovieStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieStatus>> GetMovieStatus(int id)
        {
            var movieStatus = await _context.MovieStatus.FindAsync(id);

            if (movieStatus == null)
            {
                return NotFound();
            }

            return movieStatus;
        }

        // PUT: api/MovieStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieStatus(int id, MovieStatus movieStatus)
        {
            if (id != movieStatus.MovieStatusId)
            {
                return BadRequest();
            }

            _context.Entry(movieStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieStatusExists(id))
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

        // POST: api/MovieStatus
        [HttpPost]
        public async Task<ActionResult<MovieStatus>> PostMovieStatus(MovieStatus movieStatus)
        {
            _context.MovieStatus.Add(movieStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovieStatus", new { id = movieStatus.MovieStatusId }, movieStatus);
        }

        // DELETE: api/MovieStatus/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieStatus>> DeleteMovieStatus(int id)
        {
            var movieStatus = await _context.MovieStatus.FindAsync(id);
            if (movieStatus == null)
            {
                return NotFound();
            }

            _context.MovieStatus.Remove(movieStatus);
            await _context.SaveChangesAsync();

            return movieStatus;
        }

        private bool MovieStatusExists(int id)
        {
            return _context.MovieStatus.Any(e => e.MovieStatusId == id);
        }
    }
}
