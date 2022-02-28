using ExpenseTracker.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.WebUI.Areas.Admin.Controllers
{
    public class RoleController : BaseAdminController<HomeController>
    {
        private RoleManager<IdentityRole> roleManager;
        
        public RoleController(ILogger<HomeController> logger, DbContextOptions<ExpenseTrackerDbContext> options, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleMgr) : base(logger, options, userManager)
        {
            roleManager = roleMgr;
        }

        public ViewResult Index() => View(roleManager.Roles);

        //https://www.yogihosting.com/aspnet-core-identity-roles/#create-role

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
