﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _ctx;
        private readonly ApplicationDbContect _db;
        List<string> answearList = new List<string>();
        public HomeController(ILogger<HomeController> logger,IHttpContextAccessor ctx, ApplicationDbContect db, IAccountService accountService)
        {
            _logger = logger;
            _ctx = ctx;
            _db = db;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            ViewBag.Questions = _db.Questions.ToList();
            IEnumerable<Question> model = _db.Questions.Include(n => n.Answers).ToList();
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
            List<bool> correctAns = new List<bool>();
            bool correct;
            int score = 0;
            List<Answer> currentAnswers = new List<Answer>();
            string[] questionAnswers = iformCollection["questionId"];
            
            foreach(var q in questionAnswers)
            {
               //Getting the correct answer for each question from the database
                Answer answerIdCorrect = _db.Answers
                    .Where(r => r.QuestionId == int.Parse(q))
                    .Where(a => a.Correct == true)
                    .FirstOrDefault();
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
            ViewBag.A = new Reply() { TotalScore = score, QuestionList = _db.Questions.ToList(), AnswersList = currentAnswers, correctAnswers = correctAns };
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
        public IActionResult Login(string username,string password)
        {
            var account = _accountService.Login(username, password);
            if(account != null)
            {
                HttpContext.Session.SetString("username",username);
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
            return View("Welcome");
        }
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return View("LoginPage");
        }
    }
}