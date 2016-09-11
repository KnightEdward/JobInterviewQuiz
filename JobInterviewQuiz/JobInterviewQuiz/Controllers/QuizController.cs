using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;
using JobInterviewQuiz.Helper;
using JobInterviewQuiz.Infrastructure;
using JobInterviewQuiz.Infrastructure.FilterAttribute;
using JobInterviewQuiz.Model.Dtos;
using JobInterviewQuiz.Model.ViewModels;
using JobInterViewQuiz.Dal;

namespace JobInterviewQuiz.Controllers
{
    public class QuizController : BaseController
    {
        IQuestionDal _questionDal;
        IAnswerDal _answerDal;
        ITestQuestionsAnswersDal _testQuestionsAnswerDal;
        ITestQuestionsDal _testQuestionsDal;
        IUserDal _userDal;

        public QuizController() : this(DalFactory.QuestionDal(), DalFactory.AnswerDal(), DalFactory.TestQuestionsAnswersDal(), DalFactory.TestQuestionsDal(), DalFactory.UserDal())
        {
            
        }

        public QuizController(IQuestionDal questionDal, IAnswerDal answerDal, ITestQuestionsAnswersDal testQuestionsAnswerDal, ITestQuestionsDal testQuestionsDal, IUserDal testUserDal)
        {
            _questionDal = questionDal;
            _answerDal = answerDal;
            _testQuestionsAnswerDal = testQuestionsAnswerDal;
            _testQuestionsDal = testQuestionsDal;
            _userDal = testUserDal;
        }

        // GET: Quiz    
        [ValidateQuizUser]
        public ActionResult StartPage()
        {
            if (WasTestStarted.HasValue)
                return RedirectToAction("Question", "Quiz", new { QuestionID = this.QuizUser.CurrentQuestionID });
               
            return View();
        }

        [HttpPost]
        public ActionResult Question()
        {
            _userDal.SetStateByUsername(QuizUser.Username, false);
            this.Session.Add("WasTestStarted", true);
            return RedirectToAction("Question", "Quiz");
        }

        [HttpGet]
        [ValidateQuizUser]
        public ActionResult Question(int? QuestionID)
        {
            if (!WasTestStarted.HasValue)
                return RedirectToAction("StartPage", "Quiz");
            
            QuestionViewModel viewModel = new QuestionViewModel();

            if (viewModel.RemainingTime.TotalSeconds <= 0)
                return RedirectToAction("Summary");

            Question question;
            if (QuestionID.HasValue)
            {
                if (_testQuestionsDal.GetQuestionsIdsByTestId(QuizUser.TestId).Contains(QuestionID.Value))
                {
                    question = _questionDal.GetQuestionById(QuestionID.Value);
                }
                else
                {
                    question = _questionDal.GetQuestionById(((QuizUser)this.Session["QuizUser"]).CurrentQuestionID);
                }
            }
            else
            {
                question = _questionDal.GetQuestionById(_testQuestionsDal.GetQuestionsIdsByTestId(QuizUser.TestId).OrderBy(r => r).ToList()[0]);
            }


            viewModel.QuestionID = question.Id;
            viewModel.QuestionText = question.Text_Question;
            viewModel.QuestionType = _questionDal.GetQuestionTypeById(question.Id);
            viewModel.Answers = _answerDal.GetAnswersByQuestionID(question.Id);
            viewModel.ButtonFinishClass = "redButtonClass";

            viewModel.ReviewQuestionID = null;

            QuestionViewModel.IsReviewMode = false;

            QuestionViewModel.IsLastQuestion = false;

            QuestionViewModel.NumberOfQuestions = _testQuestionsDal.GetQuestionsIdsByTestId(QuizUser.TestId).Count;

            var answerList = _testQuestionsAnswerDal.GetTestQuestionAnswersbyUser(QuizUser.Id, QuizUser.TestId, question.Id);
            if (answerList != null)
            {
                viewModel.AnswersIds = new List<int>();

                foreach (var answerId in answerList)
                {
                    viewModel.AnswersIds.Add(answerId.AnswerId);
                    if (answerId.IsReviewQuestion)
                        viewModel.ReviewQuestionID = new List<int>();
                }
            }

            ((QuizUser)this.Session["QuizUser"]).CurrentQuestionID = question.Id;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateQuizUser]
        public ActionResult NextQuestion(QuestionViewModel model)
        {
            QuestionViewModel.countIdQuestion += 1;
            if (model.RemainingTime.TotalSeconds <= 0)
                return RedirectToAction("Summary");

            SaveOrUpdateQuestionAnswer(model);

            if (model.ButtonPressed == "Review Questions")
            {


                QuestionViewModel.IsReviewMode = true;
            }

            if (model.ButtonPressed == "Finish")
            {
                QuestionViewModel.QuizEnded = true;
                return RedirectToAction("Summary", "Quiz");
            }

            if (QuestionViewModel.IsLastQuestion && model.AnswersIds != null && !QuestionViewModel.IsReviewMode)
            {
                QuestionViewModel.QuizEnded = true;
                return RedirectToAction("Summary", "Quiz");
            }


            QuestionViewModel viewModel = FillTheViewModelWithCorrectValues(model, true);

            return View("Question", viewModel);
        }

