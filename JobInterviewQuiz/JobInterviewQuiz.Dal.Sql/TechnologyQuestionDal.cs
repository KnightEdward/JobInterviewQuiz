using System.Collections.Generic;
using System.Linq;
using JobInterviewQuiz.Model.Dtos;
using JobInterViewQuiz.Dal;

namespace JobInterviewQuiz.Dal.Sql
{
    public class TechnologyQuestionDal : ITechnologyQuestionDal
    {

        //get data

        /// <summary>
        ///  Returns a list of Technologyquestion objects or an empty list if none were found.
        /// </summary>
        /// <returns></returns>
        public List<TechnologyQuestion> GetAllTechnologyQuestions()
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.TechnologyQuestions.Count() == 0 ? new List<TechnologyQuestion>() : context.TechnologyQuestions.ToList();
            }
        }

        /// <summary>
        ///  Return a TechnologyQuestion object by a given id or null if none was found.
        /// </summary>
        /// <param name="id"> Give id. </param>
        /// <returns> TechnologyQuestion or null </returns>
        public TechnologyQuestion GetTechnologyQuestionById(int id)
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.TechnologyQuestions.FirstOrDefault(r => r.Id == id);
            }
        }

        /// <summary>
        ///  Return a TechnologyQuestion object by a given text or null if none was found.
        /// </summary>
        /// <param name="id"> Give id. </param>
        /// <returns> TechnologyQuestion or null </returns>
        public TechnologyQuestion GetTechnologyQuestionByName(string inputText)
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.TechnologyQuestions.FirstOrDefault(r => r.TechnologyQuestionText == inputText);
            }
        }

        /// <summary>
        ///  Adds a question technology
        /// </summary>
        /// <param name="givenTechnology"> Given technology </param>
        public void AddTechnologyQuestion(string givenTechnology)
        {
            using (QuizEntities context = new QuizEntities())
            {

                var technologyQuestion = new TechnologyQuestion
                {
                    TechnologyQuestionText = givenTechnology
                };

                context.TechnologyQuestions.Add(technologyQuestion);
                context.SaveChanges();

            }
        }

        /// <summary>
        /// Updates Technology Question by a given id
        /// </summary>
        /// <param name="id"> Question technology id </param>
        public void UpdateTechnologyQuestionById(int id, string givenTechnology)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var technologyQuestion = context.TechnologyQuestions.FirstOrDefault(r => r.Id == id);
                technologyQuestion.TechnologyQuestionText = givenTechnology;

                context.SaveChanges();

            }
        }

        /// <summary>
        ///  Deletes Technology Question by a given id
        /// </summary>
        /// <param name="id"> Question technology id </param>
        public void DeleteTechnologyQuestionById(int id)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var technologyQuestion = context.TechnologyQuestions.FirstOrDefault(r => r.Id == id);

                context.TechnologyQuestions.Remove(technologyQuestion);
                context.SaveChanges();

            }
        }

        /// <summary>
        /// Deletes Technology Question by Name
        /// </summary>
        /// <param name="givenTechnology"> Given Technology question name </param>
        public void DeleteTechnologyQuestionByName(string givenTechnology)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var technologyQuestion = context.TechnologyQuestions.FirstOrDefault(r => r.TechnologyQuestionText == givenTechnology);

                context.TechnologyQuestions.Remove(technologyQuestion);
                context.SaveChanges();

            }
        }


    }
}
