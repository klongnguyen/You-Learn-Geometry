using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using YouLearnGeometry.Models;
using YouLearnGeometry.Services;
using YouLearnGeometry.ViewModels;

namespace YouLearnGeometry.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IMongoDbService _mongo;
    private readonly INeo4jService _neo4j;

    public AdminController(IMongoDbService mongo, INeo4jService neo4j)
    {
        _mongo = mongo;
        _neo4j = neo4j;
    }

    public async Task<IActionResult> Index()
    {
        long totalUsers = await _mongo.Users.CountDocumentsAsync(Builders<User>.Filter.Empty);
        long totalLessons = await _mongo.Lessons.CountDocumentsAsync(Builders<Lesson>.Filter.Empty);
        long totalCompleted = await _mongo.LearningProgress.CountDocumentsAsync(p => p.Status == "Completed");

        var attempts = await _mongo.AssessmentAttempts.Find(Builders<AssessmentAttempt>.Filter.Empty).ToListAsync();
        double avgScore = attempts.Any() ? Math.Round(attempts.Average(a => a.Score), 1) : 0;
        double completionRate = totalUsers > 0 && totalLessons > 0 ? Math.Round((double)totalCompleted / (totalUsers * totalLessons) * 100, 1) : 0;

        var recentUsers = await _mongo.Users.Find(Builders<User>.Filter.Empty)
            .SortByDescending(u => u.CreatedAt)
            .Limit(10)
            .ToListAsync();

        var lessons = await _mongo.Lessons.Find(Builders<Lesson>.Filter.Empty)
            .SortBy(l => l.GradeLevel).ThenBy(l => l.Order)
            .ToListAsync();

        var vm = new AdminDashboardViewModel
        {
            TotalUsers = totalUsers,
            TotalLessons = totalLessons,
            TotalCompletedLessons = totalCompleted,
            AverageTestScore = avgScore,
            OverallCompletionRate = completionRate,
            RecentUsers = recentUsers,
            Lessons = lessons
        };

        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Users()
    {
        var users = await _mongo.Users.Find(Builders<User>.Filter.Empty).SortByDescending(u => u.CreatedAt).ToListAsync();
        return View(users);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleUserStatus(string id)
    {
        var user = await _mongo.Users.Find(u => u.Id == id).FirstOrDefaultAsync();
        if (user != null && user.Username != "admin")
        {
            var update = Builders<User>.Update.Set(u => u.IsActive, !user.IsActive);
            await _mongo.Users.UpdateOneAsync(u => u.Id == id, update);
            TempData["SuccessMessage"] = $"Đã cập nhật trạng thái tài khoản {user.Username}.";
        }
        return RedirectToAction("Users");
    }

    [HttpGet]
    public async Task<IActionResult> Lessons()
    {
        var lessons = await _mongo.Lessons.Find(Builders<Lesson>.Filter.Empty).SortBy(l => l.GradeLevel).ThenBy(l => l.Order).ToListAsync();
        return View(lessons);
    }

    [HttpGet]
    public async Task<IActionResult> EditLesson(string? id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return View(new AdminLessonEditModel());
        }

        var lesson = await _mongo.Lessons.Find(l => l.Id == id).FirstOrDefaultAsync();
        if (lesson == null) return NotFound();

        var model = new AdminLessonEditModel
        {
            Id = lesson.Id,
            LessonCode = lesson.LessonCode,
            GradeLevel = lesson.GradeLevel,
            TopicCode = lesson.TopicCode,
            Title = lesson.Title,
            Order = lesson.Order,
            Summary = lesson.Summary,
            Definition = lesson.Definition,
            PropertiesText = string.Join("\n", lesson.Properties),
            FormulasText = string.Join("\n", lesson.Formulas),
            VisualExample = lesson.VisualExample,
            RealWorldExample = lesson.RealWorldExample,
            HasGeometryLab = lesson.HasGeometryLab,
            DefaultLabShape = lesson.DefaultLabShape,
            HasMiniTest = lesson.HasMiniTest
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditLesson(AdminLessonEditModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var props = (model.PropertiesText ?? "").Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
        var formulas = (model.FormulasText ?? "").Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();

        if (string.IsNullOrEmpty(model.Id))
        {
            var newLesson = new Lesson
            {
                LessonCode = model.LessonCode,
                GradeLevel = model.GradeLevel,
                TopicCode = model.TopicCode,
                Title = model.Title,
                Order = model.Order,
                Summary = model.Summary,
                Definition = model.Definition,
                Properties = props,
                Formulas = formulas,
                VisualExample = model.VisualExample,
                RealWorldExample = model.RealWorldExample,
                HasGeometryLab = model.HasGeometryLab,
                DefaultLabShape = model.DefaultLabShape,
                HasMiniTest = model.HasMiniTest,
                CreatedAt = DateTime.UtcNow
            };
            await _mongo.Lessons.InsertOneAsync(newLesson);
            TempData["SuccessMessage"] = "Đã thêm bài học mới thành công!";
        }
        else
        {
            var update = Builders<Lesson>.Update
                .Set(l => l.LessonCode, model.LessonCode)
                .Set(l => l.GradeLevel, model.GradeLevel)
                .Set(l => l.TopicCode, model.TopicCode)
                .Set(l => l.Title, model.Title)
                .Set(l => l.Order, model.Order)
                .Set(l => l.Summary, model.Summary)
                .Set(l => l.Definition, model.Definition)
                .Set(l => l.Properties, props)
                .Set(l => l.Formulas, formulas)
                .Set(l => l.VisualExample, model.VisualExample)
                .Set(l => l.RealWorldExample, model.RealWorldExample)
                .Set(l => l.HasGeometryLab, model.HasGeometryLab)
                .Set(l => l.DefaultLabShape, model.DefaultLabShape)
                .Set(l => l.HasMiniTest, model.HasMiniTest);

            await _mongo.Lessons.UpdateOneAsync(l => l.Id == model.Id, update);
            TempData["SuccessMessage"] = "Đã cập nhật bài học thành công!";
        }

        return RedirectToAction("Lessons");
    }

    [HttpGet]
    public async Task<IActionResult> KnowledgeMapAdmin()
    {
        var nodes = await _neo4j.GetAllNodesAsync();
        var rels = await _neo4j.GetAllRelationshipsAsync();
        ViewBag.Relationships = rels;
        return View(nodes);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveNode(GraphNode node)
    {
        if (string.IsNullOrEmpty(node.Id) || string.IsNullOrEmpty(node.Label))
        {
            TempData["ErrorMessage"] = "ID và Tên hiển thị của Node không được để trống.";
            return RedirectToAction("KnowledgeMapAdmin");
        }

        await _neo4j.AddOrUpdateNodeAsync(node);
        TempData["SuccessMessage"] = $"Đã lưu Node tri thức '{node.Label}' vào Neo4j thành công!";
        return RedirectToAction("KnowledgeMapAdmin");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteNode(string id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            await _neo4j.DeleteNodeAsync(id);
            TempData["SuccessMessage"] = $"Đã xóa Node '{id}' khỏi Neo4j.";
        }
        return RedirectToAction("KnowledgeMapAdmin");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveRelationship(GraphRelationship rel)
    {
        if (string.IsNullOrEmpty(rel.SourceId) || string.IsNullOrEmpty(rel.TargetId))
        {
            TempData["ErrorMessage"] = "Vui lòng chọn đầy đủ Node nguồn và Node đích.";
            return RedirectToAction("KnowledgeMapAdmin");
        }

        await _neo4j.AddRelationshipAsync(rel);
        TempData["SuccessMessage"] = "Đã lưu mối liên hệ giữa các hình vào Neo4j!";
        return RedirectToAction("KnowledgeMapAdmin");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteRelationship(string sourceId, string targetId, string type)
    {
        await _neo4j.DeleteRelationshipAsync(sourceId, targetId, type);
        TempData["SuccessMessage"] = "Đã xóa mối liên hệ khỏi Neo4j.";
        return RedirectToAction("KnowledgeMapAdmin");
    }
}
