using Microsoft.AspNetCore.Mvc;
using Orient.Data;
using Orient.Models;

namespace Orient.Controllers
{
    public class unit1_QuestionsController : Controller
    {
        private readonly ApplicationDbContect _db;
        public unit1_QuestionsController(ApplicationDbContect db)
        {
            _db = db;
        }
        public IActionResult Unit1_Test()
        {
            IEnumerable<unit1_question> questions = _db.unit1_Questions;
            return View(questions);
        } 
        public IActionResult Check(string button)
        {
            if(button== "correct")
            {
                TempData["buttonVal"] = "Correct";
            }
            else
            {
                TempData["buttonVal"] = "Incorrect";
            }
            return RedirectToAction("Unit1_Test");
        }
    }
}
