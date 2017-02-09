using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLMS.Models
{
    public class Subscribe
    {
        [Key]
        [Required]
        public string Email { get; set; }
    }
}