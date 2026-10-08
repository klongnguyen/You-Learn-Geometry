using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using YouLearnGeometry.Models;
using YouLearnGeometry.Services;
using YouLearnGeometry.ViewModels;

namespace YouLearnGeometry.Controllers;

public class AccountController : Controller
{
    private readonly IMongoDbService _mongo;

    public AccountController(IMongoDbService mongo)
    {
        _mongo = mongo;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var filter = Builders<User>.Filter.Or(
            Builders<User>.Filter.Eq(u => u.Email, model.Identifier.Trim()),
            Builders<User>.Filter.Eq(u => u.Username, model.Identifier.Trim())
        );

        var user = await _mongo.Users.Find(filter).FirstOrDefaultAsync();

        if (user == null || user.Password != model.Password)
        {
            ModelState.AddModelError("", "Email/Tên đăng nhập hoặc Mật khẩu không chính xác.");
            return View(model);
        }

        if (!user.IsActive)
        {
            ModelState.AddModelError("", "Tài khoản này đã bị khóa. Vui lòng liên hệ quản trị viên.");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role),
            new("GradeLevel", user.GradeLevel)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        {
            return Redirect(model.ReturnUrl);
        }

        if (user.Role == "Admin")
        {
            return RedirectToAction("Index", "Admin");
        }

        return RedirectToAction("Index", "Dashboard");
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return View(new RegisterViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var existingUser = await _mongo.Users.Find(u => u.Email == model.Email.Trim() || u.Username == model.Username.Trim()).FirstOrDefaultAsync();
        if (existingUser != null)
        {
            ModelState.AddModelError("", "Email hoặc Tên đăng nhập này đã tồn tại trong hệ thống.");
            return View(model);
        }

        var newUser = new User
        {
            FullName = model.FullName.Trim(),
            Email = model.Email.Trim().ToLower(),
            Username = model.Username.Trim().ToLower(),
            Password = model.Password, // Plain text for demonstration as agreed in SRS
            GradeLevel = model.GradeLevel,
            Role = "Student",
            StreakCount = 0,
            LastStreakDate = null,
            BadgesEarned = new List<string>(),
            CreatedAt = DateTime.UtcNow
        };

        await _mongo.Users.InsertOneAsync(newUser);

        // Auto login
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, newUser.Id),
            new(ClaimTypes.Name, newUser.FullName),
            new(ClaimTypes.Email, newUser.Email),
            new(ClaimTypes.Role, newUser.Role),
            new("GradeLevel", newUser.GradeLevel)
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        return RedirectToAction("Index", "Dashboard");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _mongo.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();
        if (user == null) return NotFound();

        return View(new ProfileViewModel
        {
            FullName = user.FullName,
            Email = user.Email,
            Username = user.Username,
            GradeLevel = user.GradeLevel,
            StreakCount = user.StreakCount,
            BadgesEarned = user.BadgesEarned,
            CreatedAt = user.CreatedAt
        });
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateGrade(string gradeLevel)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var update = Builders<User>.Update.Set(u => u.GradeLevel, gradeLevel);
        await _mongo.Users.UpdateOneAsync(u => u.Id == userId, update);

        // Refresh auth cookie with new grade claim
        var user = await _mongo.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.FullName),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role),
                new("GradeLevel", user.GradeLevel)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        TempData["SuccessMessage"] = $"Đã cập nhật cấp độ học thành {(gradeLevel == "Lop6" ? "Lớp 6" : "Lớp 8")} thành công!";
        return RedirectToAction("Index", "Dashboard");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
