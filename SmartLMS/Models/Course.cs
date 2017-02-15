using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartLMS.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ScaffoldColumn(false)]
        public int Rating { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<StudentCourse> Enrollments { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}