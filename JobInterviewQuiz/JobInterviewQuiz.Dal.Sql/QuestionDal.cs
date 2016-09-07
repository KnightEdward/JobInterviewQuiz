using System;
using System.Collections.Generic;
using System.Linq;
using JobInterviewQuiz.Model.Dtos;
using JobInterViewQuiz.Dal;

namespace JobInterviewQuiz.Dal.Sql
{
    public class QuestionDal : IQuestionDal
    {
        /// <summary>
        /// Returns the list of questions(question objects), if there are no questions it returns an empty list.
        /// </summary>
        /// <returns> A list of Question type objects </returns>
        public List<Question> GetAllQuestions()
        {

            using (QuizEntities context = new QuizEntities())
            {

                return context.Questions.Count() == 0 ? new List<Question>() : context.Questions.ToList();

            }

        }


        /// <summary>
        ///  Return the question(of type Question)  by a given Id, if no question was found null is returned.
        /// </summary>
        /// <param name="Id"> the id of the question</param>
        /// <returns> Question object or null </returns>
        public Question GetQuestionById(int Id)
        {
            using (QuizEntities context = new QuizEntities())
            {

                return context.Questions.FirstOrDefault(r => r.Id == Id);
            }

        }

        /// <summary>
        ///  return a list with random Question objects ,if it fails it returns an empty list
        /// </summary>
        /// <param name="numberOfQuestions"> the number of requested Questions</param>
        /// <returns> a list of Question objects or empty list</returns>
        public List<Question> GetRandomQuestions(int numberOfQuestions)
        {

            using (QuizEntities context = new QuizEntities())
            {

                try
                {
                    return context.Questions.OrderBy(r => Guid.NewGuid()).Take(numberOfQuestions).ToList();
                }
                catch (Exception)
                {

                }
            }

            return new List<Question>();

        }


        /// <summary>
        ///  Gets a random question, returns null if it fails
        /// </summary>
        /// <returns> random Question or null </returns>
        public Question GetRandomQuestion()
        {

            using (QuizEntities context = new QuizEntities())
            {

                try
                {
                    return context.Questions.OrderBy(r => Guid.NewGuid()).Take(1).ToList()[0];
                }
                catch (Exception)
                {

                }
            }

            return null;
        }


        /// <summary>
        /// Returns the list of questions(string objects), if there are no questions it returns an empty list.
        /// </summary>
        /// <returns> A list of questions of string type </returns>
        public List<string> GetQuestionsString()
        {
            var questionList = new List<string>();

            using (QuizEntities context = new QuizEntities())
            {

                if (context.Questions.Count() == 0)
                    return new List<string>();
                else
                {
                    try
                    {
                        foreach (var question in context.Questions.ToList())
                        {
                            questionList.Add(question.Text_Question);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }

            }

            return questionList;
        }


        /// <summary>
        ///  Return the question string by a given Id, if no question was found "nok" is returned.
        /// </summary>
        /// <param name="Id"> the id of the question</param>
        /// <returns> question string or  "nok" </returns>
        public string GetQuestionByIdString(int Id)
        {
            using (QuizEntities context = new QuizEntities())
            {

                try
                {
                    return context.Questions.FirstOrDefault(r => r.Id == Id).Text_Question;
                }
                catch (Exception)
                {

                }
            }

            return "nok";
        }


        public string GetQuestionTypeById(int Id)
        {
            using (QuizEntities context = new QuizEntities())
            {
                try
                {
                    return context.Questions.FirstOrDefault(r => r.Id == Id).TypeQuestion;
                }
                catch (Exception)
                {

                }
                return "nok";
            }
        }


        /// <summary>
        ///  Gets a list of questions by a given levelQuestion
        /// </summary>
        /// <param name="levelQuestion"> Given Question Level </param>
        /// <returns> List of Questions </returns>
        public List<Question> GetAllQuestionsByLevel(string levelQuestion)
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.Questions.Where(r => r.LevelQuestion == levelQuestion).ToList();
            }
        }

