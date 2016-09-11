using System;
using System.Collections.Generic;
using JobInterviewQuiz.Model.Dtos;

namespace JobInterviewQuiz.Model.ViewModels
{
    public class QuestionViewModel
    {
        public static DateTime StartTimeAndDate { get; private set; }
        public static TimeSpan InputTime { get; private set; }
        public static TimeSpan StartTime { get; private set; }

        static QuestionViewModel()
        {
            InputTime = new TimeSpan(0, 5, 10);
            StartTime = DateTime.Now.TimeOfDay;
            StartTimeAndDate = DateTime.Now;
        }

        public static bool QuizEnded { get; set; }
        public static bool IsLastQuestion { get; set; }

        public int QuestionID { get; set; }
        public string QuestionText { get; set; }

        public int TestId { get; set; }
        public List<Answer> Answers { get; set; }
        public List<int> AnswersIds { get; set; }
        public string ButtonFinishClass { get; set; }
        public string Error { get; set; }
        public string QuestionType { get; set; }
        public List<int> ReviewQuestionID { get; set; }
        public static bool IsReviewMode { get; set; }
        public string ButtonPressed { get; set; }
        public static List<int> ReviewQuestionIDList { get; set; }
        public static int countIdQuestion = 1;
        public static int NumberOfQuestions;
        public TimeSpan TotalTime
        {
            get { return StartTime.Add(InputTime); }
        }


        public TimeSpan RemainingTime
        {
            get { return TotalTime.Subtract(DateTime.Now.TimeOfDay); }
        }



        public double CurrentPoint
        {

            get { return Math.Round(InputTime.Subtract(RemainingTime).TotalSeconds * 100 / InputTime.TotalSeconds, 2); }

        }

    }
}
