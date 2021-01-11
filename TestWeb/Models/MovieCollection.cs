using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestWeb.Models
{
  public class MovieCollection
  {
    [Key]
    public int MovieCollectionId { get; set; }

    public int MovieId { get; set; }

    public int ProfileId { get; set; }

    public int MovieStatusId { get; set; }

    public virtual Movie Movie { get; set; }
    
    public virtual Profile Profile { get; set; }

    public virtual MovieStatus MovieStatus { get; set; }
  }
}