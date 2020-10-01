using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    [Authorize]
    public abstract class BaseAuthenticatedController : Controller
    {
    }
}
