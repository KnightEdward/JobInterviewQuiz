using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobInterviewQuiz.Model.Dtos
{

    public partial class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            Answers = new HashSet<Answer>();
            TestQuestions = new HashSet<TestQuestion>();
            TestQuestionsAnswers = new HashSet<TestQuestionsAnswer>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Text_Question { get; set; }

        [Required]
        [StringLength(60)]
        public string TypeQuestion { get; set; }

        [Required]
        [StringLength(25)]
        public string LevelQuestion { get; set; }

        public int QuestionTechnologyID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }

        public virtual TechnologyQuestion TechnologyQuestion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestQuestion> TestQuestions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestQuestionsAnswer> TestQuestionsAnswers { get; set; }
    }
}
