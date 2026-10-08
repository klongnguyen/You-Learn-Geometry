using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YouLearnGeometry.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty; // Plain text as agreed for demo

    public string FullName { get; set; } = string.Empty;

    public string Role { get; set; } = "Student"; // "Student" | "Admin"

    public string GradeLevel { get; set; } = "Lop6"; // "Lop6" | "Lop8"

    public int StreakCount { get; set; } = 0;

    public DateTime? LastStreakDate { get; set; }

    public List<string> BadgesEarned { get; set; } = new();

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
