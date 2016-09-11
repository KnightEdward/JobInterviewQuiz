using System.Collections.Generic;
using JobInterviewQuiz.Model.Dtos;

namespace JobInterViewQuiz.Dal
{
    public interface ITechnologyQuestionDal
    {

        //get data
        List<TechnologyQuestion> GetAllTechnologyQuestions();
        TechnologyQuestion GetTechnologyQuestionById(int id);
        TechnologyQuestion GetTechnologyQuestionByName(string technologyName);

        //add data
        void AddTechnologyQuestion(string givenTechnology);

        //update data
        void UpdateTechnologyQuestionById(int id, string givenTechnology);

        //delete data
        void DeleteTechnologyQuestionById(int id);
        void DeleteTechnologyQuestionByName(string givenTechnology);

    }
}
