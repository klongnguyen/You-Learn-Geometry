using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YouLearnGeometry.Models;

public class LearningProgress
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public string LessonId { get; set; } = string.Empty;

    // Trạng thái: "NotStarted", "Learning", "AwaitingTest", "Completed"
    public string Status { get; set; } = "NotStarted";

    public double BestMiniTestScore { get; set; } = 0.0;

    public int MiniTestAttempts { get; set; } = 0;

    public DateTime? CompletedAt { get; set; }

    public DateTime LastAccessedAt { get; set; } = DateTime.UtcNow;
}

public class AssessmentAttempt
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    // "MiniTest" | "TopicTest"
    public string AssessmentType { get; set; } = "MiniTest";

    // LessonId hoặc TopicCode
    public string TargetCode { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public double Score { get; set; } = 0.0; // Phần trăm: 0 -> 100

    public int CorrectCount { get; set; }

    public int TotalQuestions { get; set; }

    public bool IsPassed { get; set; } // MiniTest >= 75%, TopicTest >= 70%

    public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;
}

public class Achievement
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty; // "khoi_dau", "ben_bi", "bac_thay_tu_giac"

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Icon { get; set; } = string.Empty;
}
