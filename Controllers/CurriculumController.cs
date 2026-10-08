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

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _mongo.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();
        if (user == null) return RedirectToAction("Login", "Account");

        string currentGrade = user.GradeLevel ?? "Lop6";

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

        var topicViewModels = new List<TopicItemViewModel>();
        bool isTopicTestUnlocked = true;

        if (curriculum != null && curriculum.Topics != null)
        {
            foreach (var topic in curriculum.Topics)
            {
                var topicLessons = lessons.Where(l => l.TopicCode == topic.TopicCode).ToList();
                var lessonProgressItems = topicLessons.Select(l =>
                {
                    progressDict.TryGetValue(l.Id, out var prog);
                    return new LessonProgressItem { Lesson = l, Progress = prog };
                }).ToList();

                // Check unlock condition: All lessons in topic must be AwaitingTest or Completed
                bool topicUnlocked = topicLessons.Any() && topicLessons.All(l =>
                {
                    progressDict.TryGetValue(l.Id, out var prog);
                    return prog != null && (prog.Status == "AwaitingTest" || prog.Status == "Completed");
                });

                if (!topicUnlocked)
                {
                    isTopicTestUnlocked = false;
                }

                topicViewModels.Add(new TopicItemViewModel
                {
                    Topic = topic,
                    Lessons = lessonProgressItems
                });
            }
        }

        // Get latest topic test attempt
        var latestTopicAttempt = await _mongo.AssessmentAttempts
            .Find(a => a.UserId == userId && a.AssessmentType == "TopicTest")
            .SortByDescending(a => a.AttemptedAt)
            .FirstOrDefaultAsync();

        var vm = new CurriculumViewModel
        {
            GradeLevel = currentGrade,
            GradeTitle = currentGrade == "Lop6" ? "Hình học phẳng Lớp 6" : "Hình học phẳng Lớp 8",
            Topics = topicViewModels,
            IsTopicTestUnlocked = isTopicTestUnlocked,
            LatestTopicTestAttempt = latestTopicAttempt
        };

        return View(vm);
    }
}
