using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLMS.Models
{
    public class StudentCourse
    {        
        [Key]
        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; }
       
        [Key]
        [Required]
        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; }
    }
}