using MongoDB.Driver;
using YouLearnGeometry.Models;

namespace YouLearnGeometry.Services;

public interface IMongoDbService
{
    IMongoDatabase Database { get; }
    IMongoCollection<User> Users { get; }
    IMongoCollection<CurriculumLevel> CurriculumLevels { get; }
    IMongoCollection<Lesson> Lessons { get; }
    IMongoCollection<LearningProgress> LearningProgress { get; }
    IMongoCollection<AssessmentAttempt> AssessmentAttempts { get; }
    IMongoCollection<Achievement> Achievements { get; }
}

public class MongoDbService : IMongoDbService
{
    private readonly IMongoDatabase _database;

    public MongoDbService(IConfiguration configuration)
    {
        var connectionString = configuration["MongoDb:ConnectionString"]
            ?? throw new InvalidOperationException("MongoDB ConnectionString is not configured.");
        var databaseName = configuration["MongoDb:DatabaseName"] ?? "YouLearnGeometryDb";

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoDatabase Database => _database;
    public IMongoCollection<User> Users => _database.GetCollection<User>("users");
    public IMongoCollection<CurriculumLevel> CurriculumLevels => _database.GetCollection<CurriculumLevel>("curriculum_levels");
    public IMongoCollection<Lesson> Lessons => _database.GetCollection<Lesson>("lessons");
    public IMongoCollection<LearningProgress> LearningProgress => _database.GetCollection<LearningProgress>("learning_progress");
    public IMongoCollection<AssessmentAttempt> AssessmentAttempts => _database.GetCollection<AssessmentAttempt>("assessment_attempts");
    public IMongoCollection<Achievement> Achievements => _database.GetCollection<Achievement>("achievements");
}
