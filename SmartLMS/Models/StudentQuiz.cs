using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLMS.Models
{
    public class StudentQuiz
    {
        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }
        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; }
        public double Marks { get; set; }
    }
}