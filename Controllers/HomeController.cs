using Microsoft.AspNetCore.Mvc;
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
            ViewBag.Questions = _db.unit1_Questions.ToList();
           var model = new checkBox() 
           {
               checkBoxes = new List<checkBoxOption>
                {
                   new checkBoxOption()
                   {
                       IsChecked = true,
                       Description ="Option 1",
                       Value="OPTION 1"
                   },
                   new checkBoxOption()
                   {
                       IsChecked = true,
                       Description ="Option 2",
                       Value="OPTION 2"
                   },
                   new checkBoxOption()
                   {
                       IsChecked = true,
                       Description ="Option 3",
                       Value="OPTION 3"
                   }
                }
            };
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
            var questionAnswers = iformCollection["ans"];
            string a = "Nothing";
            foreach(var q in questionAnswers)
            {
               
                
            }
            ViewBag.A = a;
            return View("Report");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}