        [HttpPost]
        [ValidateQuizUser]
        public ActionResult PreviousQuestion(QuestionViewModel model)
        {
            QuestionViewModel.countIdQuestion -= 1;
            if (model.RemainingTime.TotalSeconds <= 0)
                return RedirectToAction("Summary");

            SaveOrUpdateQuestionAnswer(model);

            QuestionViewModel viewModel = FillTheViewModelWithCorrectValues(model, false);

            return View("Question", viewModel);
        }

        [HttpGet]
        [ValidateQuizUser]
        public ActionResult Summary()
        {
            if (!WasTestStarted.HasValue)
                return RedirectToAction("StartPage", "Quiz");

            if (WasTestStarted.Value && new QuestionViewModel().RemainingTime.TotalSeconds > 0 && !QuestionViewModel.QuizEnded)
                return RedirectToAction("Question", "Quiz", new { QuestionID = this.QuizUser.CurrentQuestionID });

            var viewModel = new LastPageViewModel();

            double questionTotalPoints = 7;
            double currentScore = 0;

            var questionsIds = _testQuestionsDal.GetQuestionsIdsByTestId(QuizUser.TestId);
            viewModel.QuestionsAndAnswers = new List<LastPageViewModel.AnswerStatisticsViewModel>();

            foreach (var QuestionID in questionsIds)
            {
                var question = _questionDal.GetQuestionById(QuestionID);
                var numberOfCorrectQuestionAnswers = _answerDal.GetAnswersByQuestionID(question.Id).Where(p => p.Value == true).Count();
                var valuePoint = questionTotalPoints / numberOfCorrectQuestionAnswers;
                var obtainedPoints = 0.0;

                var userAnswers = _testQuestionsAnswerDal.GetTestQuestionAnswersbyUser(QuizUser.Id, QuizUser.TestId, QuestionID);
                var questionsAnswers = _answerDal.GetAnswersByQuestionID(QuestionID);

                if (_questionDal.GetQuestionTypeById(QuestionID) != "radio")
                {
                    foreach (var answer in userAnswers)
                    {
                        if (questionsAnswers.Where(p => p.Id == answer.AnswerId).First().Value)
                        {
                            obtainedPoints += valuePoint;
                        }
                        else
                        {
                            obtainedPoints -= valuePoint;
                        }
                    }
                }
                else
                {
                    if (questionsAnswers.Where(p => p.Id == userAnswers.FirstOrDefault().AnswerId).FirstOrDefault().Value)
                    {
                        obtainedPoints += valuePoint;
                    }
                }

                if (obtainedPoints > 0)
                {
                    currentScore += Math.Ceiling(obtainedPoints);
                }

                var userAnswersIds = userAnswers.Select(p => p.AnswerId);
                viewModel.QuestionsAndAnswers.Add(
                    new LastPageViewModel.AnswerStatisticsViewModel
                    {
                        questionText = _questionDal.GetAllQuestions().Where(p => p.Id == question.Id).FirstOrDefault().Text_Question,
                        answerCorrectText = _answerDal.GetAllAnswers().Where(p => p.QuestionID == question.Id && p.Value == true).Select(p => p.Text_Answer).ToList(),
                        aswerPosibleText = _answerDal.GetAllAnswers().Where(p => p.QuestionID == question.Id && userAnswersIds.Contains(p.Id)).Select(p => p.Text_Answer).ToList()
                    });
            }

            viewModel.Points = currentScore;
            viewModel.FinalDateTime = DateTime.Now;
            viewModel.Percent = viewModel.Points * 100 / 70;
            viewModel.InitialDateTime = QuestionViewModel.StartTimeAndDate;
            viewModel.Timer = viewModel.FinalDateTime.TimeOfDay.Subtract(viewModel.InitialDateTime.TimeOfDay);

            //Send email to admin
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";

            var message = new MailMessage();

            var adminEmail = _userDal.GetAllUsers().Where(p => p.IsAdmin).FirstOrDefault().Email;

            message.To.Add(new MailAddress(adminEmail));
            message.From = new MailAddress("jobinterviewquizz@gmail.com");

            message.Subject = "Interview";

            message.Body = "Hey Admin the username " + QuizUser.Username + " got the score  " + currentScore + " and the procentage " + viewModel.Percent;



            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "jobinterviewquizz@gmail.com",  // replace with valid value
                    Password = "Adminadmin"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);

            }
            //

            Logout();

