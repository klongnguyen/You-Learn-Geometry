using YouLearnGeometry.Models;

namespace YouLearnGeometry.ViewModels;

public class AdminDashboardViewModel
{
    public long TotalUsers { get; set; }
    public long TotalLessons { get; set; }
    public long TotalCompletedLessons { get; set; }
    public double AverageTestScore { get; set; }
    public double OverallCompletionRate { get; set; }
    public List<User> RecentUsers { get; set; } = new();
    public List<Lesson> Lessons { get; set; } = new();
}

public class AdminLessonEditModel
{
    public string? Id { get; set; }
    public string LessonCode { get; set; } = string.Empty;
    public string GradeLevel { get; set; } = "Lop6";
    public string TopicCode { get; set; } = "TU_GIAC_LOP6";
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; } = 1;
    public string Summary { get; set; } = string.Empty;
    public string Definition { get; set; } = string.Empty;
    public string PropertiesText { get; set; } = string.Empty; // newline-separated
    public string FormulasText { get; set; } = string.Empty; // newline-separated
    public string VisualExample { get; set; } = string.Empty;
    public string RealWorldExample { get; set; } = string.Empty;
    public bool HasGeometryLab { get; set; } = true;
    public string DefaultLabShape { get; set; } = "HinhVuong";
    public bool HasMiniTest { get; set; } = true;
}
