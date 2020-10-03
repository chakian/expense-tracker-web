using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExpenseTracker.WebUI.Models;
using ExpenseTracker.WebUI.Controllers.Base;

namespace ExpenseTracker.WebUI.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        public HomeController(ILogger<HomeController> logger)
            : base(logger)
        {
        }

        public IActionResult Index()
        {
            if (User?.Identity.IsAuthenticated == true)
            {
                _logger.LogInformation("User is already logged in; redirecting to Dashboard");
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
