using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestWeb.Models
{
  public class Profile
  {
    [Key]
    public int ProfileId { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    public string Description { get; set; }
    public string Photo { get; set; }
    public string UserId { get; set; }

    //colectie albume
    public ICollection<MovieCollection> MovieCollections { get; set; }
  }
}