using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orient.Data;
using Orient.Interfaces;
using Orient.Models;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace Orient.Controllers
{
    public class HomeController : Controller
    {
        private IAccountService _accountService;
        private IAccountStatistics _accountStatistics;
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _ctx;
        private readonly ApplicationDbContect _db;
        List<string> answearList = new List<string>();
        public HomeController(ILogger<HomeController> logger,IHttpContextAccessor ctx, ApplicationDbContect db, IAccountService accountService, IAccountStatistics accountStatistics)
        {
            _logger = logger;
            _ctx = ctx;
            _db = db;
            _accountService = accountService;
            _accountStatistics = accountStatistics;
        }

        public IActionResult Index()
        {
            ViewBag.Questions = _db.Questions.ToList();
            IEnumerable<Question> model = _db.Questions.Include(n => n.Answers).ToList();
            TempData.Keep("CurrentTest");
            return View(model);
        }
       

        
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [Route("submit")] 
        public IActionResult Submit(IFormCollection iformCollection)
        {
           //Keeps track of whether an answer is correct or not foreach question
            List<bool> correctAns = new List<bool>();

            //Keeps track of the 
            bool correct;

            //Keeps track of the score
            int score = 0;

            //This list keeps the answers who got choosen randomly
            List<Question> questionsGiven = new List<Question>();

            //Keeps track of the given answers 
            List<Answer> currentAnswers = new List<Answer>();

            // List of the id numbers of the questions
            string[] questionAnswers = iformCollection["questionId"];

            // Checking and computing score
            foreach(var qId in questionAnswers)
            {
                questionsGiven.Add(_db.Questions.Where(q => q.QuestionId == int.Parse(qId)).FirstOrDefault());
            }
            foreach(var q in questionAnswers)
            {
               //Getting the correct answer for each question from the database
                Answer answerIdCorrect = _db.Answers.Where(r => r.QuestionId == int.Parse(q)).Where(a => a.Correct == true).FirstOrDefault();
                int givenAnswerId = int.Parse(iformCollection["question_" + q]);
                currentAnswers.Add(_db.Answers.Where(s => s.AnswerId == givenAnswerId).FirstOrDefault());
                if (answerIdCorrect.AnswerId ==givenAnswerId)
                {
                    correct = true;
                    score++;
                }
                else
                {
                    correct= false;
                }
               correctAns.Add(correct);
            }
            
             //Update total engineering stats
            int accountId = (int)HttpContext.Session.GetInt32("id");
            AccountStatistics entry = _accountStatistics.GetByAccountId(accountId);
            int softwareEngineeringAttempts = entry.softwareEngineeringAttempts + 1;
            if(score > 1)
            {
                int softwareEngineeringCompletions = entry.softwareEngineeringCompletions + 1;
                entry.softwareEngineeringCompletions = softwareEngineeringCompletions;
            }
            int softwareEngineeringMean = entry.softwareEnginneringMeanScore + softwareEngineeringAttempts * score;
            entry.softwareEngineeringAttempts = softwareEngineeringAttempts;
            entry.softwareEnginneringMeanScore = softwareEngineeringMean;
            _accountStatistics.Update(entry);
           
           

           
            
            //The results are kept in this viewbag
            ViewBag.A = new Reply() { TotalScore = score, QuestionList = questionsGiven.ToList(), AnswersList = currentAnswers, correctAnswers = correctAns };
            return View("Report");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
        public IActionResult LoginPage()
        {
            return View();
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string username, string password)
        {
            //Login performed 
            var account = _accountService.Login(username, password);
            //If login successfull
            if (account != null)
            {
                //Save username in session
                HttpContext.Session.SetString("username", username);

                //Get user data 
                var accountId = _accountService.getAccountId(username);
                var fullname = _accountService.getFullName(username);
                var education = _accountService.getEducation(username);
                

                //If user data is fetched successfully ,save it to the session
                if (fullname != null && education !=null && accountId !=null)
                {
                    //Update login count
                    AccountStatistics entry=_accountStatistics.GetByAccountId(accountId);
                    int loginc = entry.loginCount +1;
                    entry.loginCount = loginc;
                    _accountStatistics.Update(entry);

                    //Set session variables
                    HttpContext.Session.SetInt32("id", accountId);
                    HttpContext.Session.SetInt32("loginCount", loginc);
                    HttpContext.Session.SetString("fullname", fullname);
                    HttpContext.Session.SetString("education", education);
                    
                    
                }
                else
                {

                    HttpContext.Session.SetInt32("fullname", 0);
                    HttpContext.Session.SetString("fullname", "Unknown");
                    HttpContext.Session.SetString("education", "Unknown");
                }
                return RedirectToAction("welcome");
            }
            else
            {
                ViewBag.msg = "Invalid";
                return View("LoginPage");
            }
            return View();
        }
        [Route("welcome")]
        public IActionResult Welcome()
        {
            ViewBag.username=HttpContext.Session.GetString("username");
            ViewBag.fullname = HttpContext.Session.GetString("fullname");
            ViewBag.education = HttpContext.Session.GetString("education");
            ViewBag.loginCount = HttpContext.Session.GetInt32("loginCount");

            //getting account statistics
            int accountId = (int)HttpContext.Session.GetInt32("id");
            var account = _accountStatistics.GetByAccountId(accountId);

            //calculating total points statistic
            var totalAttempts = 0;
            totalAttempts += account.softwareEnginneringMeanScore;
            totalAttempts += account.dataSciencegMeanScore;
            totalAttempts += account.UXMeanScore;
            totalAttempts += account.gameMeanScore;
            totalAttempts += account.msMeanScore;

            //calculate completed tests
            var completedTests = 0;
            List<int> meanScores = new List<int>();
            meanScores.Add(account.softwareEnginneringMeanScore);
            meanScores.Add(account.dataSciencegMeanScore);
            meanScores.Add(account.UXMeanScore);
            meanScores.Add(account.gameMeanScore);
            meanScores.Add(account.msMeanScore);
            foreach (var score in meanScores)
            {
                if (score > 5)
                {
                    completedTests++;
                }
            }


            //calculating average score statistic
            var averageScore = 0;
            averageScore += account.softwareEnginneringMeanScore;
            averageScore += account.dataSciencegMeanScore;
            averageScore += account.UXMeanScore;
            averageScore += account.gameMeanScore;
            averageScore += account.msMeanScore;
            averageScore = averageScore / completedTests;

           
            //populating ViewBag with data
            ViewBag.totalAttempts = totalAttempts;
            ViewBag.averageScore = averageScore;

            ViewBag.softwareEngineeringAttempts = account.softwareEngineeringAttempts;
            ViewBag.msAttempts = account.msAttempts;
            ViewBag.gameAttempts = account.gameAttempts;
            ViewBag.dsAttempts = account.dataScienceingAttempts;
            ViewBag.uxAttempts = account.UXAttempts;

            ViewBag.msMean = account.msMeanScore;
            ViewBag.seMean = account.softwareEnginneringMeanScore;
            ViewBag.uxMean = account.UXMeanScore;
            ViewBag.gmMean = account.gameMeanScore;
            ViewBag.dsMean = account.dataSciencegMeanScore;
           
            ViewBag.completedTests = completedTests;

            return View("Welcome");
        }
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("fullname");
            HttpContext.Session.Remove("education");
            HttpContext.Session.Remove("loginCount");
          
            return View("LoginPage");
        }

        public IActionResult SoftwareEngineering()
        {
            TempData["CurrentTest"] = "Software Engineering";
            return View();
        }
        public IActionResult DataScience()
        {
            TempData["CurrentTest"] = "data science";
            return View();
        }

        public IActionResult MachineLearning()
        {
            TempData["CurrentTest"] = "machine learning";
            return View();
        }

        public IActionResult UXDesigner()
        {
            TempData["CurrentTest"] = "UX";
            return View();
        }
        public IActionResult GameDev()
        {
            TempData["CurrentTest"] = "game";
            return View();
        }
    }

}