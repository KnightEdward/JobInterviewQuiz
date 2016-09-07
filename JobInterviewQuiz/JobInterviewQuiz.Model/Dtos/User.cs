using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobInterviewQuiz.Model.Dtos
{

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            TestQuestionsAnswers = new HashSet<TestQuestionsAnswer>();
            UserTests = new HashSet<UserTest>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Username { get; set; }

        [Required]
        [StringLength(150)]
        public string Password { get; set; }

        [Required]
        [StringLength(254)]
        public string Email { get; set; }

        public bool ActivStatus { get; set; }

        public bool IsAdmin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestQuestionsAnswer> TestQuestionsAnswers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserTest> UserTests { get; set; }
    }
}
