using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JobInterviewQuiz.Dal.Sql;
using JobInterviewQuiz.Infrastructure;
using JobInterviewQuiz.Infrastructure.FilterAttribute;
using JobInterviewQuiz.Model.ViewModels;

namespace JobInterviewQuiz.Controllers
{
    public class HomeController : BaseController
    {
        QuestionDal _questionDal = new QuestionDal();
        AnswerDal _answerDal = new AnswerDal();
        TechnologyQuestionDal _technologyDal = new TechnologyQuestionDal();

        [ValidateQuizUser]
        public ActionResult Index(DemoModel model)

        {

            model.Answers = new List<string>
            {
              ""
            };
            model.AnswersIds = new List<int>
            {
                0
            };
            model.ValueAnswer = new List<string>
            {
                ""
            };

            model.TechnologyList =
                _technologyDal.GetAllTechnologyQuestions().Select(r => r.TechnologyQuestionText).ToList();
            model.QuestionList = _questionDal.GetAllQuestions().Select(r => r.Text_Question).ToList();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Autocomplete(string term)
        {
            var result = new List<KeyValuePair<string, string>>();
            var allQuestionList = _questionDal.GetAllQuestions();
            IList<SelectListItem> List = new List<SelectListItem>();
            foreach (var question in allQuestionList)
            {
                List.Add(new SelectListItem { Text = question.Text_Question, Value = question.Id.ToString() });
            }

            foreach (var item in List)
            {
                result.Add(new KeyValuePair<string, string>(item.Value.ToString(), item.Text));
            }
            var result3 = result.Where(s => s.Value.ToLower().Contains(term.ToLower())).Select(w => w).ToList();
            return Json(result3, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetDetail(int id)
        {
            DemoModel model = new DemoModel();
            model.AnswersIds = new List<int>();
            model.Answers = new List<string>();
            model.ValueAnswer = new List<string>();
            model.IsCheckDelete = new List<string>();
            model.TechnologyList = new List<string>();
            // select data by id here display static data;
            var allAnswerByIdQuestion = _answerDal.GetAnswersByQuestionID(id);
            foreach (var answer in allAnswerByIdQuestion)
            {
                model.Answers.Add(answer.Text_Answer);
                model.AnswersIds.Add(answer.Id);
                if (answer.Value == true)
                    model.ValueAnswer.Add("True");
                else
                {
                    model.ValueAnswer.Add("False");
                }
            }
            model.Category = _technologyDal.GetTechnologyQuestionById(Convert.ToInt32(_questionDal.GetQuestionById(id).QuestionTechnologyID)).TechnologyQuestionText;
            model.Type = _questionDal.GetQuestionTypeById(id);
            model.Level = _questionDal.GetQuestionById(id).LevelQuestion;
            model.TechnologyList = _technologyDal.GetAllTechnologyQuestions().Select(r => r.TechnologyQuestionText).ToList();

            return Json(model);
        }

        [HttpPost]
        public ActionResult AddQuestion(DemoModel model)
        {
            var contVal = 0;
            foreach (var valueAns in model.ValueAnswer)
            {
                if (valueAns == "Correct")
                    contVal++;
            }
            if (contVal > 1 && model.Type == "radio")
            {

                model.QuestionList = _questionDal.GetAllQuestions().Select(r => r.Text_Question).ToList();
                model.Question = model.Question;
                model.TechnologyList = new List<string>();
                model.TechnologyList.Add(model.Category);
                model.DispalyModal = true;
                model.Error = "Question type must be check box";
                return View("Index", model);
            }
            //return View();

            int technologyQuestion = _technologyDal.GetTechnologyQuestionByName(model.Category).Id;
            _questionDal.AddQuestion(model.Question, model.Type, model.Level, technologyQuestion);
            int QuestionID =
                _questionDal.GetAllQuestions().FirstOrDefault(p => p.Text_Question == model.Question && p.LevelQuestion == model.Level).Id;
            var contor = 0;
            foreach (var answer in model.Answers)
            {
                var value = model.ValueAnswer[contor];
                var finalValue = false;
                if (value == "Correct")
                {
                    finalValue = true;
                }
                contor++;
                _answerDal.AddAnswer(answer, QuestionID, finalValue);
            }
            // _questionDal.DeleteQuestionById(1);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateQuestion(DemoModel model)
        {

            int QuestionID = model.QuestionID;
            int technologyQuestion = _technologyDal.GetTechnologyQuestionByName(model.Category).Id;
            _questionDal.UpdateQuestion(QuestionID, model.Question, model.Type, model.Level, technologyQuestion);
            var contor = 0;
            foreach (var answer in model.Answers)
            {
                int answerId = model.AnswersIds[contor];
                var finalValue = false;
                if (model.ValueAnswer[contor] == "Correct")
                    finalValue = true;
                _answerDal.UpdateAnswer(answerId, answer, QuestionID, finalValue);
                contor++;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteQuestion(DemoModel model)
        {
            if (model.AnswersIds != null)
                foreach (var answer in model.AnswersIds)
                {
                    _answerDal.DeleteAnswer(answer);
                }
            int QuestionID = model.QuestionID;
            _questionDal.DeleteQuestionById(QuestionID);


            return RedirectToAction("Index");
        }
    }
}