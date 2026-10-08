using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YouLearnGeometry.Models;

public class Lesson
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public string LessonCode { get; set; } = string.Empty;

    public string GradeLevel { get; set; } = string.Empty; // "Lop6" | "Lop8"

    public string TopicCode { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public int Order { get; set; }

    public string Summary { get; set; } = string.Empty;

    // Chuẩn cấu trúc Lesson:
    // 1. Khái niệm / Định nghĩa
    public string Definition { get; set; } = string.Empty;

    // 2. Tính chất
    public List<string> Properties { get; set; } = new();

    // 3. Công thức
    public List<string> Formulas { get; set; } = new();

    // 4. Ví dụ trực quan
    public string VisualExample { get; set; } = string.Empty;

    // 5. Ví dụ thực tế
    public string RealWorldExample { get; set; } = string.Empty;

    // 6. Geometry Lab (Tùy chọn do Admin bật/tắt)
    public bool HasGeometryLab { get; set; } = false;

    public string DefaultLabShape { get; set; } = "HinhVuong"; // "HinhVuong", "HinhChuNhat", "HinhThoi", "HinhBinhHanh", "HinhThangCan"

    // 7. Đánh dấu hoàn thành / Mini Test (Tùy chọn)
    public bool HasMiniTest { get; set; } = false;

    // 8. Practice exercises (với hệ thống gợi ý Hint)
    public List<PracticeExercise> PracticeExercises { get; set; } = new();

    // 9. Mini Test (5-6 câu, ngưỡng 75%)
    public List<TestQuestion> MiniTest { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
