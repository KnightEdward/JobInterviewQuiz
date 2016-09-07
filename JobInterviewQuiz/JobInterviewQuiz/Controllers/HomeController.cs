using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobInterviewQuiz.Dal.Sql;
using JobInterviewQuiz.Model.ViewModels;

namespace JobInterviewQuiz.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            var dal = new QuestionDal();

            var model = new TestModel
            {
                Questions = dal.GetAllQuestions()
            };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}