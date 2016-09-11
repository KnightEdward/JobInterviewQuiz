using System;
using System.Web.Mvc;
using System.Web.Security;
using JobInterviewQuiz.Dal.Sql;
using JobInterviewQuiz.Helper;
using JobInterviewQuiz.Infrastructure;
using JobInterviewQuiz.Model.ViewModels;
using JobInterviewQuiz.Security;
using JobInterViewQuiz.Dal;

namespace JobInterviewQuiz.Controllers
{
    public class UserController : BaseController
    {
        IUserDal _userDal;
        IUserTestDal _userTestDal;

        public UserController()
            : this(DalFactory.UserDal(), DalFactory.UserTestDal())
        { }

        public UserController(IUserDal userDal, IUserTestDal userTestDal)
        {
            _userDal = userDal;
            _userTestDal = userTestDal;
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (this.QuizUser == null)
                return View(new UserViewModel());
            return RedirectToAction("Question", "Quiz", new { QuestionID = this.QuizUser.CurrentQuestionID });
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var viewModel = new UserViewModel();

            viewModel.Username = username;

            using (QuizEntities context = new QuizEntities())
            {
                context.Database.Exists();
                if (context != null)
                {
                    if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                    {
                        viewModel.ErrorMessage = "Incorrect username and/or password!";
                        return View(viewModel);
                    }

                    var user = _userDal.GetUserByUsername(username);

                    if (user != null && user.ActivStatus)
                    {
                        if (user.Password.Equals(SHA512Encrypter.Encrypt(password), StringComparison.OrdinalIgnoreCase))
                        {
                            FormsAuthentication.SetAuthCookie(user.Username, false);
                            var quizUser = new QuizUser
                            {
                                Id = user.Id,
                                Username = user.Username,
                                IsAdmin = user.IsAdmin
                            };
                            if (!user.IsAdmin)
                            {
                                quizUser.TestId = _userTestDal.GetTestIdByUserId(user.Id);
                            }

                            this.Session.Add("QuizUser", quizUser);

                            if (user.IsAdmin)
                            {
                                return RedirectToAction("StartPageAdmin", "Admin");
                            }

                            return RedirectToAction("StartPage", "Quiz");
                        }
                    }
                    viewModel.ErrorMessage = "Incorrect username and/or password!";
                }
                else
                {
                    viewModel.ErrorMessage = "You are not connect to database";
                }
            }
            return View(viewModel);
        }
    }
}