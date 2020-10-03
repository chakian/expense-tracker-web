using ExpenseTracker.WebUI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.WebUI.Controllers
{
    [Authorize]
    public abstract class BaseAuthenticatedController<T> : BaseController<T>
    {
        public BaseAuthenticatedController(ILogger<T> logger)
            : base(logger)
        {
        }
    }
}
