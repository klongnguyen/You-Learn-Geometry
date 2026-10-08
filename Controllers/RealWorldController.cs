using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YouLearnGeometry.Controllers;

[Authorize]
public class RealWorldController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
