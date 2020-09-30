using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebUI.Controllers.Base
{
    [Authorize]
    public abstract class BaseAuthenticatedController : Controller
    {
    }
}
