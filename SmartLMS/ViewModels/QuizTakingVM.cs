using SmartLMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLMS.ViewModels
{
    public class QuizTakingVM
    {
        public Question Question
        {
            get
            {
                if (QuestionId != 0)
                {
                    var eManager = new ApplicationDbContext();

                    return eManager.Questions.Find(QuestionId);
                }
                return null;
            }
        }

        public int QuestionId { get; set; }
        public int Choice { get; set; }

        public QuizTakingVM()
        {

            var eManager = new ApplicationDbContext();

            eManager.Questions.Find(QuestionId);
        }
    }
}