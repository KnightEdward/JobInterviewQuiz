namespace JobInterviewQuiz.Model.Dtos
{
   
    public partial class TestQuestion
    {
        public int Id { get; set; }

        public int TestId { get; set; }

        public int QuestionID { get; set; }

        public virtual Question Question { get; set; }

        public virtual Test Test { get; set; }
    }
}
