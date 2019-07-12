using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rest.API.Framework.Models
{
    public class Whiskey
    {
        [Key]
        public String WhiskeyId { get; set; }
        [Required]
        public String Name { get; set; }
        public List<Assay> Assays { get; set; }
    }
}