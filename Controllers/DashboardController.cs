using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using YouLearnGeometry.Models;
using YouLearnGeometry.Services;
using YouLearnGeometry.ViewModels;

namespace YouLearnGeometry.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly IMongoDbService _mongo;

    public DashboardController(IMongoDbService mongo)
    {
        _mongo = mongo;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _mongo.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();
        if (user == null) return RedirectToAction("Login", "Account");

        string currentGrade = user.GradeLevel ?? "Lop6";

        // Get all lessons for user's grade
        var lessons = await _mongo.Lessons
            .Find(l => l.GradeLevel == currentGrade)
            .SortBy(l => l.Order)
            .ToListAsync();

        // Get learning progress for user
        var progressList = await _mongo.LearningProgress
            .Find(p => p.UserId == userId)
            .ToListAsync();

        var progressDict = progressList.ToDictionary(p => p.LessonId, p => p);

        // Build lesson progress items
        var lessonItems = new List<LessonProgressItem>();
        Lesson? nextLesson = null;
        string? nextLessonStatus = null;

        foreach (var l in lessons)
        {
            progressDict.TryGetValue(l.Id, out var prog);
            lessonItems.Add(new LessonProgressItem
            {
                Lesson = l,
                Progress = prog
            });

            // Find first incomplete lesson to recommend for "Continue Learning"
            if (nextLesson == null && (prog == null || prog.Status != "Completed"))
            {
                nextLesson = l;
                nextLessonStatus = prog?.Status ?? "NotStarted";
            }
        }

        // If all lessons completed, nextLesson is the first lesson to review
        if (nextLesson == null && lessons.Any())
        {
            nextLesson = lessons.First();
            nextLessonStatus = "Completed";
        }

        int completedLessons = progressList.Count(p => p.Status == "Completed" && lessons.Any(l => l.Id == p.LessonId));
        int awaitingTestLessons = progressList.Count(p => p.Status == "AwaitingTest" && lessons.Any(l => l.Id == p.LessonId));

        // Get test attempts count
        var completedTests = await _mongo.AssessmentAttempts
            .Find(a => a.UserId == userId && a.IsPassed)
            .CountDocumentsAsync();

        // Get badges
        var badges = await _mongo.Achievements.Find(Builders<Achievement>.Filter.Empty).ToListAsync();

        // Recent attempts
        var recentAttempts = await _mongo.AssessmentAttempts
            .Find(a => a.UserId == userId)
            .SortByDescending(a => a.AttemptedAt)
            .Limit(5)
            .ToListAsync();

        var vm = new DashboardViewModel
        {
            User = user,
            NextLesson = nextLesson,
            NextLessonStatus = nextLessonStatus,
            TotalLessonsInGrade = lessons.Count,
            CompletedLessonsCount = completedLessons,
            AwaitingTestLessonsCount = awaitingTestLessons,
            CompletedTestsCount = (int)completedTests,
            LessonItems = lessonItems,
            Badges = badges,
            RecentAttempts = recentAttempts
        };

        return View(vm);
    }
}
