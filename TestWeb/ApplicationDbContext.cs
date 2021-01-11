using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestWeb.Models;

namespace TestWeb
{
  public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<TestWeb.Models.Profile> Profile { get; set; }
    public DbSet<TestWeb.Models.Movie> Movie { get; set; }
    public DbSet<TestWeb.Models.Comment> Comment { get; set; }
    public DbSet<TestWeb.Models.MovieStatus> MovieStatus { get; set; }
    public DbSet<TestWeb.Models.MovieCollection> MovieCollection { get; set; }
  }
}
