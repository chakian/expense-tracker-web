using ExpenseTracker.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.WebUI.Controllers.Base
{
    public class BaseController<T> : Controller
    {
        protected readonly ILogger<T> _logger;
        protected readonly DbContextOptions<ExpenseTrackerDbContext> _dbContextOptions;

        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }
        public BaseController(ILogger<T> logger, DbContextOptions<ExpenseTrackerDbContext> options)
        {
            _logger = logger;
            _dbContextOptions = options;
        }
    }
}
