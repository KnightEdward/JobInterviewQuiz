using JobInterviewQuiz.Dal.Sql;
using JobInterViewQuiz.Dal;

namespace JobInterviewQuiz.Helper
{
    public static class DalFactory
    {
        public static IQuestionDal QuestionDal()
        {
            return new QuestionDal();
        }

        public static IAnswerDal AnswerDal()
        {
            return new AnswerDal();
        }

        public static ITestQuestionsAnswersDal TestQuestionsAnswersDal()
        {
            return new TestQuestionsAnswerDal();
        }

        public static IUserDal UserDal()
        {
            return new UserDal();
        }

        public static IUserTestDal UserTestDal()
        {
            return new UserTestDal();
        }

        public static ITestQuestionsDal TestQuestionsDal()
        {
            return new TestQuestionsDal();
        }

        public static ITestTypeDal TestTypeDal()
        {
            return new TestTypeDal();
        }

        public static ITechnologyQuestionDal TechnologyQuestionDal()
        {
            return new TechnologyQuestionDal();
        }

        public static ITestsDal TestsDal()
        {
            return new TestsDal();
        }

    }
}