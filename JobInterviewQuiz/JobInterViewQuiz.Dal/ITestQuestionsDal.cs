using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobInterviewQuiz.Model.Dtos;

namespace JobInterViewQuiz.Dal
{
    public interface ITestQuestionsDal
    {
        //get data
        List<int> GetQuestionsIdsByTestId(int testId);

        List<TestQuestion> GetAllTestQuestions();
        TestQuestion GetTestQuestionById(int id);


        //add data
        void AddTestQuestion(int testId, int QuestionID);

        //update data
        void UpdateTestQuestion(int id, int testId, int QuestionID);

        //delete data
        void DeleteTestQuestion(int id);

    }
}
