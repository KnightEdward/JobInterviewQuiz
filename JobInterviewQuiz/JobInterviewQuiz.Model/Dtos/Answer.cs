using System.ComponentModel.DataAnnotations;

namespace JobInterviewQuiz.Model.Dtos
{
    public partial class Answer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Text_Answer { get; set; }

        public int QuestionID { get; set; }

        public bool Value { get; set; }

        public virtual Question Question { get; set; }
    }
}
