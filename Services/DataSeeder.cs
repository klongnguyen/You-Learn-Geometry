using MongoDB.Driver;
using YouLearnGeometry.Models;

namespace YouLearnGeometry.Services;

public interface IDataSeeder
{
    Task SeedAllAsync();
}

public class DataSeeder : IDataSeeder
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
        var count = await _mongo.Achievements.CountDocumentsAsync(Builders<Achievement>.Filter.Empty);
        if (count > 0) return;

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
            }
        };

        await _mongo.Achievements.InsertManyAsync(achievements);
    }

    private async Task SeedCurriculumLevelsAsync()
    {
        var count = await _mongo.CurriculumLevels.CountDocumentsAsync(Builders<CurriculumLevel>.Filter.Empty);
        if (count > 0) return;

        var levels = new List<CurriculumLevel>
        {
            new CurriculumLevel
            {
                GradeLevel = "Lop6",
                Name = "Hình học phẳng Lớp 6",
                Description = "Khám phá các hình phẳng quen thuộc trong tự nhiên và đời sống: Hình vuông, Tam giác đều, Lục giác đều, Hình chữ nhật, Hình thoi, Hình bình hành, Hình thang cân.",
                Topics = new List<Topic>
                {
                    new Topic
                    {
                        TopicCode = "TU_GIAC_LOP6",
                        Title = "Chủ đề: Một số hình phẳng trong thực tiễn (Tứ giác)",
                        Description = "Quan sát, nhận biết các yếu tố cạnh, góc, đường chéo và tính chu vi, diện tích các hình phẳng.",
                        Order = 1,
                        TopicTest = GenerateTopicTestLop6()
                    }
                }
            },
            new CurriculumLevel
            {
                GradeLevel = "Lop8",
                Name = "Hình học phẳng Lớp 8",
                Description = "Nghiên cứu cấu trúc hình học suy luận: Định nghĩa, tính chất, dấu hiệu nhận biết tứ giác và phương pháp chứng minh hình học.",
                Topics = new List<Topic>
                {
                    new Topic
                    {
                        TopicCode = "TU_GIAC_LOP8",
                        Title = "Chủ đề: Tứ giác & Tính chất kế thừa",
                        Description = "Học thuyết về tứ giác lồi, hình thang, hình bình hành, hình chữ nhật, hình thoi, hình vuông và mối liên hệ giữa các hình.",
                        Order = 1,
                        TopicTest = GenerateTopicTestLop8()
                    }
                }
            }
        };

        await _mongo.CurriculumLevels.InsertManyAsync(levels);
    }

    private async Task SeedLessonsAsync()
    {
        var count = await _mongo.Lessons.CountDocumentsAsync(Builders<Lesson>.Filter.Empty);
        if (count > 0) return;

        var lessons = new List<Lesson>();

        // ==================== LỚP 6 ====================
        // Lesson 1: Hình vuông
        lessons.Add(new Lesson
        {
            LessonCode = "LESSON_6_HV",
            GradeLevel = "Lop6",
            TopicCode = "TU_GIAC_LOP6",
            Title = "Bài 1: Hình vuông",
            Order = 1,
            Summary = "Nhận biết hình vuông, các cạnh, đỉnh, góc và cách tính chu vi, diện tích hình vuông.",
            Definition = "Hình vuông ABCD có: 4 đỉnh A, B, C, D; 4 cạnh bằng nhau AB = BC = CD = DA; 4 góc bằng nhau và bằng góc vuông; 2 đường chéo bằng nhau AC = BD.",
            Properties = new List<string>
            {
                "Bốn cạnh đều có độ dài bằng nhau.",
                "Bốn góc đều là các góc vuông (bằng 90°).",
                "Hai đường chéo bằng nhau và vuông góc với nhau tại trung điểm của mỗi đường."
            },
            Formulas = new List<string>
            {
                "Chu vi hình vuông có cạnh a: P = 4 × a",
                "Diện tích hình vuông có cạnh a: S = a × a = a²"
            },
            VisualExample = "Cho hình vuông ABCD có độ dài cạnh a = 6 cm. Khi đó chu vi P = 4 × 6 = 24 cm; diện tích S = 6 × 6 = 36 cm².",
            RealWorldExample = "Viên gạch men lát nền nhà, ô vuông trên bàn cờ vua, mặt của rubik, khung cửa sổ vuông vức.",
            HasGeometryLab = true,
            DefaultLabShape = "HinhVuong",
            HasMiniTest = true,
            PracticeExercises = new List<PracticeExercise>
            {
                new PracticeExercise
                {
                    QuestionText = "Hình vuông có mấy góc vuông và mấy cạnh bằng nhau?",
                    Type = "single_choice",
                    Options = new List<string> { "2 góc vuông, 4 cạnh bằng nhau", "4 góc vuông, 4 cạnh bằng nhau", "4 góc vuông, 2 cạnh bằng nhau", "Không có góc vuông" },
                    CorrectAnswers = new List<string> { "4 góc vuông, 4 cạnh bằng nhau" },
                    Hint1 = "Hãy nhớ lại định nghĩa: hình vuông là hình cân đối nhất về cả góc lẫn cạnh.",
                    Hint2 = "Số góc vuông bằng 4 và cả 4 cạnh đều có độ dài bằng nhau.",
                    Explanation = "Định nghĩa: Hình vuông có 4 góc vuông và 4 cạnh bằng nhau."
                },
                new PracticeExercise
                {
                    QuestionText = "Một chiếc khăn quàng hình vuông có cạnh 20 cm. Chu vi của chiếc khăn là bao nhiêu cm?",
                    Type = "numeric",
                    Options = new List<string>(),
                    CorrectAnswers = new List<string> { "80" },
                    Hint1 = "Công thức chu vi hình vuông là P = 4 × a.",
                    Hint2 = "Tính: 4 nhân với 20.",
                    Explanation = "Chu vi chiếc khăn = 4 × 20 = 80 cm."
                },
                new PracticeExercise
                {
                    QuestionText = "Đặc điểm nào sau đây KHÔNG PHẢI là tính chất của hai đường chéo hình vuông?",
                    Type = "single_choice",
                    Options = new List<string> { "Bằng nhau", "Vuông góc với nhau", "Cắt nhau tại trung điểm", "Song song với nhau" },
                    CorrectAnswers = new List<string> { "Song song với nhau" },
                    Hint1 = "Hai đường chéo cắt nhau tạo thành dấu cộng (+) ở giữa hình.",
                    Hint2 = "Hai đường chéo cắt nhau thì không thể song song với nhau được.",
                    Explanation = "Hai đường chéo hình vuông cắt nhau và vuông góc nhau, chúng không bao giờ song song."
                }
            },
            MiniTest = new List<TestQuestion>
            {
                new TestQuestion
                {
                    QuestionText = "Câu 1: Hình vuông có độ dài cạnh là 5 cm. Diện tích của hình vuông là:",
                    Type = "single_choice",
                    Options = new List<string> { "20 cm²", "25 cm²", "10 cm²", "15 cm²" },
                    CorrectAnswers = new List<string> { "25 cm²" },
                    Explanation = "S = a × a = 5 × 5 = 25 cm²."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 2: Hai đường chéo của hình vuông có tính chất nào?",
                    Type = "single_choice",
                    Options = new List<string> { "Bằng nhau và vuông góc với nhau", "Không bằng nhau nhưng vuông góc", "Bằng nhau nhưng không vuông góc", "Song song với nhau" },
                    CorrectAnswers = new List<string> { "Bằng nhau và vuông góc với nhau" },
                    Explanation = "Hai đường chéo hình vuông vừa bằng nhau vừa vuông góc với nhau tại trung điểm."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 3: Một hình vuông có chu vi là 36 cm. Cạnh của hình vuông đó dài bao nhiêu?",
                    Type = "single_choice",
                    Options = new List<string> { "6 cm", "9 cm", "8 cm", "18 cm" },
                    CorrectAnswers = new List<string> { "9 cm" },
                    Explanation = "Cạnh a = P / 4 = 36 / 4 = 9 cm."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 4: Các góc của hình vuông có số đo bằng bao nhiêu độ?",
                    Type = "single_choice",
                    Options = new List<string> { "60°", "90°", "120°", "180°" },
                    CorrectAnswers = new List<string> { "90°" },
                    Explanation = "Góc vuông có số đo bằng 90°."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 5: Nhận định nào sau đây là ĐÚNG về hình vuông?",
                    Type = "single_choice",
                    Options = new List<string> { "Chỉ có hai góc vuông", "Chỉ có các cạnh đối bằng nhau", "Bốn cạnh bằng nhau và bốn góc bằng nhau", "Hai đường chéo không bằng nhau" },
                    CorrectAnswers = new List<string> { "Bốn cạnh bằng nhau và bốn góc bằng nhau" },
                    Explanation = "Hình vuông có 4 cạnh bằng nhau và 4 góc bằng nhau (đều bằng 90°)."
                }
            }
        });

        // Lesson 2: Hình chữ nhật
        lessons.Add(new Lesson
        {
            LessonCode = "LESSON_6_HCN",
            GradeLevel = "Lop6",
            TopicCode = "TU_GIAC_LOP6",
            Title = "Bài 2: Hình chữ nhật",
            Order = 2,
            Summary = "Đặc điểm nhận biết hình chữ nhật, tính chu vi và diện tích qua chiều dài và chiều rộng.",
            Definition = "Hình chữ nhật ABCD có: 4 đỉnh A, B, C, D; 2 cặp cạnh đối song song và bằng nhau (AB = CD, AD = BC); 4 góc vuông; 2 đường chéo bằng nhau và cắt nhau tại trung điểm của mỗi đường.",
            Properties = new List<string>
            {
                "Hai cạnh đối song song và có độ dài bằng nhau.",
                "Bốn góc đều là góc vuông (90°).",
                "Hai đường chéo bằng nhau và cắt nhau tại trung điểm mỗi đường."
            },
            Formulas = new List<string>
            {
                "Chu vi hình chữ nhật: P = (a + b) × 2 (với a là chiều dài, b là chiều rộng)",
                "Diện tích hình chữ nhật: S = a × b"
            },
            VisualExample = "Sân trường hình chữ nhật có chiều dài a = 30 m, chiều rộng b = 15 m. Chu vi P = (30 + 15) × 2 = 90 m. Diện tích S = 30 × 15 = 450 m².",
            RealWorldExample = "Mặt bàn học, màn hình tivi, sân bóng đá, trang sách giáo khoa, bìa phong bì thư.",
            HasGeometryLab = true,
            DefaultLabShape = "HinhChuNhat",
            HasMiniTest = true,
            PracticeExercises = new List<PracticeExercise>
            {
                new PracticeExercise
                {
                    QuestionText = "Hình chữ nhật có độ dài chiều dài là 8 cm, chiều rộng 5 cm. Diện tích của hình là:",
                    Type = "single_choice",
                    Options = new List<string> { "26 cm²", "40 cm²", "13 cm²", "80 cm²" },
                    CorrectAnswers = new List<string> { "40 cm²" },
                    Hint1 = "Diện tích hình chữ nhật bằng tích của chiều dài và chiều rộng: S = a × b.",
                    Hint2 = "Lấy 8 nhân với 5.",
                    Explanation = "S = 8 × 5 = 40 cm²."
                },
                new PracticeExercise
                {
                    QuestionText = "Hai đường chéo của hình chữ nhật có tính chất nào sau đây?",
                    Type = "single_choice",
                    Options = new List<string> { "Vuông góc với nhau", "Bằng nhau và cắt nhau tại trung điểm", "Song song với nhau", "Không bao giờ cắt nhau" },
                    CorrectAnswers = new List<string> { "Bằng nhau và cắt nhau tại trung điểm" },
                    Hint1 = "Đường chéo hình chữ nhật nối 2 đỉnh đối diện.",
                    Hint2 = "Chúng bằng nhau và chia đôi nhau tại giao điểm.",
                    Explanation = "Trong hình chữ nhật, 2 đường chéo bằng nhau và cắt nhau tại trung điểm."
                }
            },
            MiniTest = new List<TestQuestion>
            {
                new TestQuestion
                {
                    QuestionText = "Câu 1: Một mảnh vườn hình chữ nhật có chiều dài 12m, chiều rộng 8m. Chu vi mảnh vườn là:",
                    Type = "single_choice",
                    Options = new List<string> { "40 m", "96 m", "20 m", "48 m" },
                    CorrectAnswers = new List<string> { "40 m" },
                    Explanation = "P = (12 + 8) × 2 = 40 m."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 2: Số đo mỗi góc trong hình chữ nhật là bao nhiêu độ?",
                    Type = "single_choice",
                    Options = new List<string> { "45°", "60°", "90°", "180°" },
                    CorrectAnswers = new List<string> { "90°" },
                    Explanation = "Hình chữ nhật có 4 góc vuông (90°)."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 3: Khẳng định nào sau đây là SAI đối với hình chữ nhật?",
                    Type = "single_choice",
                    Options = new List<string> { "Các cạnh đối bằng nhau", "Bốn cạnh luôn bằng nhau", "Hai đường chéo bằng nhau", "Bốn góc đều bằng 90°" },
                    CorrectAnswers = new List<string> { "Bốn cạnh luôn bằng nhau" },
                    Explanation = "Bốn cạnh bằng nhau là hình thoi hoặc hình vuông, không phải tính chất chung của hình chữ nhật."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 4: Diện tích hình chữ nhật có chiều dài 10 cm và chiều rộng 6 cm là:",
                    Type = "single_choice",
                    Options = new List<string> { "32 cm²", "60 cm²", "16 cm²", "100 cm²" },
                    CorrectAnswers = new List<string> { "60 cm²" },
                    Explanation = "S = 10 × 6 = 60 cm²."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 5: Giao điểm hai đường chéo của hình chữ nhật cách đều 4 đỉnh không?",
                    Type = "single_choice",
                    Options = new List<string> { "Có, cách đều 4 đỉnh", "Không cách đều", "Chỉ cách đều 2 đỉnh", "Tùy thuộc vào kích thước" },
                    CorrectAnswers = new List<string> { "Có, cách đều 4 đỉnh" },
                    Explanation = "Vì 2 đường chéo bằng nhau và cắt nhau tại trung điểm nên giao điểm cách đều cả 4 đỉnh."
                }
            }
        });

        // Lesson 3: Hình thoi (BÀI KHÔNG CÓ MINI TEST -> BẤM HOÀN THÀNH LÀ COMPLETED NGAY)
        lessons.Add(new Lesson
        {
            LessonCode = "LESSON_6_HT",
            GradeLevel = "Lop6",
            TopicCode = "TU_GIAC_LOP6",
            Title = "Bài 3: Hình thoi (Đọc & Thực hành)",
            Order = 3,
            Summary = "Tìm hiểu hình thoi với 4 cạnh bằng nhau và 2 đường chéo vuông góc. Bài học nhấn mạnh kỹ năng quan sát thực hành.",
            Definition = "Hình thoi ABCD có: 4 cạnh bằng nhau (AB = BC = CD = DA); các cạnh đối song song; hai đường chéo vuông góc với nhau tại trung điểm mỗi đường.",
            Properties = new List<string>
            {
                "Bốn cạnh bằng nhau.",
                "Hai cặp cạnh đối song song.",
                "Các góc đối bằng nhau.",
                "Hai đường chéo vuông góc với nhau và cắt nhau tại trung điểm của mỗi đường."
            },
            Formulas = new List<string>
            {
                "Chu vi hình thoi có cạnh a: P = 4 × a",
                "Diện tích hình thoi có độ dài hai đường chéo m và n: S = (m × n) / 2"
            },
            VisualExample = "Hình thoi ABCD có độ dài hai đường chéo là 6 cm và 8 cm. Diện tích S = (6 × 8) / 2 = 24 cm².",
            RealWorldExample = "Cánh diều bay lượn, họa tiết hoa văn trên thổ cẩm Tây Nguyên, viên kẹo socola hình thoi.",
            HasGeometryLab = true,
            DefaultLabShape = "HinhThoi",
            HasMiniTest = false, // Không có Mini Test: bấm Đánh dấu hoàn thành là Completed ngay!
            PracticeExercises = new List<PracticeExercise>
            {
                new PracticeExercise
                {
                    QuestionText = "Hình thoi có độ dài hai đường chéo lần lượt là 10 cm và 14 cm. Diện tích là:",
                    Type = "single_choice",
                    Options = new List<string> { "140 cm²", "70 cm²", "48 cm²", "24 cm²" },
                    CorrectAnswers = new List<string> { "70 cm²" },
                    Hint1 = "Công thức diện tích hình thoi là S = (m × n) / 2.",
                    Hint2 = "Lấy (10 × 14) rồi chia cho 2.",
                    Explanation = "S = (10 × 14) / 2 = 70 cm²."
                }
            },
            MiniTest = new List<TestQuestion>()
        });

        // Lesson 4: Hình bình hành
        lessons.Add(new Lesson
        {
            LessonCode = "LESSON_6_HBH",
            GradeLevel = "Lop6",
            TopicCode = "TU_GIAC_LOP6",
            Title = "Bài 4: Hình bình hành",
            Order = 4,
            Summary = "Đặc điểm hai cặp cạnh đối song song và bằng nhau của hình bình hành.",
            Definition = "Hình bình hành ABCD có: các cạnh đối song song và bằng nhau (AB = CD, AD = BC); các góc đối bằng nhau; hai đường chéo cắt nhau tại trung điểm mỗi đường.",
            Properties = new List<string>
            {
                "Hai cặp cạnh đối song song và bằng nhau.",
                "Hai góc đối diện bằng nhau.",
                "Hai đường chéo cắt nhau tại trung điểm của mỗi đường."
            },
            Formulas = new List<string>
            {
                "Chu vi hình bình hành có 2 cạnh kề a và b: P = 2 × (a + b)",
                "Diện tích hình bình hành có đáy a và chiều cao h: S = a × h"
            },
            VisualExample = "Hình bình hành có cạnh đáy a = 12 cm, chiều cao tương ứng h = 5 cm. Diện tích S = 12 × 5 = 60 cm².",
            RealWorldExample = "Khung kim loại thanh chắn cầu thang xếp, giá để đồ gấp gọn, nan cửa sổ nghiêng.",
            HasGeometryLab = true,
            DefaultLabShape = "HinhBinhHanh",
            HasMiniTest = true,
            PracticeExercises = new List<PracticeExercise>
            {
                new PracticeExercise
                {
                    QuestionText = "Diện tích hình bình hành có cạnh đáy 15 cm và chiều cao 6 cm là:",
                    Type = "single_choice",
                    Options = new List<string> { "90 cm²", "45 cm²", "42 cm²", "21 cm²" },
                    CorrectAnswers = new List<string> { "90 cm²" },
                    Hint1 = "Công thức S = a × h.",
                    Hint2 = "15 nhân với 6 = 90.",
                    Explanation = "S = 15 × 6 = 90 cm²."
                }
            },
            MiniTest = new List<TestQuestion>
            {
                new TestQuestion
                {
                    QuestionText = "Câu 1: Hình bình hành có tính chất nào dưới đây?",
                    Type = "single_choice",
                    Options = new List<string> { "Bốn góc vuông", "Các cạnh đối song song và bằng nhau", "Bốn cạnh bằng nhau", "Hai đường chéo vuông góc" },
                    CorrectAnswers = new List<string> { "Các cạnh đối song song và bằng nhau" },
                    Explanation = "Định nghĩa hình bình hành: các cạnh đối song song và bằng nhau."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 2: Hai đường chéo hình bình hành cắt nhau tại vị trí nào?",
                    Type = "single_choice",
                    Options = new List<string> { "Tại trung điểm mỗi đường", "Tại 1/3 mỗi đường", "Không cắt nhau", "Tại đỉnh" },
                    CorrectAnswers = new List<string> { "Tại trung điểm mỗi đường" },
                    Explanation = "Hai đường chéo hình bình hành cắt nhau tại trung điểm của mỗi đường."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 3: Chu vi hình bình hành có 2 cạnh kề 7 cm và 4 cm là:",
                    Type = "single_choice",
                    Options = new List<string> { "22 cm", "28 cm", "11 cm", "14 cm" },
                    CorrectAnswers = new List<string> { "22 cm" },
                    Explanation = "P = (7 + 4) × 2 = 22 cm."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 4: Diện tích hình bình hành đáy 8 cm, chiều cao 4 cm là:",
                    Type = "single_choice",
                    Options = new List<string> { "32 cm²", "16 cm²", "24 cm²", "12 cm²" },
                    CorrectAnswers = new List<string> { "32 cm²" },
                    Explanation = "S = 8 × 4 = 32 cm²."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 5: Trong hình bình hành, hai góc đối có mối quan hệ gì?",
                    Type = "single_choice",
                    Options = new List<string> { "Bằng nhau", "Bù nhau", "Phụ nhau", "Vuông góc" },
                    CorrectAnswers = new List<string> { "Bằng nhau" },
                    Explanation = "Trong hình bình hành, các góc đối luôn bằng nhau."
                }
            }
        });

        // Lesson 5: Hình thang cân
        lessons.Add(new Lesson
        {
            LessonCode = "LESSON_6_HTC",
            GradeLevel = "Lop6",
            TopicCode = "TU_GIAC_LOP6",
            Title = "Bài 5: Hình thang cân",
            Order = 5,
            Summary = "Đặc điểm hình thang có hai cạnh đáy song song và hai cạnh bên bằng nhau.",
            Definition = "Hình thang cân là hình thang có hai góc kề một đáy bằng nhau, hai cạnh bên bằng nhau và hai đường chéo bằng nhau.",
            Properties = new List<string>
            {
                "Hai cạnh đáy song song với nhau.",
                "Hai cạnh bên có độ dài bằng nhau.",
                "Hai đường chéo bằng nhau."
            },
            Formulas = new List<string>
            {
                "Chu vi hình thang cân: P = đáy lớn + đáy nhỏ + 2 × cạnh bên",
                "Diện tích hình thang: S = [(đáy lớn + đáy nhỏ) × chiều cao] / 2"
            },
            VisualExample = "Hình thang có hai đáy là 6 cm và 10 cm, chiều cao là 4 cm. Diện tích S = [(6 + 10) × 4] / 2 = 32 cm².",
            RealWorldExample = "Chậu hoa hình chóp cụt, túi xách thời trang, mái đền, bậc thang gỗ.",
            HasGeometryLab = true,
            DefaultLabShape = "HinhThangCan",
            HasMiniTest = true,
            PracticeExercises = new List<PracticeExercise>
            {
                new PracticeExercise
                {
                    QuestionText = "Hình thang cân có hai đáy lần lượt là 4 cm và 8 cm, chiều cao 5 cm. Diện tích là:",
                    Type = "single_choice",
                    Options = new List<string> { "30 cm²", "60 cm²", "20 cm²", "15 cm²" },
                    CorrectAnswers = new List<string> { "30 cm²" },
                    Hint1 = "Công thức tính diện tích hình thang: S = [(a + b) × h] / 2.",
                    Hint2 = "[(4 + 8) × 5] / 2 = [12 × 5] / 2 = 30.",
                    Explanation = "S = [(4 + 8) × 5] / 2 = 30 cm²."
                }
            },
            MiniTest = new List<TestQuestion>
            {
                new TestQuestion
                {
                    QuestionText = "Câu 1: Hình thang cân có đặc điểm nào dưới đây?",
                    Type = "single_choice",
                    Options = new List<string> { "Hai cạnh bên bằng nhau", "Bốn cạnh bằng nhau", "Bốn góc bằng nhau", "Hai đáy vuông góc" },
                    CorrectAnswers = new List<string> { "Hai cạnh bên bằng nhau" },
                    Explanation = "Trong hình thang cân, hai cạnh bên bằng nhau và hai đường chéo bằng nhau."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 2: Hai đường chéo của hình thang cân:",
                    Type = "single_choice",
                    Options = new List<string> { "Bằng nhau", "Vuông góc", "Song song", "Cắt nhau ở 1/3" },
                    CorrectAnswers = new List<string> { "Bằng nhau" },
                    Explanation = "Hai đường chéo của hình thang cân luôn có độ dài bằng nhau."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 3: Đáy lớn 14cm, đáy nhỏ 6cm, chiều cao 5cm. Diện tích là:",
                    Type = "single_choice",
                    Options = new List<string> { "50 cm²", "100 cm²", "25 cm²", "40 cm²" },
                    CorrectAnswers = new List<string> { "50 cm²" },
                    Explanation = "S = [(14 + 6) × 5] / 2 = 50 cm²."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 4: Hai góc kề một đáy của hình thang cân:",
                    Type = "single_choice",
                    Options = new List<string> { "Bằng nhau", "Bù nhau", "Có tổng bằng 90°", "Khác nhau" },
                    CorrectAnswers = new List<string> { "Bằng nhau" },
                    Explanation = "Trong hình thang cân, hai góc kề một đáy luôn bằng nhau."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 5: Nhận định nào sau đây là ĐÚNG?",
                    Type = "single_choice",
                    Options = new List<string> { "Hình thang có 2 đáy song song", "Hình thang có 4 cạnh bằng nhau", "Hình thang có 4 góc vuông", "Hình thang có 2 đường chéo song song" },
                    CorrectAnswers = new List<string> { "Hình thang có 2 đáy song song" },
                    Explanation = "Định nghĩa hình thang là tứ giác có hai cạnh đối song song (hai đáy)."
                }
            }
        });

        // ==================== LỚP 8 ====================
        lessons.Add(new Lesson
        {
            LessonCode = "LESSON_8_TG",
            GradeLevel = "Lop8",
            TopicCode = "TU_GIAC_LOP8",
            Title = "Bài 1: Tứ giác & Định lý tổng các góc trong tứ giác",
            Order = 1,
            Summary = "Định nghĩa tứ giác lồi và định lý tổng bốn góc trong tứ giác bằng 360°.",
            Definition = "Tứ giác ABCD là hình gồm bốn đoạn thẳng AB, BC, CD, DA trong đó bất kì hai đoạn thẳng nào cũng không cùng nằm trên một đường thẳng. Tứ giác lồi là tứ giác luôn nằm trong một nửa mặt phẳng có bờ là đường thẳng chứa bất kì cạnh nào của tứ giác.",
            Properties = new List<string>
            {
                "Định lý cốt lõi: Tổng các góc của một tứ giác luôn bằng 360° (Góc A + B + C + D = 360°).",
                "Mỗi góc ngoài của tứ giác kề bù với góc trong tại đỉnh đó."
            },
            Formulas = new List<string> { "Â + B̂ + Ĉ + D̂ = 360°" },
            VisualExample = "Tứ giác ABCD có góc A = 70°, B = 110°, C = 80°. Khi đó góc D = 360° - (70° + 110° + 80°) = 100°.",
            RealWorldExample = "Bản đồ phân khu đất đai tứ giác, cánh buồm của thuyền buồm.",
            HasGeometryLab = false,
            HasMiniTest = true,
            PracticeExercises = new List<PracticeExercise>
            {
                new PracticeExercise
                {
                    QuestionText = "Tứ giác ABCD có Â = 65°, B̂ = 117°, Ĉ = 71°. Tính số đo góc D̂:",
                    Type = "numeric",
                    Options = new List<string>(),
                    CorrectAnswers = new List<string> { "107" },
                    Hint1 = "Tổng bốn góc của tứ giác bằng 360°.",
                    Hint2 = "D̂ = 360° - (65° + 117° + 71°).",
                    Explanation = "D̂ = 360° - 253° = 107°."
                }
            },
            MiniTest = new List<TestQuestion>
            {
                new TestQuestion
                {
                    QuestionText = "Câu 1: Tổng các góc của một tứ giác bằng bao nhiêu?",
                    Type = "single_choice",
                    Options = new List<string> { "180°", "360°", "540°", "720°" },
                    CorrectAnswers = new List<string> { "360°" },
                    Explanation = "Định lý: Tổng các góc của một tứ giác bằng 360°."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 2: Tứ giác có 3 góc vuông thì góc còn lại là góc gì?",
                    Type = "single_choice",
                    Options = new List<string> { "Góc nhọn", "Góc vuông (90°)", "Góc tù", "Góc bẹt" },
                    CorrectAnswers = new List<string> { "Góc vuông (90°)" },
                    Explanation = "Góc còn lại = 360° - (90° × 3) = 90° (góc vuông)."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 3: Một tứ giác có thể có nhiều nhất bao nhiêu góc nhọn?",
                    Type = "single_choice",
                    Options = new List<string> { "1", "2", "3", "4" },
                    CorrectAnswers = new List<string> { "3" },
                    Explanation = "Nếu cả 4 góc đều nhọn (< 90°) thì tổng < 360°, vô lý. Do đó tối đa 3 góc nhọn."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 4: Tứ giác có 4 góc bằng nhau thì số đo mỗi góc là:",
                    Type = "single_choice",
                    Options = new List<string> { "60°", "90°", "120°", "45°" },
                    CorrectAnswers = new List<string> { "90°" },
                    Explanation = "360° / 4 = 90°."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 5: Tứ giác ABCD có góc ngoài tại đỉnh A là 110°. Góc trong tại đỉnh A bằng:",
                    Type = "single_choice",
                    Options = new List<string> { "70°", "110°", "90°", "250°" },
                    CorrectAnswers = new List<string> { "70°" },
                    Explanation = "Góc trong và góc ngoài kề bù nên bằng 180° - 110° = 70°."
                }
            }
        });

        // Lesson 2 Lớp 8: Hình vuông & Tính kế thừa
        lessons.Add(new Lesson
        {
            LessonCode = "LESSON_8_HV",
            GradeLevel = "Lop8",
            TopicCode = "TU_GIAC_LOP8",
            Title = "Bài 2: Hình vuông & Cây tính chất kế thừa",
            Order = 2,
            Summary = "Mối liên hệ sâu sắc giữa Hình vuông, Hình chữ nhật, Hình thoi và Hình bình hành trong các bài toán chứng minh.",
            Definition = "Hình vuông là tứ giác vừa có 4 góc vuông (hình chữ nhật) vừa có 4 cạnh bằng nhau (hình thoi). Hình vuông kế thừa toàn bộ tính chất của cả hai lớp hình này.",
            Properties = new List<string>
            {
                "Kế thừa Hình chữ nhật: 2 đường chéo bằng nhau, 4 góc vuông.",
                "Kế thừa Hình thoi: 2 đường chéo vuông góc với nhau và là các đường phân giác.",
                "Kế thừa Hình bình hành: Các cạnh đối song song và bằng nhau, giao điểm đường chéo là tâm đối xứng."
            },
            Formulas = new List<string> { "S = a²", "d = a√2 (với d là đường chéo)" },
            VisualExample = "Khi chứng minh tứ giác ABCD là hình vuông, ta có thể chứng minh nó là Hình chữ nhật có 2 đường chéo vuông góc, hoặc là Hình thoi có một góc vuông.",
            RealWorldExample = "Áp dụng định lý trong thiết kế kiến trúc đối xứng, cắt gọt vật liệu xây dựng.",
            HasGeometryLab = true,
            DefaultLabShape = "HinhVuong",
            HasMiniTest = true,
            PracticeExercises = new List<PracticeExercise>
            {
                new PracticeExercise
                {
                    QuestionText = "Dấu hiệu nào sau đây chứng minh một hình chữ nhật là hình vuông?",
                    Type = "single_choice",
                    Options = new List<string> { "Có hai đường chéo bằng nhau", "Có hai đường chéo vuông góc với nhau", "Có 4 góc vuông", "Có các cạnh đối song song" },
                    CorrectAnswers = new List<string> { "Có hai đường chéo vuông góc với nhau" },
                    Hint1 = "Hình chữ nhật vốn đã có 2 đường chéo bằng nhau rồi.",
                    Hint2 = "Cần thêm tính chất đặc trưng của hình thoi: hai đường chéo vuông góc.",
                    Explanation = "Dấu hiệu nhận biết: Hình chữ nhật có hai đường chéo vuông góc với nhau là hình vuông."
                }
            },
            MiniTest = new List<TestQuestion>
            {
                new TestQuestion
                {
                    QuestionText = "Câu 1: Hình thoi có thêm điều kiện nào thì trở thành hình vuông?",
                    Type = "single_choice",
                    Options = new List<string> { "Có một góc vuông", "Có 2 đường chéo vuông góc", "Có 4 cạnh bằng nhau", "Có 2 cạnh đối song song" },
                    CorrectAnswers = new List<string> { "Có một góc vuông" },
                    Explanation = "Hình thoi có một góc vuông là hình vuông."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 2: Hình vuông có bao nhiêu trục đối xứng?",
                    Type = "single_choice",
                    Options = new List<string> { "1", "2", "4", "Vô số" },
                    CorrectAnswers = new List<string> { "4" },
                    Explanation = "Hình vuông có 4 trục đối xứng (2 đường trung trực và 2 đường chéo)."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 3: Giao điểm hai đường chéo hình vuông là:",
                    Type = "single_choice",
                    Options = new List<string> { "Tâm đối xứng", "Trọng tâm", "Trực tâm", "Không có ý nghĩa" },
                    CorrectAnswers = new List<string> { "Tâm đối xứng" },
                    Explanation = "Giao điểm 2 đường chéo hình vuông là tâm đối xứng của hình."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 4: Đường chéo hình vuông cạnh a có độ dài bằng:",
                    Type = "single_choice",
                    Options = new List<string> { "a", "2a", "a√2", "a²" },
                    CorrectAnswers = new List<string> { "a√2" },
                    Explanation = "Theo định lý Pytago: d² = a² + a² = 2a² => d = a√2."
                },
                new TestQuestion
                {
                    QuestionText = "Câu 5: Nhận định nào sau đây là SAI?",
                    Type = "single_choice",
                    Options = new List<string> { "Mọi hình vuông đều là hình chữ nhật", "Mọi hình vuông đều là hình thoi", "Mọi hình chữ nhật đều là hình vuông", "Mọi hình vuông đều là hình bình hành" },
                    CorrectAnswers = new List<string> { "Mọi hình chữ nhật đều là hình vuông" },
                    Explanation = "Hình chữ nhật chỉ là hình vuông khi có thêm 2 cạnh kề bằng nhau hoặc 2 đường chéo vuông góc."
                }
            }
        });

        await _mongo.Lessons.InsertManyAsync(lessons);
    }

    private static List<TestQuestion> GenerateTopicTestLop6()
    {
        return new List<TestQuestion>
        {
            new TestQuestion { QuestionText = "Câu 1: Hình nào có 4 cạnh bằng nhau và 4 góc vuông?", Options = new List<string> { "Hình chữ nhật", "Hình vuông", "Hình thoi", "Hình thang" }, CorrectAnswers = new List<string> { "Hình vuông" } },
            new TestQuestion { QuestionText = "Câu 2: Diện tích hình vuông cạnh 7 cm là:", Options = new List<string> { "28 cm²", "49 cm²", "14 cm²", "21 cm²" }, CorrectAnswers = new List<string> { "49 cm²" } },
            new TestQuestion { QuestionText = "Câu 3: Hình chữ nhật có chu vi 30 cm, chiều dài 9 cm. Chiều rộng là:", Options = new List<string> { "6 cm", "12 cm", "21 cm", "3 cm" }, CorrectAnswers = new List<string> { "6 cm" } },
            new TestQuestion { QuestionText = "Câu 4: Diện tích hình thoi có hai đường chéo 8 cm và 12 cm là:", Options = new List<string> { "96 cm²", "48 cm²", "20 cm²", "24 cm²" }, CorrectAnswers = new List<string> { "48 cm²" } },
            new TestQuestion { QuestionText = "Câu 5: Hình bình hành có diện tích 54 cm², cạnh đáy 9 cm. Chiều cao là:", Options = new List<string> { "6 cm", "12 cm", "3 cm", "27 cm" }, CorrectAnswers = new List<string> { "6 cm" } },
            new TestQuestion { QuestionText = "Câu 6: Hình thang cân có đặc điểm nào về hai đường chéo?", Options = new List<string> { "Bằng nhau", "Vuông góc", "Song song", "Trùng nhau" }, CorrectAnswers = new List<string> { "Bằng nhau" } },
            new TestQuestion { QuestionText = "Câu 7: Chu vi hình thoi có cạnh 5 cm là:", Options = new List<string> { "20 cm", "25 cm", "10 cm", "15 cm" }, CorrectAnswers = new List<string> { "20 cm" } },
            new TestQuestion { QuestionText = "Câu 8: Cặp cạnh đối trong hình chữ nhật có tính chất gì?", Options = new List<string> { "Song song và bằng nhau", "Vuông góc với nhau", "Không bằng nhau", "Cắt nhau tại đỉnh" }, CorrectAnswers = new List<string> { "Song song và bằng nhau" } },
            new TestQuestion { QuestionText = "Câu 9: Diện tích hình thang có hai đáy 5cm, 7cm và chiều cao 4cm là:", Options = new List<string> { "24 cm²", "48 cm²", "12 cm²", "16 cm²" }, CorrectAnswers = new List<string> { "24 cm²" } },
            new TestQuestion { QuestionText = "Câu 10: Hình nào sau đây KHÔNG có tâm đối xứng?", Options = new List<string> { "Hình vuông", "Hình chữ nhật", "Hình bình hành", "Hình thang cân (không phải hình chữ nhật)" }, CorrectAnswers = new List<string> { "Hình thang cân (không phải hình chữ nhật)" } }
        };
    }

    private static List<TestQuestion> GenerateTopicTestLop8()
    {
        return new List<TestQuestion>
        {
            new TestQuestion { QuestionText = "Câu 1: Tổng 4 góc của một tứ giác luôn bằng:", Options = new List<string> { "180°", "360°", "540°", "720°" }, CorrectAnswers = new List<string> { "360°" } },
            new TestQuestion { QuestionText = "Câu 2: Hình thang là tứ giác có:", Options = new List<string> { "2 cạnh đối song song", "4 cạnh bằng nhau", "2 đường chéo bằng nhau", "4 góc vuông" }, CorrectAnswers = new List<string> { "2 cạnh đối song song" } },
            new TestQuestion { QuestionText = "Câu 3: Hình thang cân là hình thang có:", Options = new List<string> { "Hai góc kề một đáy bằng nhau", "Hai cạnh đáy bằng nhau", "Hai đường chéo vuông góc", "Bốn cạnh bằng nhau" }, CorrectAnswers = new List<string> { "Hai góc kề một đáy bằng nhau" } },
            new TestQuestion { QuestionText = "Câu 4: Dấu hiệu nhận biết hình bình hành là:", Options = new List<string> { "Tứ giác có các cạnh đối song song", "Tứ giác có 2 đường chéo bằng nhau", "Tứ giác có 1 góc vuông", "Tứ giác có 2 cạnh kề bằng nhau" }, CorrectAnswers = new List<string> { "Tứ giác có các cạnh đối song song" } },
            new TestQuestion { QuestionText = "Câu 5: Hình chữ nhật là hình bình hành có:", Options = new List<string> { "Một góc vuông", "Hai cạnh kề bằng nhau", "Hai đường chéo vuông góc", "Bốn cạnh bằng nhau" }, CorrectAnswers = new List<string> { "Một góc vuông" } },
            new TestQuestion { QuestionText = "Câu 6: Hình thoi là hình bình hành có:", Options = new List<string> { "Hai đường chéo vuông góc với nhau", "Hai đường chéo bằng nhau", "Một góc vuông", "Hai góc kề bù nhau" }, CorrectAnswers = new List<string> { "Hai đường chéo vuông góc với nhau" } },
            new TestQuestion { QuestionText = "Câu 7: Hình vuông là hình chữ nhật có:", Options = new List<string> { "Hai cạnh kề bằng nhau", "Hai đường chéo bằng nhau", "Bốn góc vuông", "Hai cạnh đối song song" }, CorrectAnswers = new List<string> { "Hai cạnh kề bằng nhau" } },
            new TestQuestion { QuestionText = "Câu 8: Trong hình chữ nhật, giao điểm hai đường chéo là:", Options = new List<string> { "Tâm đối xứng", "Trọng tâm", "Trực tâm", "Đỉnh" }, CorrectAnswers = new List<string> { "Tâm đối xứng" } },
            new TestQuestion { QuestionText = "Câu 9: Tứ giác có 2 đường chéo vuông góc tại trung điểm mỗi đường là:", Options = new List<string> { "Hình thoi", "Hình chữ nhật", "Hình thang cân", "Tứ giác thường" }, CorrectAnswers = new List<string> { "Hình thoi" } },
            new TestQuestion { QuestionText = "Câu 10: Tứ giác vừa là hình chữ nhật vừa là hình thoi là:", Options = new List<string> { "Hình vuông", "Hình bình hành", "Hình thang cân", "Không tồn tại" }, CorrectAnswers = new List<string> { "Hình vuông" } }
        };
    }
}
