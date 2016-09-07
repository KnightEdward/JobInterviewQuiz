namespace JobInterviewQuiz.Model.Dtos
{

    public partial class TestQuestionsAnswer
    {
        public int Id { get; set; }

        public int QuestionID { get; set; }

        public int TestId { get; set; }

        public int AnswerId { get; set; }

        public bool IsReviewQuestion { get; set; }

        public int? UserID { get; set; }

        public virtual Question Question { get; set; }

        public virtual Test Test { get; set; }

        public virtual User User { get; set; }
    }
}
