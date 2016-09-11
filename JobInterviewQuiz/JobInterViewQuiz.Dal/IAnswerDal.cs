using System.Collections.Generic;
using JobInterviewQuiz.Model.Dtos;

namespace JobInterViewQuiz.Dal
{
    public interface IAnswerDal
    {
        //get data
        List<Answer> GetAllAnswers();
        List<Answer> GetAnswersByQuestionID(int QuestionID);
        Answer GetCorrectAnswerByQuestionID(int QuestionID);

        //add data
        void AddAnswer(string textAnswer, int QuestionID, bool value);

        //update data
        void UpdateAnswer(int id, string textAnswer, int QuestionID, bool value);

        //delete data
        void DeleteAnswer(int id);


    }
}
