using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JobInterviewQuiz.Helper;
using JobInterviewQuiz.Infrastructure;
using JobInterviewQuiz.Infrastructure.FilterAttribute;
using JobInterviewQuiz.Model.Dtos;
using JobInterviewQuiz.Model.ViewModels;
using JobInterViewQuiz.Dal;

namespace JobInterviewQuiz.Controllers
{
    public class TestController : BaseController
    {
        ITestTypeDal _testTypeDal;
        IQuestionDal _questionDal;
        ITechnologyQuestionDal _technologyQuestionDal;
        ITestsDal _testsDal;
        ITestQuestionsDal _testQuestionsDal;

        public TestController() : this(DalFactory.TestTypeDal(), DalFactory.QuestionDal(), DalFactory.TechnologyQuestionDal(), DalFactory.TestsDal(), DalFactory.TestQuestionsDal())
        {

        }

        public TestController(ITestTypeDal testTypeDal, IQuestionDal questionDal, ITechnologyQuestionDal technologyQuestionDal, ITestsDal testsDal, ITestQuestionsDal testQuestionsDal)
        {
            _testTypeDal = testTypeDal;
            _questionDal = questionDal;
            _technologyQuestionDal = technologyQuestionDal;
            _testsDal = testsDal;
            _testQuestionsDal = testQuestionsDal;
        }

        // GET: Test
        [ValidateQuizUser]
        public ActionResult Index()
        {

            TestViewModel model = new TestViewModel();

            model = FillData(model);


            TestViewModel.ErrorMessage = "";
            TestViewModel.SuccessMessage = "";


            return View(model);
        }



        [HttpPost]
        public ActionResult AddTest(TestViewModel model)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < model.TechQuestionNumber.Count; i++)
                    if (model.TechQuestionNumber[i] > _questionDal.GetAllQuestionsByLevelAndTechnology(model.TestLevel, _technologyQuestionDal.GetTechnologyQuestionByName(model.TechnologyQuestionsList[i]).Id).Count)
                    {
                        model = FillData(model);
                        TestViewModel.ErrorMessage = "There aren't enough questions available, please check Available Questions and try again ! ";
                        TestViewModel.SuccessMessage = "";
                        return View("Index", model);
                    }

                TestViewModel.ErrorMessage = "";
                TestViewModel.SuccessMessage = "Test saved to database !";


                var testTypeId = _testTypeDal.GetTestTypeByType(model.TestType).Id;
                var numberOfQuestions = model.TechQuestionNumber.Sum();

                _testsDal.AddTest(testTypeId, model.TestName, model.TestLevel, numberOfQuestions);

                var testId = _testsDal.GetAllTests().Where(r => r.TestTypeId == testTypeId && r.Name == model.TestName && r.LevelTest == model.TestLevel && r.NumberOfQuestions == numberOfQuestions).ToList()[0].Id;


                for (int i = 0; i < model.TechQuestionNumber.Count; i++)
                {
                    if (model.TechQuestionNumber[i] == 0)
                        continue;

                    var questionList = _questionDal.GetRandomQuestionsByLevelAndTechnology(model.TechQuestionNumber[i], model.TestLevel, _technologyQuestionDal.GetTechnologyQuestionByName(model.TechnologyQuestionsList[i]).Id);
                    foreach (var question in questionList)
                    {
                        _testQuestionsDal.AddTestQuestion(testId, question.Id);
                    }

                }




                model = FillData(model);



                return View("Index", model);
            }

            TestViewModel.ErrorMessage = "The Test Name field is required !";
            TestViewModel.SuccessMessage = "";

            model = FillData(model);

            return View("Index", model);
        }



        public TestViewModel FillData(TestViewModel model)
        {
            List<TestCustom> testList = new List<TestCustom>();

            foreach (var test in _testsDal.GetAllTests())
            {
                testList.Add(new TestCustom
                {
                    Id = test.Id,
                    TestTypeId = (int)test.TestTypeId,
                    Name = test.Name,
                    LevelTest = test.LevelTest,
                    NumberOfQuestions = test.NumberOfQuestions,
                    TestTypeString = _testTypeDal.GetTestTypeById((int)test.TestTypeId).Type,
                    TechValuesList = new List<int>()

                });
            }



            foreach (var test in testList)
            {

                var questionList = new List<Question>();

                foreach (var question in _testQuestionsDal.GetQuestionsIdsByTestId(test.Id))
                {
                    questionList.Add(_questionDal.GetQuestionById(question));
                }

                foreach (var tech in _technologyQuestionDal.GetAllTechnologyQuestions())
                {
                    //test.TechValuesList.Add(_testQuestionsDal.get _questionDal.GetAllQuestionsByLevelAndTechnology(test.LevelTest,tech.Id).Count);
                    test.TechValuesList.Add(questionList.Where(r => r.LevelQuestion == test.LevelTest && r.QuestionTechnologyID == tech.Id).Count());
                }
            }

            model = new TestViewModel
            {
                TestTypeList = _testTypeDal.GetAllTestType(),
                TestLevelList = _questionDal.GetAllQuestions().Select(r => r.LevelQuestion).Distinct().ToList(),
                TechnologyQuestionsList = _technologyQuestionDal.GetAllTechnologyQuestions().Select(r => r.TechnologyQuestionText).ToList(),
                TestList = testList


            };


            var rows = model.TechnologyQuestionsList.Count;
            var columns = model.TestLevelList.Count;

            model.QuestionGrid = new int[rows, columns];


            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                {
                    model.QuestionGrid[i, j] = _questionDal.GetAllQuestionsByLevelAndTechnology(model.TestLevelList[j], _technologyQuestionDal.GetTechnologyQuestionByName(model.TechnologyQuestionsList[i]).Id).Count;
                }


            return model;
        }

        [HttpPost]
        public void DeleteTest(int id)
        {
            _testsDal.DeleteTestById(id);
        }

    }
}