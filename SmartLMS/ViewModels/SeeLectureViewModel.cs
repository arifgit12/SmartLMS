using SmartLMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartLMS.ViewModels
{
    public class SeeLectureViewModel
    {
        [Key]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [Required]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Lecturer UserName")]
        public string LecturerUserName { get; set; }
        public ICollection<Lecture> Lectures { get; set; }
    }
}