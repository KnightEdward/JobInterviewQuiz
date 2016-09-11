using System;
using System.Collections.Generic;

namespace JobInterviewQuiz.Model.ViewModels
{
    public class LastPageViewModel
    {
        public double Points { get; set; }
        public double Percent { get; set; }
        public TimeSpan Timer { get; set; }
        public DateTime InitialDateTime { get; set; }
        public DateTime FinalDateTime { get; set; }
        public List<AnswerStatisticsViewModel> QuestionsAndAnswers { get; set; }


        public class AnswerStatisticsViewModel
        {
            public string questionText { get; set; }
            public List<string> answerCorrectText { get; set; }
            public List<string> aswerPosibleText { get; set; }

        }
    }
}
