using YouLearnGeometry.Models;

namespace YouLearnGeometry.ViewModels;

public class DashboardViewModel
{
    public User User { get; set; } = new();
    public Lesson? NextLesson { get; set; }
    public string? NextLessonStatus { get; set; }
    public int TotalLessonsInGrade { get; set; }
    public int CompletedLessonsCount { get; set; }
    public int AwaitingTestLessonsCount { get; set; }
    public int CompletedTestsCount { get; set; }
    public double ProgressPercentage => TotalLessonsInGrade > 0 ? Math.Round((double)CompletedLessonsCount / TotalLessonsInGrade * 100, 1) : 0;
    public List<LessonProgressItem> LessonItems { get; set; } = new();
    public List<Achievement> Badges { get; set; } = new();
    public List<AssessmentAttempt> RecentAttempts { get; set; } = new();
}

public class LessonProgressItem
{
    public Lesson Lesson { get; set; } = new();
    public LearningProgress? Progress { get; set; }
    public string StatusBadgeClass => Progress?.Status switch
    {
        "Completed" => "bg-success text-white",
        "AwaitingTest" => "bg-warning text-dark",
        "Learning" => "bg-info text-white",
        _ => "bg-secondary text-white"
    };
    public string StatusText => Progress?.Status switch
    {
        "Completed" => "Đã hoàn thành",
        "AwaitingTest" => "Chờ kiểm tra (Mini Test)",
        "Learning" => "Đang học",
        _ => "Chưa học"
    };
}

public class CurriculumViewModel
{
    public string GradeLevel { get; set; } = "Lop6";
    public string GradeTitle { get; set; } = "Hình học Lớp 6";
    public List<TopicItemViewModel> Topics { get; set; } = new();
    public bool IsTopicTestUnlocked { get; set; }
    public AssessmentAttempt? LatestTopicTestAttempt { get; set; }
}

public class TopicItemViewModel
{
    public Topic Topic { get; set; } = new();
    public List<LessonProgressItem> Lessons { get; set; } = new();
}

public class LessonDetailViewModel
{
    public Lesson Lesson { get; set; } = new();
    public LearningProgress? Progress { get; set; }
    public string CurrentStatus => Progress?.Status ?? "NotStarted";
    public Lesson? PreviousLesson { get; set; }
    public Lesson? NextLesson { get; set; }
    public string GradeTitle { get; set; } = "Hình học THCS";
    public string CurrentTopicCode { get; set; } = string.Empty;
    public List<TopicItemViewModel> CourseTopics { get; set; } = new();
}

public class PracticeSubmitModel
{
    public string LessonId { get; set; } = string.Empty;
    public string ExerciseId { get; set; } = string.Empty;
    public string UserAnswer { get; set; } = string.Empty;
    public int AttemptCount { get; set; }
}

public class PracticeResultModel
{
    public bool IsCorrect { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Hint { get; set; }
    public bool AllowViewAnswer { get; set; }
    public string? CorrectAnswerText { get; set; }
    public string? Explanation { get; set; }
}

public class MiniTestSubmitModel
{
    public string LessonId { get; set; } = string.Empty;
    public List<QuestionAnswerDto> Answers { get; set; } = new();
}

public class QuestionAnswerDto
{
    public string QuestionId { get; set; } = string.Empty;
    public string SelectedOption { get; set; } = string.Empty;
}

public class TestResultViewModel
{
    public string Title { get; set; } = string.Empty;
    public string TargetCode { get; set; } = string.Empty;
    public string AssessmentType { get; set; } = "MiniTest";
    public int CorrectCount { get; set; }
    public int TotalQuestions { get; set; }
    public double Score { get; set; }
    public bool IsPassed { get; set; }
    public double Threshold { get; set; }
    public bool HasNewlyCompletedLesson { get; set; }
    public int UpdatedStreak { get; set; }
    public List<string> NewBadgesEarned { get; set; } = new();
    public string BackUrl { get; set; } = "/Dashboard";
}

public class PropertyBuilderViewModel
{
    public List<string> AvailableProperties { get; set; } = new();
    public List<string> SelectedProperties { get; set; } = new();
    public string DeduceState { get; set; } = "Initial"; // "Initial", "Sufficient", "Insufficient", "NoMatch"
    public string MatchedShapeName { get; set; } = string.Empty;
    public string MatchedShapeCode { get; set; } = string.Empty;
    public string Explanation { get; set; } = string.Empty;
    public List<string> CandidateShapes { get; set; } = new();
}
