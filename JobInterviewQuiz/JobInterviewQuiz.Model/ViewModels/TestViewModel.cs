using System.Collections.Generic;
using JobInterviewQuiz.Model.Dtos;
using Microsoft.Build.Framework;

namespace JobInterviewQuiz.Model.ViewModels
{
    public class TestViewModel
    {

        public List<TestType> TestTypeList { get; set; }
        public List<string> TestLevelList { get; set; }
        public List<string> TechnologyQuestionsList { get; set; }
        public List<TestCustom> TestList { get; set; }

        public static string ErrorMessage { get; set; }
        public static string SuccessMessage { get; set; }

        public int[,] QuestionGrid { get; set; }

        [Required]
        public string TestName { get; set; }

        [Required]
        public string TestType { get; set; }

        [Required]
        public string TestLevel { get; set; }

        public int TestId { get; set; }

        public List<int> TechQuestionNumber { get; set; }
    }
}
