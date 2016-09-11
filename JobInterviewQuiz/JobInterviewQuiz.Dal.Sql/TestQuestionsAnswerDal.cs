using System.Collections.Generic;
using System.Linq;
using JobInterviewQuiz.Model.Dtos;
using JobInterViewQuiz.Dal;

namespace JobInterviewQuiz.Dal.Sql
{
    public class TestQuestionsAnswerDal : ITestQuestionsAnswersDal
    {
        /// <summary>
        /// Gets a list of all the TestQuestionAnswers(objects) or empty list if none were found
        /// </summary>
        /// <returns> List of  TestQuestionAnswers objects or empty list </returns>
        public List<TestQuestionsAnswer> GetAllTestQuestionsAnswers()
        {
            using (QuizEntities context = new QuizEntities())
            {

                return context.TestQuestionsAnswers.Count() == 0 ? new List<TestQuestionsAnswer>() : context.TestQuestionsAnswers.ToList();

            }
        }

        /// <summary>
        ///  Gets a list of TestQuestionAnswers(objects) by a userId,testId and IsReviewQuestion status or an empty list of none were found.
        /// </summary>
        /// <param name="userId"> Given Id </param>
        /// <param name="testId"> Given TestId </param>
        /// <param name="isReviewQuestion"> IsReviewQuestion status </param>
        /// <returns> List of TestQuestionAnswers or empty list </returns>
        public List<TestQuestionsAnswer> GetTestQuestionAnswersForReviewByUser(int userId, int testId, bool isReviewQuestion)
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.TestQuestionsAnswers.Where(r => r.UserID == userId && r.TestId == testId && r.IsReviewQuestion == isReviewQuestion).Count() == 0 ? new List<TestQuestionsAnswer>() : context.TestQuestionsAnswers.Where(r => r.UserID == userId && r.TestId == testId && r.IsReviewQuestion == isReviewQuestion).ToList();
            }
        }


        /// <summary>
        ///  Returns a TestQuestionAnswer by a given id or null if nothing was found
        /// </summary>
        /// <param name="id"> the object id</param>
        /// <returns> Returns TestQuestionAnswers object or null  </returns>
        public TestQuestionsAnswer GetTestQuestionsAnswersById(int id)
        {
            using (QuizEntities context = new QuizEntities())
            {

                return context.TestQuestionsAnswers.FirstOrDefault(r => r.Id == id);

            }

        }

        /// <summary>
        ///  Gets a list of TestQuestionAnswers(objects) found by a testID or empty list if none were found
        /// </summary>
        /// <param name="testId"></param>
        /// <returns> List of TestQuestionAnswers(objects) or empty list</returns>
        public List<TestQuestionsAnswer> GetAllTestQuestionAnswersByTestId(int testId)
        {
            using (QuizEntities context = new QuizEntities())
            {

                return context.TestQuestionsAnswers.Where(r => r.TestId == testId).Count() == 0 ? new List<TestQuestionsAnswer>() : context.TestQuestionsAnswers.Where(r => r.TestId == testId).ToList();

            }
        }

        public void AddQuestionsAnswersDal(int QuestionID, int testId, int answerId, int userId, bool isReviewQuestion)
        {
            using (QuizEntities context = new QuizEntities())
            {
                TestQuestionsAnswer _testQuestionsAnswer = new TestQuestionsAnswer()
                {
                    QuestionID = QuestionID,
                    TestId = testId,
                    AnswerId = answerId,
                    UserID = userId,
                    IsReviewQuestion = isReviewQuestion
                };
                context.TestQuestionsAnswers.Add(_testQuestionsAnswer);
                context.SaveChanges();
            }
        }

        public void UpdateQuestionAnswersDal(int QuestionID, int testId, int answerId, int userId, bool isReviewQuestion)
        {
            using (QuizEntities context = new QuizEntities())
            {
                TestQuestionsAnswer _testQuestionsAnswer = context.TestQuestionsAnswers.FirstOrDefault(r => r.QuestionID == QuestionID);

                _testQuestionsAnswer.AnswerId = answerId;
                _testQuestionsAnswer.UserID = userId;
                _testQuestionsAnswer.IsReviewQuestion = isReviewQuestion;

                context.SaveChanges();
            }
        }
        /// <summary>
        ///  Adds questions with answers given by the user.
        /// </summary>
        /// <param name="QuestionID"> Given QuestionID</param>
        /// <param name="testId"> Given TestID </param>
        /// <param name="answersId"> Given AnswerID </param>
        public void AddQuestionsAnswersDal(int QuestionID, int testId, List<int> answersId, int userId, bool isReviewQuestion)
        {
            using (QuizEntities context = new QuizEntities())
            {
                TestQuestionsAnswer _testQuestionsAnswer = new TestQuestionsAnswer()
                {
                    QuestionID = QuestionID,
                    TestId = testId,
                    UserID = userId,
                    IsReviewQuestion = isReviewQuestion

                };

                foreach (var answerId in answersId)
                {
                    _testQuestionsAnswer.AnswerId = answerId;
                    context.TestQuestionsAnswers.Add(_testQuestionsAnswer);
                    context.SaveChanges();
                }

            }
        }

        /// <summary>
        ///  Updates questions with answers given by the user by deleting and then adding the test questions with answers.
        /// </summary>
        /// <param name="QuestionID"> Given QuestionID</param>
        /// <param name="testId"> Given TestID </param>
        /// <param name="answersId"> Given AnswerID </param>
        public void UpdateQuestionAnswersDal(int QuestionID, int testId, List<int> answersId, int userId, bool isReviewQuestion)
        {

            using (QuizEntities context = new QuizEntities())
            {
                var _testQuestionsAnswerList = context.TestQuestionsAnswers.Where(r => r.QuestionID == QuestionID && r.TestId == testId && r.UserID == userId).ToList();

                context.TestQuestionsAnswers.RemoveRange(_testQuestionsAnswerList);
                context.SaveChanges();


            }

            AddQuestionsAnswersDal(QuestionID, testId, answersId, userId, isReviewQuestion);
        }


        public List<TestQuestionsAnswer> GetTestQuestionAnswersbyUser(int userId, int testId, int QuestionID)
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.TestQuestionsAnswers.Where(r => r.UserID == userId && r.TestId == testId && r.QuestionID == QuestionID).ToList();
            }
        }

        //delete data
        /// <summary>
        /// Deletes a test question by a given id.
        /// </summary>
        /// <param name="id"> Given id. </param>
        public void DeleteTestQuestionsAnswersById(int id)
        {
            using (QuizEntities context = new QuizEntities())
            {

                var testQuestionAnswer = context.TestQuestionsAnswers.FirstOrDefault(r => r.Id == id);

                context.TestQuestionsAnswers.Remove(testQuestionAnswer);
                context.SaveChanges();

            }
        }

        /// <summary>
        /// Deletes test questions by a given user id.
        /// </summary>
        /// <param name="userId"> Given User Id. </param>
        public void DeleteTestQuestionsAnswersByUserId(int userId)
        {
            using (QuizEntities context = new QuizEntities())
            {

                var testQuestionsAnswers = context.TestQuestionsAnswers.Where(r => r.UserID == userId).ToList();

                context.TestQuestionsAnswers.RemoveRange(testQuestionsAnswers);
                context.SaveChanges();

            }
        }

        /// <summary>
        /// Deletes test questions by a given user id and test id.
        /// </summary>
        /// <param name="userId"> Given userId </param>
        /// <param name="testId"> Given testId </param>
        public void DeleteTestQuestionsAnswersByUserAndTestId(int userId, int testId)
        {
            using (QuizEntities context = new QuizEntities())
            {

                var testQuestionsAnswers = context.TestQuestionsAnswers.Where(r => r.UserID == userId && r.TestId == testId).ToList();

                context.TestQuestionsAnswers.RemoveRange(testQuestionsAnswers);
                context.SaveChanges();

            }
        }
    }
}
