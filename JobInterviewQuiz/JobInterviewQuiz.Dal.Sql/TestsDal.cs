using System;
using System.Collections.Generic;
using System.Linq;
using JobInterviewQuiz.Model.Dtos;
using JobInterViewQuiz.Dal;

namespace JobInterviewQuiz.Dal.Sql
{
    public class TestsDal : ITestsDal
    {
        /// <summary>
        /// Returns the list with all the  tests(Test objects)  if there are no tests it returns an empty list.
        /// </summary>
        /// <returns> A list of Test type objects </returns>
        public List<Test> GetAllTests()
        {
            using (QuizEntities context = new QuizEntities())
            {

                return context.Tests.Count() == 0 ? new List<Test>() : context.Tests.ToList();

            }
        }

        /// <summary>
        ///  Return the answer(of type Test)  by a given test ID, if no test was found null is returned.
        /// </summary>
        /// <param name="Id"> the id of the test</param>
        /// <returns> Test object or null </returns>
        public Test GetTestById(int testId)
        {

            using (QuizEntities context = new QuizEntities())
            {

                try
                {
                    return context.Tests.FirstOrDefault(r => r.Id == testId);
                }
                catch (Exception)
                {

                }
            }

            return null;

        }

        /// <summary>
        ///  Return the test string by a given Id, if no test was found "nok" is returned.
        /// </summary>
        /// <param name="Id"> the id of the test</param>
        /// <returns> test string or  "nok" </returns>
        public string GetTestByIdString(int testId)
        {
            using (QuizEntities context = new QuizEntities())
            {

                try
                {
                    return context.Tests.FirstOrDefault(r => r.Id == testId).Name;
                }
                catch (Exception)
                {

                }
            }

            return "nok";
        }

        //add data

        /// <summary>
        ///  Adds a test to database
        /// </summary>
        /// <param name="testTypeId"></param>
        /// <param name="name"></param>
        /// <param name="levelTest"></param>
        /// <param name="numberOfQuestions"></param>
        public void AddTest(int testTypeId, string name, string levelTest, int numberOfQuestions)
        {
            using (QuizEntities context = new QuizEntities())
            {

                Test test = new Test()
                {
                    TestTypeId = testTypeId,
                    Name = name,
                    LevelTest = levelTest,
                    NumberOfQuestions = numberOfQuestions
                };

                context.Tests.Add(test);
                context.SaveChanges();
            }
        }

        //update data

        /// <summary>
        ///  Updates test by given id
        /// </summary>
        /// <param name="id"> Given id </param>
        /// <param name="testTypeId"> Given test type id </param>
        /// <param name="name"> Given name </param>
        /// <param name="levelTest"> Given level test</param>
        /// <param name="numberOfQuestions"> Given number of questions </param>
        public void UpdateTest(int id, int testTypeId, string name, string levelTest, int numberOfQuestions)
        {
            using (QuizEntities context = new QuizEntities())
            {

                var test = context.Tests.FirstOrDefault(r => r.Id == id);

                test.TestTypeId = testTypeId;
                test.Name = name;
                test.LevelTest = levelTest;
                test.NumberOfQuestions = numberOfQuestions;

                context.SaveChanges();

            }
        }

        //delete data
        /// <summary>
        ///  Deletes test by id
        /// </summary>
        /// <param name="id"> Given id </param>
        public void DeleteTestById(int id)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var test = context.Tests.FirstOrDefault(r => r.Id == id);

                context.Tests.Remove(test);
                context.SaveChanges();
            }
        }
    }
}