        /// <summary>
        ///  Gets a list of questions by a given Question Technology Id
        /// </summary>
        /// <param name="levelQuestion"> Given Question Technology Id </param>
        /// <returns> List of Questions </returns>
        public List<Question> GetAllQuestionsByQuestionTechnologyID(int questionTechnologyId)
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.Questions.Where(r => r.QuestionTechnologyID == questionTechnologyId).ToList();
            }
        }

        /// <summary>
        ///  Gets a list of questions by a given Question Technology Id and a Question Level
        /// </summary>
        /// <param name="levelQuestion"> Given Question Level </param>
        /// <param name="questionTechnologyId"> Given Question Technology Id </param>
        /// <returns> List of questions </returns>
        public List<Question> GetAllQuestionsByLevelAndTechnology(string levelQuestion, int questionTechnologyId)
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.Questions.Where(r => r.LevelQuestion == levelQuestion && r.QuestionTechnologyID == questionTechnologyId).ToList();
            }
        }


        /// <summary>
        ///  Gets a list of questions by a given levelQuestion
        /// </summary>
        /// <param name="numberOfQuestions"> Given number of questions </param>
        /// <param name="levelQuestion"> Given Question Level </param>
        /// <returns> List of Questions </returns>
        public List<Question> GetRandomQuestionsByLevel(int numberOfQuestions, string levelQuestion)
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.Questions.Where(r => r.LevelQuestion == levelQuestion).OrderBy(r => Guid.NewGuid()).Take(numberOfQuestions).ToList();
            }
        }

        /// <summary>
        ///  Gets a list of questions by a given Question Technology Id
        /// </summary>
        /// <param name="numberOfQuestions"> Given number of questions </param>
        /// <param name="levelQuestion"> Given Question Technology Id </param>
        /// <returns> List of Questions </returns>
        public List<Question> GetRandomQuestionsByQuestionTechnologyID(int numberOfQuestions, int questionTechnologyId)
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.Questions.Where(r => r.QuestionTechnologyID == questionTechnologyId).OrderBy(r => Guid.NewGuid()).Take(numberOfQuestions).ToList();
            }
        }

        /// <summary>
        ///  Gets a list of questions by a given Question Technology Id and a Question Level
        /// </summary>
        /// <param name="numberOfQuestions"> Given number of questions </param>
        /// <param name="levelQuestion"> Given Question Level </param>
        /// <param name="questionTechnologyId"> Given Question Technology Id </param>
        /// <returns> List of questions </returns>
        public List<Question> GetRandomQuestionsByLevelAndTechnology(int numberOfQuestions, string levelQuestion, int questionTechnologyId)
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.Questions.Where(r => r.LevelQuestion == levelQuestion && r.QuestionTechnologyID == questionTechnologyId).OrderBy(r => Guid.NewGuid()).Take(numberOfQuestions).ToList();
            }
        }

        /// <summary>
        ///  Adds question to database
        /// </summary>
        /// <param name="textQuestion"> Given Question Text </param>
        /// <param name="typeQuestion"> Given Question Type </param>
        /// <param name="levelQuestion"> Given Question Level </param>
        /// <param name="questionTechnologyId"> Given QuestionTechnology Id</param>
        public void AddQuestion(string textQuestion, string typeQuestion, string levelQuestion, int questionTechnologyId)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var question = new Question
                {
                    Text_Question = textQuestion,
                    TypeQuestion = typeQuestion,
                    LevelQuestion = levelQuestion,
                    QuestionTechnologyID = questionTechnologyId
                };

                context.Questions.Add(question);
                context.SaveChanges();

            }
        }

        /// <summary>
        /// Updates question by a given Id
        /// </summary>
        /// <param name="id"> Given question id </param>
        /// <param name="textQuestion"> Given Question Text </param>
        /// <param name="typeQuestion"> Given Question Type </param>
        /// <param name="levelQuestion"> Given Question Level </param>
        /// <param name="questionTechnologyId"> Given Question Technology Id </param>
        public void UpdateQuestion(int id, string textQuestion, string typeQuestion, string levelQuestion, int questionTechnologyId)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var question = context.Questions.FirstOrDefault(r => r.Id == id);

                question.Text_Question = textQuestion;
                question.TypeQuestion = typeQuestion;
                question.LevelQuestion = levelQuestion;
                question.QuestionTechnologyID = questionTechnologyId;

                context.SaveChanges();

            }
        }

        /// <summary>
        ///  Deletes question by a given id
        /// </summary>
        /// <param name="id"> Given question id </param>
        public void DeleteQuestionById(int id)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var question = context.Questions.FirstOrDefault(r => r.Id == id);

                context.Questions.Remove(question);

                context.SaveChanges();
            }
        }
    }
}
