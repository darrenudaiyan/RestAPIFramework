using System;
using System.ComponentModel.DataAnnotations;

namespace Rest.API.Framework.Models
{
    public class Assay
    {
        [Key]
        public String AssayId { get; set; }
        [Required]
        public String Name { get; set; }
        public String Percent { get; set; }
        public String SpecificGravity { get; set; }
    }
}