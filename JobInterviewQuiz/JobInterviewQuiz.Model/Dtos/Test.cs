using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobInterviewQuiz.Model.Dtos
{
  
    public partial class Test
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Test()
        {
            TestQuestions = new HashSet<TestQuestion>();
            TestQuestionsAnswers = new HashSet<TestQuestionsAnswer>();
            UserTests = new HashSet<UserTest>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(25)]
        public string LevelTest { get; set; }

        public int? TestTypeId { get; set; }

        public int NumberOfQuestions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestQuestion> TestQuestions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestQuestionsAnswer> TestQuestionsAnswers { get; set; }

        public virtual TestType TestType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserTest> UserTests { get; set; }
    }
}
