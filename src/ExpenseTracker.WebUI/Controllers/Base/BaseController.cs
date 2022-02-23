using ExpenseTracker.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace ExpenseTracker.WebUI.Controllers.Base
{
    public class BaseController<T> : Controller
    {
        protected readonly ILogger<T> _logger;
        protected readonly DbContextOptions<ExpenseTrackerDbContext> _dbContextOptions;
        protected readonly ExpenseTrackerDbContext _dbContext;

        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }
        [Obsolete]
        public BaseController(ILogger<T> logger, DbContextOptions<ExpenseTrackerDbContext> options)
        {
            _logger = logger;
            _dbContextOptions = options;
            _dbContext = new ExpenseTrackerDbContext(_dbContextOptions);
        }
        public BaseController(ILogger<T> logger, ExpenseTrackerDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }
    }
}
