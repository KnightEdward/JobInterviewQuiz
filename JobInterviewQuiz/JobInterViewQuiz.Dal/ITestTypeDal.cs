using System.Collections.Generic;
using JobInterviewQuiz.Model.Dtos;

namespace JobInterViewQuiz.Dal
{
    public interface ITestTypeDal
    {
        //get data
        List<TestType> GetAllTestType();
        TestType GetTestTypeById(int id);
        TestType GetTestTypeByType(string type);

        //add data
        void AddTestType(string type);

        //update data
        void UpdateTestTypeById(int id, string type);
        void UpdateTestTypeByType(string oldType, string newType);

        //delete data
        void DeleteTestTypeById(int id);
        void DeleteTestTypeByType(string type);
    }
}
