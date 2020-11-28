using ExpenseTracker.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.WebUI.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController<HomeController>
    {
        public HomeController(ILogger<HomeController> logger, DbContextOptions<ExpenseTrackerDbContext> options, UserManager<IdentityUser> userManager) : base(logger, options, userManager)
        {
        }

        public IActionResult Index()
        {
            var us = User;
            return View();
        }
    }
}
