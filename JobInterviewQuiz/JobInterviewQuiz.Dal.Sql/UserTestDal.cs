using System.Collections.Generic;
using System.Linq;
using JobInterviewQuiz.Model.Dtos;
using JobInterViewQuiz.Dal;

namespace JobInterviewQuiz.Dal.Sql
{
    public class UserTestDal : IUserTestDal
    {

        //get data
        /// <summary>
        /// Gets a list of all the user tests or an empty list if none were found.
        /// </summary>
        /// <returns> List of UserTests or empty list </returns>
        public List<UserTest> GetAllUserTests()
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.UserTests.Count() == 0 ? new List<UserTest>() : context.UserTests.ToList();
            }
        }

        /// <summary>
        /// Returns the TestId by a given UserId or 0 if none was found.
        /// </summary>
        /// <param name="UserId"> Given UserId. </param>
        /// <returns> Test Id or 0 </returns>
        public int GetTestIdByUserId(int UserId)
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.UserTests.Where(p => p.UserID == UserId).FirstOrDefault(r => r.IsFinished == false).TestId;
            }
        }

        //add data

        /// <summary>
        /// Adds user test to database
        /// </summary>
        /// <param name="userId"> Given User Id </param>
        /// <param name="testId"> Given Test Id </param>
        /// <param name="isFinished"> Given status </param>
        public void AddUserTest(int userId, int testId, bool isFinished)
        {
            using (QuizEntities context = new QuizEntities())
            {

                var userTest = new UserTest
                {
                    UserID = userId,
                    TestId = testId,
                    IsFinished = isFinished
                };

                context.UserTests.Add(userTest);
                context.SaveChanges();

            }
        }

        //update data

        /// <summary>
        /// Updates user test
        /// </summary>
        /// <param name="id"> Given Id  </param>
        /// <param name="userId"> Given User Id  </param>
        /// <param name="testId"> Given Test Id </param>
        /// <param name="isFinished"> Given status </param>
        public void UpdateUserTest(int id, int userId, int testId, bool isFinished)
        {

            using (QuizEntities context = new QuizEntities())
            {

                var userTest = context.UserTests.FirstOrDefault(r => r.Id == id);

                userTest.UserID = userId;
                userTest.TestId = testId;
                userTest.IsFinished = isFinished;

                context.SaveChanges();

            }

        }

        /// <summary>
        /// Sets isFinished state by id
        /// </summary>
        /// <param name="id"> Given Id  </param>
        /// <param name="isFinished"> Given state </param>
        public void SetStateById(int id, bool isFinished)
        {

            using (QuizEntities context = new QuizEntities())
            {
                var userTest = context.UserTests.FirstOrDefault(r => r.Id == id);

                userTest.IsFinished = isFinished;
                context.SaveChanges();

            }

        }

        /// <summary>
        /// Sets test id by id
        /// </summary>
        /// <param name="id"> Given id </param>
        /// <param name="testId"> Given test id</param>
        public void SetTestIdById(int id, int testId)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var userTest = context.UserTests.FirstOrDefault(r => r.Id == id);

                userTest.TestId = testId;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Sets isFinished state by user id and test id
        /// </summary>
        /// <param name="userId"> Given user id </param>
        /// <param name="testId"> Given test id </param>
        /// <param name="isFinished"> Given isFinished status </param>
        public void SetStateByUserAndTestId(int userId, int testId, bool isFinished)
        {

            using (QuizEntities context = new QuizEntities())
            {
                var userTest = context.UserTests.FirstOrDefault(r => r.UserID == userId && r.TestId == testId);

                userTest.IsFinished = isFinished;
                context.SaveChanges();

            }

        }

        //delete data

        /// <summary>
        /// Delete user test by id
        /// </summary>
        /// <param name="id"> Given id </param>
        public void DeleteUserTestById(int id)
        {

            using (QuizEntities context = new QuizEntities())
            {

                var userTest = context.UserTests.FirstOrDefault(r => r.Id == id);

                context.UserTests.Remove(userTest);
                context.SaveChanges();

            }

        }

        /// <summary>
        /// Delete user test by user id and test id
        /// </summary>
        /// <param name="userId"> Given user id </param>
        /// <param name="testId"> Given test id </param>
        public void DeleteUserTestByUserAndTestId(int userId, int testId)
        {

            using (QuizEntities context = new QuizEntities())
            {

                var userTest = context.UserTests.FirstOrDefault(r => r.UserID == userId && r.TestId == testId);

                context.UserTests.Remove(userTest);
                context.SaveChanges();

            }

        }

    }
}
