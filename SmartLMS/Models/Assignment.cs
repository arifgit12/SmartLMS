using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLMS.Models
{
    public class Assignment
    {
        [Key]
        public int AssignmentId { get; set; }
        [Required]
        public string AssignmentName { get; set; }
        [Required]
        public DateTime LastDate { get; set; }
        public Course Course { get; set; }
        [Required]
        public int CourseId { get; set; }
        public ICollection<StudentAssignment> Students { get; set; }
    }
}