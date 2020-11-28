using ExpenseTracker.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class BaseAdminController<T> : Controller
    {
        protected readonly ILogger<T> _logger;
        protected readonly DbContextOptions<ExpenseTrackerDbContext> _dbContextOptions;
        private readonly UserManager<IdentityUser> _userManager;

        public BaseAdminController(ILogger<T> logger, DbContextOptions<ExpenseTrackerDbContext> options, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _dbContextOptions = options;
            _userManager = userManager;
        }

        protected string UserId
        {
            get
            {
                return _userManager.GetUserId(User);
            }
        }
    }
}
