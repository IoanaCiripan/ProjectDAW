using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestWeb.Models
{
  public class Movie
  {
    [Key]
    public int MovieId { get; set; }

    public string Title { get; set; }

    public string Duration { get; set; }

    public string Description { get; set; }

    public string TrailerLink { get; set; }

    public string PosterLink { get; set; }

    public ICollection<Comment> Comments { get; set; }
  }
}