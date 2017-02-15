using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartLMS.Models
{
    public class Question
    {
        [Required]
        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }
        
        [Required]
        public int QuestionId { get; set; }
        [Required]

        public string QuestionText { get; set; }
        public virtual List<AnswerChoice> AnswerChoices { get; set; }
    }
}