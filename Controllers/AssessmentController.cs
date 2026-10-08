using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using YouLearnGeometry.Models;
using YouLearnGeometry.Services;
using YouLearnGeometry.ViewModels;

namespace YouLearnGeometry.Controllers;

[Authorize]
public class AssessmentController : Controller
{
    private readonly IMongoDbService _mongo;

    public AssessmentController(IMongoDbService mongo)
    {
        _mongo = mongo;
    }

    [HttpGet]
    public async Task<IActionResult> TopicTest(string topicCode)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _mongo.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();
        if (user == null) return RedirectToAction("Login", "Account");

        string currentGrade = user.GradeLevel ?? "Lop6";

        var curriculum = await _mongo.CurriculumLevels
            .Find(c => c.GradeLevel == currentGrade)
            .FirstOrDefaultAsync();

        var topic = curriculum?.Topics.FirstOrDefault(t => t.TopicCode == topicCode);
        if (topic == null) return NotFound("Không tìm thấy chủ đề kiểm tra.");

        // Kiểm tra điều kiện mở: Toàn bộ bài học trong chủ đề phải là AwaitingTest hoặc Completed
        var lessons = await _mongo.Lessons
            .Find(l => l.TopicCode == topicCode && l.GradeLevel == currentGrade)
            .ToListAsync();

        var progressList = await _mongo.LearningProgress
            .Find(p => p.UserId == userId)
            .ToListAsync();
        var progressDict = progressList.ToDictionary(p => p.LessonId, p => p);

        bool isUnlocked = lessons.Any() && lessons.All(l =>
        {
            progressDict.TryGetValue(l.Id, out var p);
            return p != null && (p.Status == "AwaitingTest" || p.Status == "Completed");
        });

        if (!isUnlocked)
        {
            TempData["ErrorMessage"] = "Bạn cần hoàn thành nội dung (bấm Đánh dấu hoàn thành) tất cả các bài học trong chủ đề trước khi mở khóa bài Topic Test!";
            return RedirectToAction("Index", "Curriculum");
        }

        ViewBag.Topic = topic;
        return View(topic.TopicTest);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitTopicTest(string topicCode, List<QuestionAnswerDto> answers)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _mongo.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();
        if (user == null) return RedirectToAction("Login", "Account");

        string currentGrade = user.GradeLevel ?? "Lop6";

        var curriculum = await _mongo.CurriculumLevels
            .Find(c => c.GradeLevel == currentGrade)
            .FirstOrDefaultAsync();

        var topic = curriculum?.Topics.FirstOrDefault(t => t.TopicCode == topicCode);
        if (topic == null) return NotFound();

        int correctCount = 0;
        int totalQuestions = topic.TopicTest.Count;

        foreach (var q in topic.TopicTest)
        {
            var userAns = answers?.FirstOrDefault(a => a.QuestionId == q.Id)?.SelectedOption;
            if (userAns != null && q.CorrectAnswers.Any(c => string.Equals(c.Trim(), userAns.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                correctCount++;
            }
        }

        double score = totalQuestions > 0 ? Math.Round((double)correctCount / totalQuestions * 100, 1) : 0;
        bool isPassed = score >= 70.0; // Ngưỡng đạt Topic Test: 70%

        var attempt = new AssessmentAttempt
        {
            UserId = userId!,
            AssessmentType = "TopicTest",
            TargetCode = topicCode,
            Title = topic.Title,
            Score = score,
            CorrectCount = correctCount,
            TotalQuestions = totalQuestions,
            IsPassed = isPassed,
            AttemptedAt = DateTime.UtcNow
        };
        await _mongo.AssessmentAttempts.InsertOneAsync(attempt);

        var newBadges = new List<string>();

        // Huy hiệu "Bậc thầy Tứ giác" (Topic Test >= 80%)
        if (score >= 80.0 && !(user.BadgesEarned ?? new List<string>()).Contains("bac_thay_tu_giac"))
        {
            var badges = new HashSet<string>(user.BadgesEarned ?? new List<string>()) { "bac_thay_tu_giac" };
            var update = Builders<User>.Update.Set(u => u.BadgesEarned, badges.ToList());
            await _mongo.Users.UpdateOneAsync(u => u.Id == userId, update);
            newBadges.Add("bac_thay_tu_giac");
        }

        var resultVm = new TestResultViewModel
        {
            Title = $"Kết quả {topic.Title}",
            TargetCode = topicCode,
            AssessmentType = "TopicTest",
            CorrectCount = correctCount,
            TotalQuestions = totalQuestions,
            Score = score,
            IsPassed = isPassed,
            Threshold = 70.0,
            HasNewlyCompletedLesson = false,
            NewBadgesEarned = newBadges,
            BackUrl = Url.Action("Index", "Curriculum") ?? "/Curriculum"
        };

        return View("Result", resultVm);
    }
}
