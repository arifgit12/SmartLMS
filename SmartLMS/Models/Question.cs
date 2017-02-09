using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLMS.Models
{
    public class Question
    {
        [Required]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        
        [Required]
        public int QuestionId { get; set; }
        [Required]

        public string QuestionText { get; set; }
        public virtual List<AnswerChoice> AnswerChoices { get; set; }
    }
}