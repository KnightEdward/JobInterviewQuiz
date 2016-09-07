using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobInterviewQuiz.Model.Dtos
{

    public partial class TechnologyQuestion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TechnologyQuestion()
        {
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }

        [Column("TechnologyQuestion")]
        [Required]
        [StringLength(45)]
        public string TechnologyQuestionText { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
    }
}
