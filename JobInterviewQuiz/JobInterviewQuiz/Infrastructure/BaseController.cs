using System.Web.Mvc;

namespace JobInterviewQuiz.Infrastructure
{
    public class BaseController : Controller
    {
        public QuizUser QuizUser
        {
            get
            {
                return this.Session["QuizUser"] as QuizUser;
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return this.QuizUser != null;
            }
        }

        public bool? WasTestStarted
        {
            get
            {
                return ((bool?)this.Session["WasTestStarted"]);
            }
        }

        public ActionResult RedirectToLogInPage()
        {
            this.Session.Abandon();
            return RedirectToAction("Login", "User");
        }
    }
}