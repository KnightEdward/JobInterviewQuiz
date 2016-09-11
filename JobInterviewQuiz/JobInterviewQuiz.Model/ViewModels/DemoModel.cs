using System.Collections.Generic;

namespace JobInterviewQuiz.Model.ViewModels
{
    public class DemoModel
    {
        public int QuestionID { get; set; }

        public List<int> AnswersIds { get; set; }

        public string Question { get; set; }

        public List<string> Answers { get; set; }

        public string Level { get; set; }

        public string Type { get; set; }

        public string Category { get; set; }

        public List<string> ValueAnswer { get; set; }

        public List<string> TechnologyList { get; set; }

        public List<string> IsCheckDelete { get; set; }

        public bool DispalyModal { get; set; }

        public string Error { get; set; }

        public List<string> QuestionList { get; set; }

        public class AnswersIdText
        {
            public string id;

            public string TextAnswer;
        }
    }
}
