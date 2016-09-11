using System.Linq;
using System.Web.Mvc;
using JobInterviewQuiz.Dal.Sql;
using JobInterviewQuiz.Infrastructure;
using JobInterviewQuiz.Infrastructure.FilterAttribute;
using JobInterviewQuiz.Model.ViewModels;
using JobInterviewQuiz.Security;

namespace JobInterviewQuiz.Controllers
{
    public class AdminUserController : BaseController
    {
        UserDal _userDal = new UserDal();
        UserTestDal _userTestDal = new UserTestDal();
        TestsDal _testsDal = new TestsDal();

        [HttpGet]
        [ValidateQuizUser]
        public ActionResult UserAdmin()
        {
            SetView();
            return View();
        }

        [HttpPost]
        public ActionResult EditUser(int id)
        {
            SetView();
            UserAdminViewModel viewModel = new UserAdminViewModel();

            var user = _userDal.GetUserById(id);

            viewModel.Id = id;
            viewModel.Username = user.Username;
            viewModel.Password = user.Password;
            viewModel.Email = user.Email;
            viewModel.ActivStatus = user.ActivStatus;
            viewModel.IsAdmin = user.IsAdmin;

            var testId = _userTestDal.GetTestIdByUserId(id);
            var testName = _testsDal.GetTestById(testId).Name;

            viewModel.TestName = testName;

            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            _userDal.DeleteUserById(id);
            return RedirectToAction("UserAdmin", "AdminUser");
        }

        [HttpPost]
        public ActionResult AddUser(UserAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                _userDal.AddUser(model.Username, SHA512Encrypter.Encrypt(model.Password), model.Email);

                var userId = _userDal.GetUserByUsername(model.Username).Id;
                var tests = _testsDal.GetAllTests();

                int testId = 0;
                foreach (var test in tests)
                {
                    if (test.Name == model.TestName)
                        testId = test.Id;
                }

                _userTestDal.AddUserTest(userId, testId, false);
                return RedirectToAction("UserAdmin", "AdminUser");
            }
            SetView();
            return View("UserAdmin", model);
        }

        [HttpPost]
        public ActionResult UpdateUser(UserAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                _userDal.UpdateUser(model.Id, model.Username, model.Email, model.ActivStatus, model.IsAdmin);

                var testIdFromUserTest = _userTestDal.GetTestIdByUserId(model.Id);
                var userTestId = _userTestDal.GetAllUserTests().Where(p => p.TestId == testIdFromUserTest).First().Id;
                var testId = _testsDal.GetAllTests().Where(p => p.Name == model.TestName).FirstOrDefault().Id;

                _userTestDal.SetTestIdById(userTestId, testId);

                return Json(new { success = true });
            }
            return PartialView("EditUser", model);
        }

        private void SetView()
        {
            ViewBag.Tests = _testsDal.GetAllTests().OrderBy(test => test.Name);
            ViewBag.Users = _userDal.GetAllUsers().OrderBy(user => user.Username);
            ViewData["TestForUser"] = _userTestDal.GetAllUserTests();
        }
    }
}