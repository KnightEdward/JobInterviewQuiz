using System.Collections.Generic;
using JobInterviewQuiz.Model.Dtos;

namespace JobInterViewQuiz.Dal
{
    public interface IUserTestDal
    {
        //get data
        List<UserTest> GetAllUserTests();
        int GetTestIdByUserId(int UserId);

        //add data
        void AddUserTest(int userId, int testId, bool isFinished);

        //update data
        void UpdateUserTest(int id, int userId, int testId, bool isFinished);
        void SetStateById(int id, bool state);
        void SetStateByUserAndTestId(int userId, int testId, bool isFinished);

        //delete data
        void DeleteUserTestById(int id);
        void DeleteUserTestByUserAndTestId(int userId, int testId);
    }
}
