using MongoDB.Driver;
using YouLearnGeometry.Models;

namespace YouLearnGeometry.Services;

public interface IDataSeeder
{
    Task SeedAllAsync();
}

public partial class DataSeeder : IDataSeeder
{
    private readonly IMongoDbService _mongo;
    private readonly INeo4jService _neo4j;

    public DataSeeder(IMongoDbService mongo, INeo4jService neo4j)
    {
        _mongo = mongo;
        _neo4j = neo4j;
    }

    public async Task SeedAllAsync()
    {
        await SeedUsersAsync();
        await SeedAchievementsAsync();
        await SeedCurriculumLevelsAsync();
        await SeedLessonsAsync();
        await _neo4j.InitializeSchemaAndSeedAsync();
    }

    private async Task SeedUsersAsync()
    {
        var count = await _mongo.Users.CountDocumentsAsync(Builders<User>.Filter.Empty);
        if (count > 0) return;

        var users = new List<User>
        {
            new User
            {
                Username = "admin",
                Email = "admin@ylg.edu.vn",
                Password = "admin",
                FullName = "Quản trị viên (Admin)",
                Role = "Admin",
                GradeLevel = "Lop8",
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Username = "longstudent",
                Email = "hocsinh@ylg.edu.vn",
                Password = "123",
                FullName = "Nguyễn Kim Long",
                Role = "Student",
                GradeLevel = "Lop6",
                StreakCount = 1,
                LastStreakDate = DateTime.UtcNow.Date,
                BadgesEarned = new List<string> { "khoi_dau" },
                CreatedAt = DateTime.UtcNow
            }
        };

        await _mongo.Users.InsertManyAsync(users);
    }

    private async Task SeedAchievementsAsync()
    {
        var achievements = new List<Achievement>
        {
            new Achievement
            {
                Code = "khoi_dau",
                Title = "Khởi đầu nan",
                Description = "Hoàn thành bài học hình học đầu tiên trên YLG",
                Icon = "🌱"
            },
            new Achievement
            {
                Code = "ben_bi",
                Title = "Chiến binh bền bỉ",
                Description = "Duy trì chuỗi học tập liên tục trong 3 ngày (Streak ≥ 3)",
                Icon = "🔥"
            },
            new Achievement
            {
                Code = "bac_thay_tu_giac",
                Title = "Bậc thầy Tứ giác",
                Description = "Đạt kết quả từ 80% trở lên trong bài kiểm tra Topic Test",
                Icon = "👑"
            },
            new Achievement
            {
                Code = "nha_hinh_hoc_tre",
                Title = "Nhà hình học xuất sắc",
                Description = "Vượt qua bài kiểm tra chủ đề Hình tròn hoặc Tam giác",
                Icon = "🏆"
            }
        };

        foreach (var ach in achievements)
        {
            var existing = await _mongo.Achievements.Find(a => a.Code == ach.Code).FirstOrDefaultAsync();
            if (existing != null)
            {
                ach.Id = existing.Id;
                await _mongo.Achievements.ReplaceOneAsync(a => a.Id == existing.Id, ach);
            }
            else
            {
                await _mongo.Achievements.InsertOneAsync(ach);
            }
        }
    }

    private async Task SeedCurriculumLevelsAsync()
    {
        var levels = new List<CurriculumLevel>
        {
            GetCurriculumLevelLop6(),
            GetCurriculumLevelLop7(),
            GetCurriculumLevelLop8(),
            GetCurriculumLevelLop9()
        };

        foreach (var lvl in levels)
        {
            var existing = await _mongo.CurriculumLevels
                .Find(c => c.GradeLevel == lvl.GradeLevel)
                .FirstOrDefaultAsync();

            if (existing != null)
            {
                lvl.Id = existing.Id;
                await _mongo.CurriculumLevels.ReplaceOneAsync(c => c.Id == existing.Id, lvl);
            }
            else
            {
                await _mongo.CurriculumLevels.InsertOneAsync(lvl);
            }
        }
    }

    private async Task SeedLessonsAsync()
    {
        var allLessons = new List<Lesson>();
        allLessons.AddRange(GetLessonsLop6());
        allLessons.AddRange(GetLessonsLop7());
        allLessons.AddRange(GetLessonsLop8());
        allLessons.AddRange(GetLessonsLop9());

        foreach (var lesson in allLessons)
        {
            var existing = await _mongo.Lessons
                .Find(l => l.LessonCode == lesson.LessonCode)
                .FirstOrDefaultAsync();

            if (existing != null)
            {
                lesson.Id = existing.Id;
                await _mongo.Lessons.ReplaceOneAsync(l => l.Id == existing.Id, lesson);
            }
            else
            {
                await _mongo.Lessons.InsertOneAsync(lesson);
            }
        }
    }
}
