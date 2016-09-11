using System.Collections.Generic;
using System.Linq;
using JobInterviewQuiz.Model.Dtos;
using JobInterViewQuiz.Dal;

namespace JobInterviewQuiz.Dal.Sql
{
    public class TestQuestionsDal : ITestQuestionsDal
    {
        //get data
        /// <summary>
        ///  Gets all test questions or an empty list if none were found.
        /// </summary>
        /// <returns> TestQuestions list or an empty one </returns>
        public List<TestQuestion> GetAllTestQuestions()
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.TestQuestions.Count() == 0 ? new List<TestQuestion>() : context.TestQuestions.ToList();
            }
        }

        public List<int> GetQuestionsIdsByTestId(int testId)
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.TestQuestions.Where(q => q.TestId == testId).Select(q => q.QuestionID).ToList();
            }
        }

        /// <summary>
        ///  Gets a TestQuestion object by a given id or null if none was found.
        /// </summary>
        /// <returns> TetsQuestion  object or null. </returns>
        public TestQuestion GetTestQuestionById(int id)
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.TestQuestions.FirstOrDefault(r => r.Id == id);
            }
        }

        //add data
        /// <summary>
        ///  Adds a TestQuestion to database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="testId"></param>
        /// <param name="QuestionID"></param>
        public void AddTestQuestion(int testId, int QuestionID)
        {
            using (QuizEntities context = new QuizEntities())
            {

                var testQuestion = new TestQuestion
                {
                    TestId = testId,
                    QuestionID = QuestionID
                };

                context.TestQuestions.Add(testQuestion);
                context.SaveChanges();

            }
        }

        //update data
        /// <summary>
        ///  Updates a testQuestion by a given Id.
        /// </summary>
        /// <param name="id"> Given Id. </param>
        /// <param name="testId"> Given TestId. </param>
        /// <param name="QuestionID"> Given QuestionID. </param>
        public void UpdateTestQuestion(int id, int testId, int QuestionID)
        {
            using (QuizEntities context = new QuizEntities())
            {

                var testQuestion = context.TestQuestions.FirstOrDefault(r => r.Id == id);

                testQuestion.TestId = testId;
                testQuestion.QuestionID = QuestionID;

                context.SaveChanges();

            }
        }

        //delete data
        /// <summary>
        /// Deletes a test question by a given id.
        /// </summary>
        /// <param name="id"> Given Id. </param>
        public void DeleteTestQuestion(int id)
        {

            using (QuizEntities context = new QuizEntities())
            {

                var testQuestion = context.TestQuestions.FirstOrDefault(r => r.Id == id);

                context.TestQuestions.Remove(testQuestion);
                context.SaveChanges();

            }

        }
    }
}
