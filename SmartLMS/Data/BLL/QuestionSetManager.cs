using SmartLMS.Data.Repository;
using SmartLMS.Models;
using System.Collections.Generic;

namespace SmartLMS.Data.BLL
{
    public class QuizSetManager
    {
        public void CreateQuestion(Question question)
        {
            {
                using (var repository = new CommonRepository<Question>())
                {
                    repository.Create(question);
                }
            }

        }

        public Question GetQuestionByID(int questionID)
        {
            {
                using (var repository = new CommonRepository<Question>())
                {
                    return repository.Get(questionID);
                }
            }

        }

        public void ModifyQuestion(Question question)
        {
            {
                using (var repository = new CommonRepository<Question>())
                {
                    repository.Update(question);
                }
            }

        }
        public void DeleteQuestion(Question question)
        {
            {
                using (var repository = new CommonRepository<Question>())
                {   
                    repository.Delete(question);
                }
            }

        }
                
        public void CreateQuiz(Quiz Quiz)
        {
            {
                using (var repository = new CommonRepository<Quiz>())
                {
                    repository.Create(Quiz);
                }
            }

        }
        
        public void ModifyQuiz(Quiz Quiz)
        {
            {
                using (var repository = new CommonRepository<Quiz>())
                {
                    repository.Update(Quiz);
                }
            }

        }
        
        public void DeleteQuiz(Quiz Quiz)
        {
            {
                using (var repository = new CommonRepository<Quiz>())
                {
                    repository.Delete(Quiz);
                }
            }

        }
        public IEnumerable<Quiz> GetAllQuiz()
        {
            using (var QuizAssign = new CommonRepository<Quiz>())
            {
                return QuizAssign.GetAll();
            }
        }
        public IEnumerable<Question> GetAllQuestions()
        {
            using (var QuestionAssign = new CommonRepository<Question>())
            {
                return QuestionAssign.GetAll();
            }
        }

        public Question GetallQuestion(int id)
        {
            using (var QuestionAssign = new CommonRepository<Question>())
            {
                return QuestionAssign.Get(id);
            }
        }

        public Quiz GetparticularQuiz(int id)
        {
            using (var QuizAssign = new CommonRepository<Quiz>())
            {
                return QuizAssign.Get(id);
            }
        }
    }
}