using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orient.Data;
using Orient.Interfaces;
using Orient.Models;
using System.Diagnostics;
using System.Reflection.Metadata;
using static System.Formats.Asn1.AsnWriter;

namespace Orient.Controllers
{
    public class HomeController : Controller
    {
        private IAccountService _accountService;
        private IAccountStatistics _accountStatistics;
        private IChatAnswer _chatAnswer;
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _ctx;
        private readonly ApplicationDbContect _db;
        List<string> answearList = new List<string>();
        public HomeController(ILogger<HomeController> logger,IHttpContextAccessor ctx, ApplicationDbContect db, IAccountService accountService, IAccountStatistics accountStatistics, IChatAnswer chatAnswer)
        {
            _logger = logger;
            _ctx = ctx;
            _db = db;
            _accountService = accountService;
            _accountStatistics = accountStatistics;
            _chatAnswer = chatAnswer;
        }

        public IActionResult Index()
        {
            ViewBag.Questions = _db.Questions.ToList();
            IEnumerable<Question> model = _db.Questions.Include(n => n.Answers).ToList();
            TempData["TestType"] = "multiple choice";
            if (TempData["CurrentTest"] == "machine learning" || TempData["CurrentTest"] == "DS")
            {
                TempData["TestType"] = "true or false";
            }
            TempData.Keep("CurrentTest");
            return View(model);
        }
        public IActionResult finalTest()
        {
            ViewBag.Questions = _db.Questions.ToList();
            IEnumerable<Question> model = _db.Questions.Include(n => n.Answers).ToList();
            TempData["TestType"] = "multiple choice";
            if (TempData["CurrentTest"] == "machine learning" || TempData["CurrentTest"] == "data science")
            {
                TempData["TestType"] = "true or false";
            }
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
            foreach (var qId in questionAnswers)
            {
                questionsGiven.Add(_db.Questions.Where(q => q.QuestionId == int.Parse(qId)).FirstOrDefault());
            }
            foreach (var q in questionAnswers)
            {
                //Getting the correct answer for each question from the database
                Answer answerIdCorrect = _db.Answers.Where(r => r.QuestionId == int.Parse(q)).Where(a => a.Correct == true).FirstOrDefault();
                int givenAnswerId = int.Parse(iformCollection["question_" + q]);
                currentAnswers.Add(_db.Answers.Where(s => s.AnswerId == givenAnswerId).FirstOrDefault());
                if (answerIdCorrect.AnswerId == givenAnswerId)
                {
                    correct = true;
                    score++;
                }
                else
                {
                    correct = false;
                }
                correctAns.Add(correct);
            }

            //Update total engineering stats
            int completionBarrier = 4;
            int accountId = (int)HttpContext.Session.GetInt32("id");
            AccountStatistics entry = _accountStatistics.GetByAccountId(accountId);
            if (TempData["CurrentTest"].ToString() == "MS") {
                int msAttempts = entry.msAttempts + 1;
                if (score > completionBarrier)
                {
                    int msCompletions = entry.msCompletions + 1;
                    entry.msCompletions = msCompletions;
                }
                int msMean = (entry.msMeanScore * (msAttempts-1) +  score)/msAttempts;
                entry.msAttempts = msAttempts;
                entry.msMeanScore = msMean;
            }
            else if(TempData["CurrentTest"].ToString() == "Software Engineering")
            {
                int softwareEngineeringAttempts = entry.softwareEngineeringAttempts + 1;
                if (score > completionBarrier)
                {
                    int softwareEngineeringCompletions = entry.softwareEngineeringCompletions + 1;
                    entry.softwareEngineeringCompletions = softwareEngineeringCompletions;
                }
                int softwareEngineeringMean = (entry.softwareEnginneringMeanScore * (softwareEngineeringAttempts - 1) + score) / softwareEngineeringAttempts;
                entry.softwareEngineeringAttempts = softwareEngineeringAttempts;
                entry.softwareEnginneringMeanScore = softwareEngineeringMean;
            }
            else if (TempData["CurrentTest"].ToString() == "DS")
            {
                int dsAttempts = entry.dataScienceingAttempts + 1;
                if (score > completionBarrier)
                {
                    int dsCompletions = entry.dataScienceCompletions + 1;
                    entry.dataScienceCompletions = dsCompletions;
                }
                int dsMean = (entry.dataSciencegMeanScore * (dsAttempts - 1) + score) / dsAttempts;
                entry.dataScienceingAttempts = dsAttempts;
                entry.dataSciencegMeanScore = dsMean;
            }else if (TempData["CurrentTest"].ToString() == "UX")
            {
                int uxAttempts = entry.UXAttempts + 1;
                if (score > completionBarrier)
                {
                    int uxCompletions = entry.UXCompletions + 1;
                    entry.UXCompletions = uxCompletions;
                }
                int uxMean = (entry.UXMeanScore * (uxAttempts - 1) + score) / uxAttempts;
                entry.UXAttempts = uxAttempts;
                entry.UXMeanScore = uxMean;
            }else if (TempData["CurrentTest"].ToString() == "GM")
            {
                int gmAttempts = entry.gameAttempts + 1;
                if (score > completionBarrier)
                {
                    int gameCompletions = entry.gameCompletions + 1;
                    entry.gameCompletions = gameCompletions;
                }
                int gmMean = (entry.gameMeanScore * (gmAttempts - 1) + score) / gmAttempts;
                entry.gameAttempts = gmAttempts;
                entry.gameMeanScore = gmMean;
            }

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
                    HttpContext.Session.SetString("Journey", "unlocked");

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
            totalAttempts += account.softwareEngineeringAttempts;
            totalAttempts += account.dataScienceingAttempts;
            totalAttempts += account.UXAttempts;
            totalAttempts += account.gameAttempts;
            totalAttempts += account.msAttempts;

            //calculate completed tests
            var completedTests = 0;
            List<int>  completions= new List<int>();
            completions.Add(account.softwareEngineeringCompletions);
            completions.Add(account.dataScienceCompletions);
            completions.Add(account.UXCompletions);
            completions.Add(account.gameCompletions);
            completions.Add(account.msCompletions);
            foreach (var score in completions)
            {
                if (score >0 )
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
            if (completedTests > 0)
            {
                averageScore = averageScore / completedTests;
            }
            else
            {
                averageScore = 0;
            }
           

           
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

            ViewBag.msCompletions = account.msCompletions;
            ViewBag.seCompletions = account.softwareEngineeringCompletions;
            ViewBag.dsCompletions = account.dataScienceCompletions;
            ViewBag.uxCompletions = account.UXCompletions;
            ViewBag.gmCompletions = account.gameCompletions;

            ViewBag.completedTests = completedTests;
            ViewBag.JourneyLocked = HttpContext.Session.GetString("Journey");
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

        // Course views 
        public IActionResult SoftwareEngineering()
        {
            TempData["CurrentTest"] = "Software Engineering";

            return View();
        }
        public IActionResult DataScience()
        {
            TempData["CurrentTest"] = "DS";
            return View();
        }

        public IActionResult MachineLearning()
        {
            TempData["CurrentTest"] = "MS";
            return View();
        }

        public IActionResult UXDesigner()
        {
            TempData["CurrentTest"] = "UX";
            return View();
        }
        public IActionResult GameDev()
        {
            TempData["CurrentTest"] = "GM";
            return View();
        }
        public IActionResult SuggestionSystem()
        {
            ViewBag.Sectors = _db.DaySectors.ToList();
            IEnumerable<DaySector> model = _db.DaySectors.Include(n => n.Parts).ToList();
            ViewBag.username = HttpContext.Session.GetString("fullname");
          //  ViewBag.username = "Nikoleta Vlachou";
            return View(model);
        }
        [HttpPost]
        public IActionResult Suggest(IFormCollection iformCollection)
        {
            string career = string.Empty;
            double testFactor = 0.3;
            double journeyFactor = 0.7;
            int accountId = (int)HttpContext.Session.GetInt32("id");
            AccountStatistics account = _accountStatistics.GetByAccountId(accountId);
            
            double se = account.softwareEnginneringMeanScore * testFactor;
            double ms = account.msMeanScore* testFactor;
            double ds = account.msCompletions * testFactor;
            double ux = account.UXMeanScore * testFactor;
            double gm = account.gameMeanScore * testFactor;
            // List of the id numbers of the questions
            string[] sectors = iformCollection["sectionId"];

            List<Part> chosenparts = new List<Part>();
            // Checking and computing score
           
            foreach (var q in sectors)
            {
                int givenAnswerId = int.Parse(iformCollection["section_" + q]);
                //Find Part in db
                chosenparts.Add(_db.Parts.Where(p => p.DaySectorId == int.Parse(q)).Where(p => p.PartId == givenAnswerId).FirstOrDefault());
               
            }
            foreach(var part in chosenparts)
            {
                //Score accordingly
                if (part.PartCareer == "SE")
                {
                    se=se + 2*0.7;
                }
                else if(part.PartCareer == "MS")
                {
                    ms = ms + 2 * 0.7;
                }
                else if (part.PartCareer == "DS")
                {
                    ds = ds + 2 * 0.7;
                }
                else if (part.PartCareer == "UX")
                {
                    ux=ux + 2 * 0.7;
                }
                else if (part.PartCareer == "GM")
                {
                    gm=gm + 2 * 0.7;
                }
               
                double[] numbers = new double[5] { se 
                    ,ms,ds,ux,gm };
                double maxNumber = numbers[0]; // Assume the first number is the maximum

                // Loop through the remaining numbers and compare them with the current maximum
                for (int i = 1; i < numbers.Length; i++)
                {
                    maxNumber = Math.Max(maxNumber, numbers[i]);
                }
                TempData["match"] = maxNumber*10;
                if (maxNumber == se)
                {
                    career = "Software Engineer";
                }
                else if (maxNumber == ms)
                {
                    career = "Machine Learning Engineer";
                }
                else if (maxNumber == ux)
                {
                    career = "UX Designer";
                }
                else if (maxNumber == ds)
                {
                    career = "Data Scientist";
                }
                else
                {
                    career = "Game Developer";
                }
            }
            TempData["career"] = career;
            return View("Career");
        }
        public IActionResult Career()
        {
            return View();
        }
        public IActionResult ChatBoard()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FinalSubmit(IFormCollection iformCollection)
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
            foreach (var qId in questionAnswers)
            {
                questionsGiven.Add(_db.Questions.Where(q => q.QuestionId == int.Parse(qId)).FirstOrDefault());
            }
            foreach (var q in questionAnswers)
            {
                //Getting the correct answer for each question from the database
                Answer answerIdCorrect = _db.Answers.Where(r => r.QuestionId == int.Parse(q)).Where(a => a.Correct == true).FirstOrDefault();
                int givenAnswerId = int.Parse(iformCollection["question_" + q]);
                currentAnswers.Add(_db.Answers.Where(s => s.AnswerId == givenAnswerId).FirstOrDefault());
                if (answerIdCorrect.AnswerId == givenAnswerId)
                {
                    correct = true;
                    score++;
                }
                else
                {
                    correct = false;
                }
                correctAns.Add(correct);
            }

            HttpContext.Session.SetString("Journey", "unlocked");
            //The results are kept in this viewbag
            ViewBag.A = new Reply() { TotalScore = score, QuestionList = questionsGiven.ToList(), AnswersList = currentAnswers, correctAnswers = correctAns };
            return View("Report");
        }
        [HttpPost]
        public IActionResult Create(chatAnswer model)
        {
            chatAnswer chatAnswer = new chatAnswer();
            chatAnswer = model;
            if (ModelState.IsValid)
            {
                _chatAnswer.AddAnswer(chatAnswer);

            }
            return View("ChatBoard");
        }
        public IActionResult Help()
        {
            return View();
        }
    }

}