using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobInterviewQuiz.Model.Dtos;

namespace JobInterviewQuiz.Model.ViewModels
{
    public class TestModel
    {
        public List<Question> Questions { get; set; }

        public Question Question { get; set; }

        public List<User> UserList { get; set; }

        public List<TechnologyQuestion> TechnologyQuestionList { get; set; }

        public List<TestQuestion> TestQuestionsList { get; set; }

        public List<TestQuestionsAnswer> TestQuestionAnswersList { get; set; }

        public List<UserTest> UserTestList { get; set; }

        public List<Test> TestList { get; set; }

        public List<TestType> TestTypeList { get; set; }


        public int Number { get; set; }
    }
}
