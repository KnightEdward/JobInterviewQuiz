using System.Web.Mvc;
using JobInterviewQuiz.Infrastructure;
using JobInterviewQuiz.Infrastructure.FilterAttribute;

namespace JobInterviewQuiz.Controllers
{
    public class AdminController : BaseController
    {
        [HttpGet]
        [ValidateQuizUser]
        public ActionResult StartPageAdmin()
        {
            if (!this.QuizUser.IsAdmin)
                RedirectToLogInPage();
            return View();
        }
    }
}