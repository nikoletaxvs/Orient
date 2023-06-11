using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orient.Data;
using Orient.Models;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace Orient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _ctx;
        private readonly ApplicationDbContect _db;
        List<string> answearList = new List<string>();
        public HomeController(ILogger<HomeController> logger,IHttpContextAccessor ctx, ApplicationDbContect db)
        {
            _logger = logger;
            _ctx = ctx;
            _db = db;
        }

        public IActionResult Index()
        {
            ViewBag.Questions = _db.Questions.ToList();
            IEnumerable<Question> model = _db.Questions.Include(n => n.Answers).ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(checkBox model)
        {
            
            
            return RedirectToAction("Report");
        }

        
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        [Route("submit")] 
        public IActionResult Submit(IFormCollection iformCollection)
        {
            List<bool> replies = new List<bool>();
            string[] questionAnswers = iformCollection["questionId"];
            
            foreach(var q in questionAnswers)
            {
               
                Answer answerIdCorrect = _db.Answers
                    .Where(r => r.QuestionId == int.Parse(q))
                    .Where(a => a.Correct == true)
                    .FirstOrDefault();
                if (answerIdCorrect.AnswerId == int.Parse(iformCollection["question_" + q]))
                {
                    replies.Add(true);
                }
                else
                {
                    replies.Add(false);
                }
                
            }
            ViewBag.A = replies;
            return View("Report");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}