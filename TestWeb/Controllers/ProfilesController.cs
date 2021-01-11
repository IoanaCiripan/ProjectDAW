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
  public class ProfilesController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public ProfilesController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: api/Profiles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Profile>>> GetProfile()
    {
      return await _context.Profile
        .Include(p => p.MovieCollections)
          .ThenInclude(mc => mc.Movie)
        .Include(p => p.MovieCollections)
          .ThenInclude(mc => mc.MovieStatus)
        .Include(p => p.MovieCollections)
          .ThenInclude(mc => mc.Movie)
            .ThenInclude(m => m.Comments)
        .ToListAsync();
    }

    // GET: api/Profiles/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Profile>> GetProfile(string id)
    {
      List<Profile> Profiles = await _context.Profile
        .Include(p => p.MovieCollections)
          .ThenInclude(mc => mc.Movie)
        .Include(p => p.MovieCollections)
          .ThenInclude(mc => mc.MovieStatus)
        .Include(p => p.MovieCollections)
          .ThenInclude(mc => mc.Movie)
            .ThenInclude(m => m.Comments).ToListAsync();
      Profile profile = Profiles.Where(p => p.UserId == id).FirstOrDefault();

      if (profile == null)
      {
        return NotFound();
      }

      return profile;
    }

    // PUT: api/Profiles/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProfile(int id, Profile profile)
    {
      if (id != profile.ProfileId)
      {
        return BadRequest();
      }

      _context.Entry(profile).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ProfileExists(id))
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

    // POST: api/Profiles
    [HttpPost]
    public async Task<ActionResult<Profile>> PostProfile(Profile profile)
    {
      _context.Profile.Add(profile);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetProfile", new { id = profile.ProfileId }, profile);
    }

    // DELETE: api/Profiles/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Profile>> DeleteProfile(int id)
    {
      var profile = await _context.Profile.FindAsync(id);
      if (profile == null)
      {
        return NotFound();
      }

      _context.Profile.Remove(profile);
      await _context.SaveChangesAsync();

      return profile;
    }

    private bool ProfileExists(int id)
    {
      return _context.Profile.Any(e => e.ProfileId == id);
    }
  }
}
