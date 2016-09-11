using System.Collections.Generic;
using System.Linq;
using JobInterviewQuiz.Model.Dtos;
using JobInterViewQuiz.Dal;

namespace JobInterviewQuiz.Dal.Sql
{
    public class AnswerDal : IAnswerDal
    {
        //get data

        /// <summary>
        /// Returns the list with all the  answers(Answer objects)  if there are no answers it returns an empty list.
        /// </summary>
        /// <returns> A list of Answer type objects </returns>
        public List<Answer> GetAllAnswers()
        {

            using (QuizEntities context = new QuizEntities())
            {

                return context.Answers.Count() == 0 ? new List<Answer>() : context.Answers.ToList();

            }

        }

        /// <summary>
        /// Returns the list of answers(Answer objects) for a specific question , if there are no answers it returns an empty list.
        /// </summary>
        /// <returns> A list of Answer type objects </returns>
        public List<Answer> GetAnswersByQuestionID(int QuestionID)
        {

            using (QuizEntities context = new QuizEntities())
            {

                return context.Answers.Where(r => r.QuestionID == QuestionID).Count() == 0 ? new List<Answer>() : context.Answers.Where(r => r.QuestionID == QuestionID).ToList();

            }


        }


        /// <summary>
        ///  Return the answer(of type Answer)  by a given question ID, if no answer was found null is returned.
        /// </summary>
        /// <param name="Id"> the id of the question</param>
        /// <returns> Answer object or null </returns>
        public Answer GetCorrectAnswerByQuestionID(int QuestionID)
        {
            using (QuizEntities context = new QuizEntities())
            {

                return context.Answers.FirstOrDefault(r => r.QuestionID == QuestionID && r.Value == true);

            }

        }


        //add data
        /// <summary>
        ///  Add answer to database
        /// </summary>
        /// <param name="textAnswer"> Given Answer text </param>
        /// <param name="QuestionID"> Given Question Id </param>
        /// <param name="value"> Given question value </param>
        public void AddAnswer(string textAnswer, int QuestionID, bool value)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var answer = new Answer
                {
                    Text_Answer = textAnswer,
                    QuestionID = QuestionID,
                    Value = value
                };
                context.Answers.Add(answer);
                context.SaveChanges();
            }
        }

        //update data
        /// <summary>
        ///  Updates an answer after a given Id.
        /// </summary>
        /// <param name="id"> Given Id </param>
        /// <param name="textAnswer"> Given text of the answer </param>
        /// <param name="QuestionID"> Given question Id </param>
        /// <param name="Value"> Given value of the answer </param>
        public void UpdateAnswer(int id, string textAnswer, int QuestionID, bool value)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var answer = context.Answers.FirstOrDefault(r => r.Id == id);
                answer.Text_Answer = textAnswer;
                answer.QuestionID = QuestionID;
                answer.Value = value;

                context.SaveChanges();
            }
        }

        //delete data
        /// <summary>
        /// Deletes an answer after a given Id.
        /// </summary>
        /// <param name="id"> Given Id </param>
        public void DeleteAnswer(int id)
        {
            using (QuizEntities context = new QuizEntities())
            {

                var answer = context.Answers.FirstOrDefault(r => r.Id == id);

                context.Answers.Remove(answer);
                context.SaveChanges();

            }

        }






    }
}
