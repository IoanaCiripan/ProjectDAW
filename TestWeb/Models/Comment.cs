using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestWeb.Models
{
  public class Comment
  {
    [Key]
    public int CommentId { get; set; }
    
    public string Content { get; set; }

    public int UpVoteNumber { get; set; }

    public int DownVoteNumber { get; set; }

    public int MovieId { get; set; }

    public int ProfileId { get; set; }
  }
}