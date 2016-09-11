using System.Collections.Generic;
using System.Linq;
using JobInterviewQuiz.Model.Dtos;
using JobInterViewQuiz.Dal;

namespace JobInterviewQuiz.Dal.Sql
{
    public class TestTypeDal : ITestTypeDal
    {
        //get data

        /// <summary>
        /// Gets all existing test types or and empty list if none were found
        /// </summary>
        /// <returns> TestType list or an empty one </returns>
        public List<TestType> GetAllTestType()
        {

            using (QuizEntities context = new QuizEntities())
            {
                return context.TestTypes.Count() == 0 ? new List<TestType>() : context.TestTypes.ToList();
            }

        }

        /// <summary>
        /// Get test type by given id or null if none was found
        /// </summary>
        /// <param name="id"> given Id </param>
        /// <returns> Test Type or null</returns>
        public TestType GetTestTypeById(int id)
        {

            using (QuizEntities context = new QuizEntities())
            {
                return context.TestTypes.FirstOrDefault(r => r.Id == id);
            }

        }

        /// <summary>
        /// Get test type by given type or null if none was found
        /// </summary>
        /// <param name="type"> Given type </param>
        /// <returns> TestType or null </returns>
        public TestType GetTestTypeByType(string type)
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.TestTypes.FirstOrDefault(r => r.Type == type);
            }
        }

        /// <summary>
        /// Adds a test type to database
        /// </summary>
        /// <param name="type"> Given Type </param>
        public void AddTestType(string type)
        {

            using (QuizEntities context = new QuizEntities())
            {

                var testType = new TestType
                {
                    Type = type
                };

                context.TestTypes.Add(testType);
                context.SaveChanges();

            }

        }

        /// <summary>
        /// Updates TestType by id
        /// </summary>
        /// <param name="id"> Given id </param>
        /// <param name="type"> Given type </param>
        public void UpdateTestTypeById(int id, string type)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var testType = context.TestTypes.FirstOrDefault(r => r.Id == id);

                testType.Type = type;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates TestType by existing type
        /// </summary>
        /// <param name="oldType"> Old type </param>
        /// <param name="newType"> New Type </param>
        public void UpdateTestTypeByType(string oldType, string newType)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var testType = context.TestTypes.FirstOrDefault(r => r.Type == oldType);

                testType.Type = newType;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes test type by given id
        /// </summary>
        /// <param name="id"> Given id </param>
        public void DeleteTestTypeById(int id)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var testType = context.TestTypes.FirstOrDefault(r => r.Id == id);

                context.TestTypes.Remove(testType);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes test type by given string
        /// </summary>
        /// <param name="type"> Given string </param>
        public void DeleteTestTypeByType(string type)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var testType = context.TestTypes.FirstOrDefault(r => r.Type == type);

                context.TestTypes.Remove(testType);
                context.SaveChanges();
            }
        }

    }
}
