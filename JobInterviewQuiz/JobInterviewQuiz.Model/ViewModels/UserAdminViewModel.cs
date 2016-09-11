using System.ComponentModel.DataAnnotations;

namespace JobInterviewQuiz.Model.ViewModels
{
    public class UserAdminViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password need minimun 6 characters!")]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool ActivStatus { get; set; }
        public bool IsAdmin { get; set; }
        public string TestName { get; set; }

    }
}
