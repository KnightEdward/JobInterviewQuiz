using System.ComponentModel.DataAnnotations.Schema;

namespace JobInterviewQuiz.Model.Dtos
{

    [Table("UserTest")]
    public partial class UserTest
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public int TestId { get; set; }

        public virtual Test Test { get; set; }

        public virtual User User { get; set; }
    }
}
