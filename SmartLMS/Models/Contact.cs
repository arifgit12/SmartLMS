using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLMS.Models
{
    public class Contact
    {

        [Required]
        public string ContactName { get; set; }

        [Key]
        [Required]
        public string ContactEmail { get; set; }
        [Required]
        public string ContactMessage { get; set; }
    }
}