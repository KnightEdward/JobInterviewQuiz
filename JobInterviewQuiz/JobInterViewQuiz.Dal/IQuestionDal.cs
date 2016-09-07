using System.Collections.Generic;
using JobInterviewQuiz.Model.Dtos;

namespace JobInterViewQuiz.Dal
{
    public interface IQuestionDal
    {
        //read data
        List<Question> GetAllQuestions();
        Question GetQuestionById(int Id);
        Question GetRandomQuestion();
        List<Question> GetRandomQuestions(int numberOfQuestions);
        List<string> GetQuestionsString();
        string GetQuestionByIdString(int Id);
        string GetQuestionTypeById(int Id);

        List<Question> GetAllQuestionsByLevel(string levelQuestion);
        List<Question> GetAllQuestionsByQuestionTechnologyID(int questionTechnologyId);
        List<Question> GetAllQuestionsByLevelAndTechnology(string levelQuestion, int questionTechnologyId);


        List<Question> GetRandomQuestionsByLevel(int numberOfQuestions, string levelQuestion);
        List<Question> GetRandomQuestionsByQuestionTechnologyID(int numberOfQuestions, int questionTechnologyId);
        List<Question> GetRandomQuestionsByLevelAndTechnology(int numberOfQuestions, string levelQuestion, int questionTechnologyId);


        //add data
        void AddQuestion(string textQuestion, string typeQuestion, string levelQuestion, int questionTechnologyId);

        //update data
        void UpdateQuestion(int id, string textQuestion, string typeQuestion, string levelQuestion, int questionTechnologyId);

        //delete data
        void DeleteQuestionById(int id);
    }
}
