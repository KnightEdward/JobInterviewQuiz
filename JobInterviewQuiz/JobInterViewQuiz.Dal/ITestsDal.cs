using System.Collections.Generic;
using JobInterviewQuiz.Model.Dtos;

namespace JobInterViewQuiz.Dal
{
    public interface ITestsDal
    {
        //read data
        List<Test> GetAllTests();
        Test GetTestById(int testId);
        string GetTestByIdString(int testId);

        //add data
        void AddTest(int testTypeId, string name, string levelTest, int numberOfQuestions);

        //update data
        void UpdateTest(int id, int testTypeId, string name, string levelTest, int numberOfQuestions);

        //delete data
        void DeleteTestById(int id);
    }
}
