using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using YouLearnGeometry.Models;
using YouLearnGeometry.Services;
using YouLearnGeometry.ViewModels;

namespace YouLearnGeometry.Controllers;

[Authorize]
public class CurriculumController : Controller
{
    private readonly IMongoDbService _mongo;

    public CurriculumController(IMongoDbService mongo)
    {
        _mongo = mongo;
    }

    public async Task<IActionResult> Index(string? grade = null, string? topic = null)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _mongo.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();
        if (user == null) return RedirectToAction("Login", "Account");

        string currentGrade = !string.IsNullOrEmpty(grade) ? grade : (user.GradeLevel ?? "Lop6");

        var curriculum = await _mongo.CurriculumLevels
            .Find(c => c.GradeLevel == currentGrade)
            .FirstOrDefaultAsync();

        var lessons = await _mongo.Lessons
            .Find(l => l.GradeLevel == currentGrade)
            .SortBy(l => l.Order)
            .ToListAsync();

        var progressList = await _mongo.LearningProgress
            .Find(p => p.UserId == userId)
            .ToListAsync();
        var progressDict = progressList.ToDictionary(p => p.LessonId, p => p);

        // Get all topic test attempts for this user
        var topicAttempts = await _mongo.AssessmentAttempts
            .Find(a => a.UserId == userId && a.AssessmentType == "TopicTest")
            .SortByDescending(a => a.AttemptedAt)
            .ToListAsync();

        var topicViewModels = new List<TopicItemViewModel>();
        bool isOverallTopicTestUnlocked = true;

        if (curriculum != null && curriculum.Topics != null)
        {
            foreach (var t in curriculum.Topics.OrderBy(t => t.Order))
            {
                var topicLessons = lessons.Where(l => l.TopicCode == t.TopicCode).ToList();
                var lessonProgressItems = topicLessons.Select(l =>
                {
                    progressDict.TryGetValue(l.Id, out var prog);
                    return new LessonProgressItem { Lesson = l, Progress = prog };
                }).ToList();

                // Unlock condition: All lessons in topic must be AwaitingTest or Completed
                bool topicUnlocked = topicLessons.Any() && topicLessons.All(l =>
                {
                    progressDict.TryGetValue(l.Id, out var prog);
                    return prog != null && (prog.Status == "AwaitingTest" || prog.Status == "Completed");
                });

                if (!topicUnlocked)
                {
                    isOverallTopicTestUnlocked = false;
                }

                var latestAttempt = topicAttempts.FirstOrDefault(a => a.TargetCode == t.TopicCode);

                bool isTopicCompleted = topicLessons.Any() && topicLessons.All(l =>
                {
                    progressDict.TryGetValue(l.Id, out var prog);
                    return prog != null && prog.Status == "Completed";
                }) && (latestAttempt == null || latestAttempt.IsPassed);

                topicViewModels.Add(new TopicItemViewModel
                {
                    Topic = t,
                    Lessons = lessonProgressItems,
                    IsTopicTestUnlocked = topicUnlocked,
                    LatestTopicTestAttempt = latestAttempt,
                    IsCompleted = isTopicCompleted
                });
            }
        }

        string activeTopicCode = !string.IsNullOrEmpty(topic) && topicViewModels.Any(tv => tv.Topic.TopicCode == topic)
            ? topic
            : (topicViewModels.FirstOrDefault()?.Topic.TopicCode ?? string.Empty);

        var vm = new CurriculumViewModel
        {
            GradeLevel = currentGrade,
            GradeTitle = currentGrade switch
            {
                "Lop6" => "Hình học phẳng Lớp 6",
                "Lop7" => "Hình học phẳng Lớp 7",
                "Lop8" => "Hình học phẳng Lớp 8",
                "Lop9" => "Hình học phẳng Lớp 9",
                _ => "Chương trình Hình học phẳng THCS"
            },
            Topics = topicViewModels,
            ActiveTopicCode = activeTopicCode,
            IsTopicTestUnlocked = isOverallTopicTestUnlocked,
            LatestTopicTestAttempt = topicAttempts.FirstOrDefault()
        };

        return View(vm);
    }
}
