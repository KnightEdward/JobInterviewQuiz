using System.Collections.Generic;
using JobInterviewQuiz.Model.Dtos;

namespace JobInterViewQuiz.Dal
{
    public interface ITestQuestionsAnswersDal
    {
        //get data
        List<TestQuestionsAnswer> GetAllTestQuestionsAnswers();
        TestQuestionsAnswer GetTestQuestionsAnswersById(int id);
        List<TestQuestionsAnswer> GetAllTestQuestionAnswersByTestId(int testId);
        List<TestQuestionsAnswer> GetTestQuestionAnswersbyUser(int userId, int testId, int QuestionID);

        List<TestQuestionsAnswer> GetTestQuestionAnswersForReviewByUser(int userId, int testId, bool isReviewQuestion);


        //add data
        void AddQuestionsAnswersDal(int QuestionID, int testId, int answerId, int userId, bool isReviewQuestion);
        void AddQuestionsAnswersDal(int QuestionID, int testId, List<int> answersId, int userId, bool isReviewQuestion);

        //update data
        void UpdateQuestionAnswersDal(int QuestionID, int testId, int answerId, int userId, bool isReviewQuestion);
        void UpdateQuestionAnswersDal(int QuestionID, int testId, List<int> answersId, int userId, bool isReviewQuestion);


        //delete data
        void DeleteTestQuestionsAnswersById(int id);
        void DeleteTestQuestionsAnswersByUserId(int userId);
        void DeleteTestQuestionsAnswersByUserAndTestId(int userId, int testId);
    }
}
