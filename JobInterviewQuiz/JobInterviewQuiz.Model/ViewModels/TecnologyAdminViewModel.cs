using System.Collections.Generic;
using JobInterviewQuiz.Model.Dtos;

namespace JobInterviewQuiz.Model.ViewModels
{
    public class TecnologyAdminViewModel
    {
        public List<TechnologyQuestion> TechnologyQuestionAdminList { get; set; }
        public TechnologyQuestion Technology { get; set; }
        public int TechnologyId { get; set; }
        public string TechnologyText { get; set; }
        public string TechnologyAdminText { get; set; }
    }
}
