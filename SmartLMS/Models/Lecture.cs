using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLMS.Models
{
    public class Lecture
    {
        [Key]
        public int LectureId { get; set; }
        [Required]
        public string LectureName { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }        

        public string ApplicationUserID { get; set; }
        public ApplicationUser User { get; set; }
    }
}