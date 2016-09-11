using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JobInterviewQuiz.Dal.Sql;
using JobInterviewQuiz.Infrastructure;
using JobInterviewQuiz.Infrastructure.FilterAttribute;
using JobInterviewQuiz.Model.Dtos;
using JobInterviewQuiz.Model.ViewModels;

namespace JobInterviewQuiz.Controllers
{
    public class TechnologyAdminController : BaseController
    {
        TechnologyQuestionDal _technologyDal = new TechnologyQuestionDal();
        // GET: TechnologyAdmin
        [ValidateQuizUser]
        public ActionResult TechnologyCrud(TecnologyAdminViewModel model)
        {
            model.TechnologyQuestionAdminList = new List<TechnologyQuestion>();
            model.TechnologyQuestionAdminList = _technologyDal.GetAllTechnologyQuestions();
            model.TechnologyText = "";
            return View(model);
        }

        [HttpPost]
        public ActionResult AddTecnology(TecnologyAdminViewModel model)
        {
            model.TechnologyQuestionAdminList = _technologyDal.GetAllTechnologyQuestions();
            if (model.TechnologyQuestionAdminList.Any(p => string.Equals(p.TechnologyQuestionText, model.TechnologyAdminText, StringComparison.OrdinalIgnoreCase)) == false)
            {
                _technologyDal.AddTechnologyQuestion(model.TechnologyAdminText);
                return RedirectToAction("TechnologyCrud");
            }
            ModelState.AddModelError("TechnologyText", "This technology exist!");
            return View("TechnologyCrud", model);
        }

        [HttpPost]
        public ActionResult UpdateTecnology(TecnologyAdminViewModel model)
        {
            var technologyId = _technologyDal.GetAllTechnologyQuestions().Where(p => p.TechnologyQuestionText == model.TechnologyText).FirstOrDefault().Id;
            _technologyDal.UpdateTechnologyQuestionById(technologyId, model.TechnologyAdminText);
            return RedirectToAction("TechnologyCrud");
        }

        [HttpPost]
        public ActionResult DeleteTecnology(TecnologyAdminViewModel model)
        {
            var technologyId = _technologyDal.GetAllTechnologyQuestions().Where(p => p.TechnologyQuestionText == model.TechnologyText).FirstOrDefault().Id;
            _technologyDal.DeleteTechnologyQuestionById(technologyId);
            return RedirectToAction("TechnologyCrud");
        }

    }
}