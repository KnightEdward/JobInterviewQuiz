using System.Web.Mvc;

namespace JobInterviewQuiz.Infrastructure.FilterAttribute
{
    public class ValidateQuizUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var baseController = filterContext.Controller as BaseController;

            if (!baseController.IsLoggedIn)
            {
                filterContext.Result = baseController.RedirectToLogInPage();
            }
        }
    }
}