            return View(viewModel);
        }

        private void Logout()
        {
            this.Session.Abandon();
            FormsAuthentication.SignOut();
        }

        private QuestionViewModel FillTheViewModelWithCorrectValues(QuestionViewModel model, bool isNext)
        {
            List<Question> allQuestions = new List<Question>();

            if (QuestionViewModel.IsReviewMode)
            {
                foreach (var QuestionID in QuestionViewModel.ReviewQuestionIDList)
                {
                    allQuestions.Add(_questionDal.GetQuestionById(QuestionID));
                }



            }
            else
            {
                foreach (var QuestionID in _testQuestionsDal.GetQuestionsIdsByTestId(QuizUser.TestId))
                {
                    allQuestions.Add(_questionDal.GetQuestionById(QuestionID));
                }
            }

            QuestionViewModel.NumberOfQuestions = allQuestions.Count;

            allQuestions = allQuestions.OrderBy(r => r.Id).ToList();

            if (!isNext)
            {
                allQuestions = allQuestions.OrderByDescending(r => r.Id).ToList();
                //allQuestions.Reverse();
            }

            List<Question> testQuetionsAfterIndex;
            Question question;

            if (QuestionViewModel.IsLastQuestion && isNext) //is last question
            {

                testQuetionsAfterIndex = allQuestions;

                question = testQuetionsAfterIndex.FirstOrDefault();

                QuestionViewModel.IsLastQuestion = false;

                QuestionViewModel.countIdQuestion = 1;

            }
            else
            {
                testQuetionsAfterIndex = allQuestions.SkipWhile(p => p.Id != model.QuestionID).ToList();

                question = (model.AnswersIds != null)
                      ? testQuetionsAfterIndex.Skip(1).FirstOrDefault()
                      : testQuetionsAfterIndex.FirstOrDefault();
            }

            //var testQuetionsAfterIndex = allQuestions.SkipWhile(p => p.Id != model.QuestionID);

            //var question = (model.AnswersIds != null)
            //      ? testQuetionsAfterIndex.Skip(1).FirstOrDefault()
            //      : testQuetionsAfterIndex.FirstOrDefault();

            var viewModel = new QuestionViewModel();

            viewModel.QuestionType = _questionDal.GetQuestionTypeById(question.Id);
            viewModel.QuestionID = question.Id;
            viewModel.QuestionText = question.Text_Question;
            viewModel.Answers = _answerDal.GetAnswersByQuestionID(question.Id);
            viewModel.AnswersIds = new List<int>();


            ((QuizUser)this.Session["QuizUser"]).CurrentQuestionID = question.Id;

            if (model.AnswersIds != null)
            {
                var answerList = _testQuestionsAnswerDal.GetTestQuestionAnswersbyUser(QuizUser.Id, QuizUser.TestId,
                    question.Id);

                foreach (var answerId in answerList)
                {

                    viewModel.AnswersIds.Add(answerId.AnswerId);
                    //viewModel.IsReviewQuestion = answerId.IsReviewQuestion;
                    viewModel.ReviewQuestionID = answerId.IsReviewQuestion == true ? new List<int>() : null;

                }

            }
            else
            {
                viewModel.Error = "At least one answer required!";
            }

            List<int> questionList = new List<int>();

            if (QuestionViewModel.IsReviewMode)
            {
                questionList = QuestionViewModel.ReviewQuestionIDList;
                questionList.Sort();
            }
            else
            {
                questionList = _testQuestionsDal.GetQuestionsIdsByTestId(QuizUser.TestId);

                questionList.Sort();
            }


            if (question.Id == questionList.LastOrDefault())
            {
                QuestionViewModel.IsLastQuestion = true;
                viewModel.ButtonFinishClass = "greenButtonClass";
                QuestionViewModel.ReviewQuestionIDList = _testQuestionsAnswerDal.GetTestQuestionAnswersForReviewByUser(QuizUser.Id, QuizUser.TestId, true).Select(r => r.QuestionID).Distinct().ToList();

            }
            else
            {
                QuestionViewModel.IsLastQuestion = false;
                viewModel.ButtonFinishClass = "redButtonClass";
            }
            return viewModel;
        }


        private void SaveOrUpdateQuestionAnswer(QuestionViewModel model)
        {
            var isReview = model.ReviewQuestionID == null ? false : true;

            if (model.AnswersIds != null)
            {
                if (_testQuestionsAnswerDal.GetAllTestQuestionAnswersByTestId(QuizUser.TestId)
                         .Where(r => r.QuestionID == model.QuestionID && r.TestId == QuizUser.TestId)
                         .FirstOrDefault() == null)
                {
                    _testQuestionsAnswerDal.AddQuestionsAnswersDal(model.QuestionID, QuizUser.TestId,
                          model.AnswersIds, QuizUser.Id, isReview);
                }
                else
                {
                    _testQuestionsAnswerDal.UpdateQuestionAnswersDal(model.QuestionID, QuizUser.TestId,
                        model.AnswersIds, QuizUser.Id, isReview);
                }
            }


        }

    }
}