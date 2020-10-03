using ExpenseTracker.Persistence;
using ExpenseTracker.WebUI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.WebUI.Controllers
{
    [Authorize]
    public abstract class BaseAuthenticatedController<T> : BaseController<T>
    {
        protected readonly UserManager<IdentityUser> _userManager;

        public BaseAuthenticatedController(ILogger<T> logger)
            : base(logger)
        {
        }
        public BaseAuthenticatedController(ILogger<T> logger, DbContextOptions<ExpenseTrackerDbContext> options)
            : base(logger, options)
        {
        }

        public BaseAuthenticatedController(ILogger<T> logger, DbContextOptions<ExpenseTrackerDbContext> options, UserManager<IdentityUser> userManager)
            : base(logger, options)
        {
            _userManager = userManager;
        }
    }
}
