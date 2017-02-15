using System;
using System.ComponentModel.DataAnnotations;

namespace SmartLMS.Models
{
    public class AnswerChoice
    {
        [Key]
        [Required]
        public int AnswerChoiceId { get; set; }
        [Required]
        public string Choices { get; set; }
        [Required]
        public bool IsCorrect { get; set; }
        [Required]
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}