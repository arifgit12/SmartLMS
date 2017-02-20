using SmartLMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLMS.ViewModels
{
    public class CourseViewModel
    {
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [Required]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "The module must have a description!")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [ScaffoldColumn(false)]
        public int Rating { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<StudentCourse> Enrollments { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}