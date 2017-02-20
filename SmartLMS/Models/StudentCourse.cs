using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLMS.Models
{
    public enum EnrollStatus
    {
        Rejected = 0,
        Pending = 1,
        Accepted = 2
    }
    public class StudentCourse
    {
        [Display(Name = "Status")]
        [DefaultValue(EnrollStatus.Accepted)]
        public EnrollStatus Status { get; set; }

        [Key]
        [Required]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
       
        [Key]
        [Required]
        public string StudentId { get; set; }
        public virtual ApplicationUser Student { get; set; }
    }
}