using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JobInterviewQuiz.Dal.Sql;
using JobInterviewQuiz.Model.Dtos;

namespace JobInterviewQuiz.Controllers
{
    public class QuestionAdminController : Controller
    {

        QuestionDal _questionDal = new QuestionDal();


        // GET: Home
        [HttpGet]
        public ActionResult SearchQuestionAdmin()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SearchQuestionAdmin(string prefix)
        {
            //Note : you can bind same list from database

            List<Question> allQuestionList = new List<Question>();
            allQuestionList = _questionDal.GetAllQuestions();

            //Searching records from list using LINQ query
            var Name = (from questionText in allQuestionList
                        where questionText.Text_Question.StartsWith(prefix)
                        select new { questionText.Text_Question });
            return Json(Name, JsonRequestBehavior.AllowGet);
        }
    }
}