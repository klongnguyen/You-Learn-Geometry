using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YouLearnGeometry.Controllers;

[Authorize]
public class GeometryLabController : Controller
{
    public IActionResult Index(string? initialShape = "HinhVuong", bool challengeMode = false)
    {
        ViewBag.InitialShape = initialShape ?? "HinhVuong";
        ViewBag.ChallengeMode = challengeMode;
        return View();
    }
}
