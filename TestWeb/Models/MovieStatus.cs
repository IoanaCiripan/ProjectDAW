
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestWeb.Models
{
  public class MovieStatus
  {
    [Key]
    public int MovieStatusId { get; set; }
    
    public string Name { get; set; }
  }
}