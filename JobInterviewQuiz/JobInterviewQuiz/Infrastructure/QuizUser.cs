namespace JobInterviewQuiz.Infrastructure
{
    public class QuizUser
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int TestId { get; set; }

        public int CurrentQuestionID { get; set; }

        public bool IsAdmin { get; set; }
    }
}