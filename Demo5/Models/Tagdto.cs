using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo5.Models
{
    public class Tagdto
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }    
        public string Description { get; set; } 
    }
}