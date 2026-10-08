using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YouLearnGeometry.Models;

public class CurriculumLevel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public string GradeLevel { get; set; } = string.Empty; // "Lop6" | "Lop8"

    public string Name { get; set; } = string.Empty; // "Hình học phẳng Lớp 6"

    public string Description { get; set; } = string.Empty;

    public List<Topic> Topics { get; set; } = new();
}

public class Topic
{
    public string TopicCode { get; set; } = string.Empty; // "TU_GIAC_LOP6", "TU_GIAC_LOP8"

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Order { get; set; }

    public List<TestQuestion> TopicTest { get; set; } = new();
}
