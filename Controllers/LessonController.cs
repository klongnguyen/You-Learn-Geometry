using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using YouLearnGeometry.Models;
using YouLearnGeometry.Services;
using YouLearnGeometry.ViewModels;

namespace YouLearnGeometry.Controllers;

[Authorize]
public class LessonController : Controller
{
    private readonly IMongoDbService _mongo;

    public LessonController(IMongoDbService mongo)
    {
        _mongo = mongo;
    }

    [HttpGet]
    public async Task<IActionResult> Detail(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var lesson = await _mongo.Lessons.Find(l => l.Id == id).FirstOrDefaultAsync();
        if (lesson == null) return NotFound();

        // Get or initialize progress
        var progress = await _mongo.LearningProgress
            .Find(p => p.UserId == userId && p.LessonId == id)
            .FirstOrDefaultAsync();

        if (progress == null)
        {
            progress = new LearningProgress
            {
                UserId = userId!,
                LessonId = id,
                Status = "Learning",
                LastAccessedAt = DateTime.UtcNow
            };
            await _mongo.LearningProgress.InsertOneAsync(progress);
        }
        else if (progress.Status == "NotStarted")
        {
            var update = Builders<LearningProgress>.Update
                .Set(p => p.Status, "Learning")
                .Set(p => p.LastAccessedAt, DateTime.UtcNow);
            await _mongo.LearningProgress.UpdateOneAsync(p => p.Id == progress.Id, update);
            progress.Status = "Learning";
        }
        else
        {
            var update = Builders<LearningProgress>.Update.Set(p => p.LastAccessedAt, DateTime.UtcNow);
            await _mongo.LearningProgress.UpdateOneAsync(p => p.Id == progress.Id, update);
        }

        // Navigation (Prev/Next lesson)
        var allLessons = await _mongo.Lessons
            .Find(l => l.GradeLevel == lesson.GradeLevel)
            .SortBy(l => l.Order)
            .ToListAsync();

        int currentIndex = allLessons.FindIndex(l => l.Id == id);
        Lesson? prevLesson = currentIndex > 0 ? allLessons[currentIndex - 1] : null;
        Lesson? nextLesson = currentIndex >= 0 && currentIndex < allLessons.Count - 1 ? allLessons[currentIndex + 1] : null;

        var vm = new LessonDetailViewModel
        {
            Lesson = lesson,
            Progress = progress,
            PreviousLesson = prevLesson,
            NextLesson = nextLesson
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkCompleted(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var lesson = await _mongo.Lessons.Find(l => l.Id == id).FirstOrDefaultAsync();
        if (lesson == null) return NotFound();

        var progress = await _mongo.LearningProgress
            .Find(p => p.UserId == userId && p.LessonId == id)
            .FirstOrDefaultAsync();

        bool wasCompletedBefore = progress?.Status == "Completed";

        if (!lesson.HasMiniTest)
        {
            // Case 1: Bài KHÔNG có Mini Test -> Bấm nút là Completed luôn
            if (progress == null)
            {
                progress = new LearningProgress
                {
                    UserId = userId!,
                    LessonId = id,
                    Status = "Completed",
                    CompletedAt = DateTime.UtcNow,
                    LastAccessedAt = DateTime.UtcNow
                };
                await _mongo.LearningProgress.InsertOneAsync(progress);
            }
            else
            {
                var update = Builders<LearningProgress>.Update
                    .Set(p => p.Status, "Completed")
                    .Set(p => p.CompletedAt, DateTime.UtcNow)
                    .Set(p => p.LastAccessedAt, DateTime.UtcNow);
                await _mongo.LearningProgress.UpdateOneAsync(p => p.Id == progress.Id, update);
            }

            if (!wasCompletedBefore)
            {
                await AwardStreakAndBadgesAsync(userId!);
            }

            TempData["SuccessMessage"] = "🎉 Tuyệt vời! Bạn đã hoàn thành xuất sắc bài học này.";
        }
        else
        {
            // Case 2: Bài CÓ Mini Test -> Bấm nút chuyển sang AwaitingTest
            if (progress == null)
            {
                progress = new LearningProgress
                {
                    UserId = userId!,
                    LessonId = id,
                    Status = "AwaitingTest",
                    LastAccessedAt = DateTime.UtcNow
                };
                await _mongo.LearningProgress.InsertOneAsync(progress);
            }
            else if (progress.Status != "Completed")
            {
                var update = Builders<LearningProgress>.Update
                    .Set(p => p.Status, "AwaitingTest")
                    .Set(p => p.LastAccessedAt, DateTime.UtcNow);
                await _mongo.LearningProgress.UpdateOneAsync(p => p.Id == progress.Id, update);
            }

            TempData["InfoMessage"] = "Bạn đã hoàn thành nội dung lý thuyết & thực hành. Hãy làm bài Mini Test bên dưới (đạt từ 75%) để được công nhận Hoàn thành bài học nhé!";
        }

        return RedirectToAction("Detail", new { id });
    }

    [HttpPost]
    public async Task<IActionResult> SubmitPractice([FromBody] PracticeSubmitModel model)
    {
        var lesson = await _mongo.Lessons.Find(l => l.Id == model.LessonId).FirstOrDefaultAsync();
        if (lesson == null) return Json(new { success = false, message = "Không tìm thấy bài học" });

        var exercise = lesson.PracticeExercises.FirstOrDefault(e => e.Id == model.ExerciseId);
        if (exercise == null) return Json(new { success = false, message = "Không tìm thấy câu hỏi" });

        bool isCorrect = exercise.CorrectAnswers.Any(a => string.Equals(a.Trim(), model.UserAnswer?.Trim(), StringComparison.OrdinalIgnoreCase));

        if (isCorrect)
        {
            return Json(new PracticeResultModel
            {
                IsCorrect = true,
                Message = "🎉 Chính xác! Bạn đã hiểu rất rõ kiến thức này.",
                Explanation = exercise.Explanation
            });
        }

        // Quyết định theo quy tắc nghiệp vụ Hint (FR-10):
        // Sai lần 1: Cho phép thử lại
        // Sai lần 2: Hiển thị Hint 1
        // Sai lần 3+: Hiển thị Hint 2, sau đó mới cho xem đáp án
        if (model.AttemptCount <= 1)
        {
            return Json(new PracticeResultModel
            {
                IsCorrect = false,
                Message = "Chưa chính xác, hãy suy nghĩ thêm và thử lại một lần nữa nhé!",
                Hint = null,
                AllowViewAnswer = false
            });
        }
        else if (model.AttemptCount == 2)
        {
            return Json(new PracticeResultModel
            {
                IsCorrect = false,
                Message = "Vẫn chưa đúng. Đây là gợi ý dành cho bạn:",
                Hint = string.IsNullOrEmpty(exercise.Hint1) ? "Hãy đọc lại phần tính chất ở trên nhé." : exercise.Hint1,
                AllowViewAnswer = false
            });
        }
        else
        {
            return Json(new PracticeResultModel
            {
                IsCorrect = false,
                Message = "Chưa chính xác. Đừng nản lòng, hãy xem gợi ý chi tiết:",
                Hint = string.IsNullOrEmpty(exercise.Hint2) ? exercise.Hint1 : exercise.Hint2,
                AllowViewAnswer = true,
                CorrectAnswerText = string.Join(", ", exercise.CorrectAnswers),
                Explanation = exercise.Explanation
            });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitMiniTest(MiniTestSubmitModel model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var lesson = await _mongo.Lessons.Find(l => l.Id == model.LessonId).FirstOrDefaultAsync();
        if (lesson == null) return NotFound();

        int correctCount = 0;
        int totalQuestions = lesson.MiniTest.Count;

        foreach (var q in lesson.MiniTest)
        {
            var userAns = model.Answers?.FirstOrDefault(a => a.QuestionId == q.Id)?.SelectedOption;
            if (userAns != null && q.CorrectAnswers.Any(c => string.Equals(c.Trim(), userAns.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                correctCount++;
            }
        }

        double score = totalQuestions > 0 ? Math.Round((double)correctCount / totalQuestions * 100, 1) : 0;
        bool isPassed = score >= 75.0; // Ngưỡng đạt 75% theo SRS

        var progress = await _mongo.LearningProgress
            .Find(p => p.UserId == userId && p.LessonId == model.LessonId)
            .FirstOrDefaultAsync();

        bool wasCompletedBefore = progress?.Status == "Completed";
        bool newlyCompleted = false;

        double bestScore = Math.Max(progress?.BestMiniTestScore ?? 0, score);
        int attempts = (progress?.MiniTestAttempts ?? 0) + 1;

        if (progress == null)
        {
            progress = new LearningProgress
            {
                UserId = userId!,
                LessonId = model.LessonId,
                Status = isPassed ? "Completed" : "AwaitingTest",
                BestMiniTestScore = bestScore,
                MiniTestAttempts = attempts,
                CompletedAt = isPassed ? DateTime.UtcNow : null,
                LastAccessedAt = DateTime.UtcNow
            };
            await _mongo.LearningProgress.InsertOneAsync(progress);
            if (isPassed) newlyCompleted = true;
        }
        else
        {
            // Giữ nguyên Completed nếu lần làm lại này bị trượt
            string newStatus = progress.Status == "Completed" ? "Completed" : (isPassed ? "Completed" : "AwaitingTest");
            DateTime? compAt = progress.Status == "Completed" ? progress.CompletedAt : (isPassed ? DateTime.UtcNow : null);

            var update = Builders<LearningProgress>.Update
                .Set(p => p.Status, newStatus)
                .Set(p => p.BestMiniTestScore, bestScore)
                .Set(p => p.MiniTestAttempts, attempts)
                .Set(p => p.CompletedAt, compAt)
                .Set(p => p.LastAccessedAt, DateTime.UtcNow);

            await _mongo.LearningProgress.UpdateOneAsync(p => p.Id == progress.Id, update);
            if (!wasCompletedBefore && isPassed) newlyCompleted = true;
        }

        // Lưu bản ghi kiểm tra (assessment_attempts)
        var attempt = new AssessmentAttempt
        {
            UserId = userId!,
            AssessmentType = "MiniTest",
            TargetCode = lesson.Id,
            Title = lesson.Title,
            Score = score,
            CorrectCount = correctCount,
            TotalQuestions = totalQuestions,
            IsPassed = isPassed,
            AttemptedAt = DateTime.UtcNow
        };
        await _mongo.AssessmentAttempts.InsertOneAsync(attempt);

        List<string> newBadges = new();
        int currentStreak = 0;

        if (newlyCompleted)
        {
            (currentStreak, newBadges) = await AwardStreakAndBadgesAsync(userId!);
        }

        var resultVm = new TestResultViewModel
        {
            Title = $"Kết quả Mini Test - {lesson.Title}",
            TargetCode = lesson.Id,
            AssessmentType = "MiniTest",
            CorrectCount = correctCount,
            TotalQuestions = totalQuestions,
            Score = score,
            IsPassed = isPassed,
            Threshold = 75.0,
            HasNewlyCompletedLesson = newlyCompleted,
            UpdatedStreak = currentStreak,
            NewBadgesEarned = newBadges,
            BackUrl = Url.Action("Detail", new { id = lesson.Id }) ?? "/Dashboard"
        };

        return View("~/Views/Assessment/Result.cshtml", resultVm);
    }

    private async Task<(int streak, List<string> newBadges)> AwardStreakAndBadgesAsync(string userId)
    {
        var user = await _mongo.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();
        if (user == null) return (0, new List<string>());

        var newBadges = new List<string>();
        var today = DateTime.UtcNow.Date;
        int streak = user.StreakCount;

        // BR-05 & TBD-04: Streak tính khi có hoàn thành 1 bài trong ngày, mỗi ngày tính 1 lần
        if (user.LastStreakDate == null)
        {
            streak = 1;
        }
        else if (user.LastStreakDate.Value.Date == today)
        {
            // Đã tính streak hôm nay rồi, giữ nguyên
        }
        else if (user.LastStreakDate.Value.Date == today.AddDays(-1))
        {
            streak += 1;
        }
        else
        {
            streak = 1; // Đứt streak, bắt đầu lại
        }

        var badges = new HashSet<string>(user.BadgesEarned ?? new List<string>());

        // Huy hiệu 1: "Khởi đầu nan" - Hoàn thành bài học đầu tiên
        if (!badges.Contains("khoi_dau"))
        {
            badges.Add("khoi_dau");
            newBadges.Add("khoi_dau");
        }

        // Huy hiệu 2: "Chiến binh bền bỉ" - Streak >= 3
        if (streak >= 3 && !badges.Contains("ben_bi"))
        {
            badges.Add("ben_bi");
            newBadges.Add("ben_bi");
        }

        var update = Builders<User>.Update
            .Set(u => u.StreakCount, streak)
            .Set(u => u.LastStreakDate, today)
            .Set(u => u.BadgesEarned, badges.ToList());

        await _mongo.Users.UpdateOneAsync(u => u.Id == userId, update);

        return (streak, newBadges);
    }
}
