using YouLearnGeometry.Models;

namespace YouLearnGeometry.Services;

public partial class DataSeeder
{
    private static CurriculumLevel GetCurriculumLevelLop6()
    {
        return new CurriculumLevel
        {
            GradeLevel = "Lop6",
            Name = "Hình học phẳng Lớp 6 - Kết nối tri thức",
            Description = "Khám phá các hình phẳng quen thuộc trong tự nhiên và đời sống: Tam giác đều, Hình vuông, Lục giác đều, Hình chữ nhật, Hình thoi, Hình bình hành, Hình thang cân và tính đối xứng.",
            Topics = new List<Topic>
            {
                new Topic
                {
                    TopicCode = "HINH_PHANG_THUC_TIEN_6",
                    Title = "Chủ đề 1: Một số hình phẳng trong thực tiễn",
                    Description = "Quan sát, nhận biết các yếu tố cạnh, góc, đường chéo và tính chu vi, diện tích các hình phẳng quen thuộc.",
                    Order = 1,
                    TopicTest = GenerateTopicTestLop6_HinhPhang()
                },
                new Topic
                {
                    TopicCode = "TINH_DOI_XUNG_6",
                    Title = "Chủ đề 2: Tính đối xứng của hình phẳng trong tự nhiên",
                    Description = "Khám phá vẻ đẹp cân đối hài hòa của vạn vật qua trục đối xứng và tâm đối xứng.",
                    Order = 2,
                    TopicTest = GenerateTopicTestLop6_DoiXung()
                }
            }
        };
    }

    private static List<Lesson> GetLessonsLop6()
    {
        return new List<Lesson>
        {
            // Bài 1: Tam giác đều, hình vuông, lục giác đều
            new Lesson
            {
                LessonCode = "LESSON_6_TAM_GIAC_DEU_VUONG_LUC_GIAC",
                GradeLevel = "Lop6",
                TopicCode = "HINH_PHANG_THUC_TIEN_6",
                Title = "Bài 18: Tam giác đều. Hình vuông. Lục giác đều",
                Order = 1,
                Summary = "Nhận biết các hình đa giác đều trực quan: Tam giác đều (3 cạnh bằng nhau, 3 góc bằng nhau), Hình vuông (4 cạnh bằng nhau, 4 góc vuông), Lục giác đều (6 cạnh bằng nhau, 3 đường chéo chính).",
                Definition = "• Tam giác đều ABC có: 3 đỉnh A, B, C; 3 cạnh bằng nhau AB = BC = CA; 3 góc bằng nhau và bằng 60°.\n• Hình vuông ABCD có: 4 đỉnh; 4 cạnh bằng nhau AB = BC = CD = DA; 4 góc bằng nhau và bằng góc vuông (90°); 2 đường chéo bằng nhau AC = BD.\n• Lục giác đều ABCDEF có: 6 cạnh bằng nhau; 6 góc bằng nhau; 3 đường chéo chính AD, BE, CF bằng nhau và cắt nhau tại trung điểm O.",
                Properties = new List<string>
                {
                    "Tam giác đều có 3 trục đối xứng đi qua mỗi đỉnh và trung điểm cạnh đối diện.",
                    "Hình vuông có 4 trục đối xứng (2 đường chéo và 2 đường nối trung điểm các cạnh đối) và 1 tâm đối xứng.",
                    "Lục giác đều ghép từ 6 tam giác đều bằng nhau có chung đỉnh tâm O."
                },
                Formulas = new List<string>
                {
                    "Chu vi tam giác đều cạnh a: P = 3 × a",
                    "Chu vi hình vuông cạnh a: P = 4 × a; Diện tích: S = a²",
                    "Chu vi lục giác đều cạnh a: P = 6 × a"
                },
                VisualExample = "Một viên gạch men hình vuông có cạnh 30 cm có chu vi P = 4 × 30 = 120 cm và diện tích S = 30 × 30 = 900 cm².",
                RealWorldExample = "Biển báo giao thông hình tam giác đều cảnh báo nguy hiểm, ô gạch bông hình lục giác trên vỉa hè, mặt cắt ngang của tổ ong mật.",
                HasGeometryLab = true,
                DefaultLabShape = "HinhVuong",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Tam giác đều ABC có cạnh AB = 7 cm. Chu vi của tam giác ABC bằng bao nhiêu cm?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "21" },
                        Hint1 = "Tam giác đều có 3 cạnh có độ dài bằng nhau.",
                        Hint2 = "Công thức chu vi P = 3 × a = 3 × 7.",
                        Explanation = "Chu vi tam giác đều ABC là P = 3 × 7 = 21 cm."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Lục giác đều có bao nhiêu đường chéo chính nối các đỉnh đối diện?",
                        Type = "single_choice",
                        Options = new List<string> { "2 đường chéo", "3 đường chéo", "4 đường chéo", "6 đường chéo" },
                        CorrectAnswers = new List<string> { "3 đường chéo" },
                        Hint1 = "Mỗi đường chéo chính nối một cặp đỉnh đối diện qua tâm.",
                        Hint2 = "Có 6 đỉnh nên tạo được 6 ÷ 2 = 3 đường chéo chính.",
                        Explanation = "Lục giác đều có 3 đường chéo chính cắt nhau tại tâm hình lục giác."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Hình vuông có đặc điểm nào sau đây về các góc?",
                        Type = "single_choice",
                        Options = new List<string> { "Có 4 góc vuông (90°)", "Có 2 góc nhọn và 2 góc tù", "Có 3 góc vuông", "Các góc đều bằng 60°" },
                        CorrectAnswers = new List<string> { "Có 4 góc vuông (90°)" },
                        Hint1 = "Hình vuông có tất cả các góc bằng nhau.",
                        Hint2 = "Mỗi góc trong hình vuông đều là một góc vuông 90°.",
                        Explanation = "Hình vuông có 4 cạnh bằng nhau và 4 góc vuông (90°)."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Hình nào có 3 cạnh bằng nhau và 3 góc bằng nhau?",
                        Type = "single_choice",
                        Options = new List<string> { "Tam giác đều", "Tam giác vuông", "Hình vuông", "Hình thoi" },
                        CorrectAnswers = new List<string> { "Tam giác đều" },
                        Explanation = "Tam giác đều là tam giác có 3 cạnh bằng nhau và 3 góc bằng nhau (đều bằng 60°)."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Một mảnh đất hình vuông có cạnh 15 m. Chu vi mảnh đất là:",
                        Type = "single_choice",
                        Options = new List<string> { "60 m", "225 m", "30 m", "45 m" },
                        CorrectAnswers = new List<string> { "60 m" },
                        Explanation = "Chu vi hình vuông P = 4 × a = 4 × 15 = 60 m."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Lục giác đều cạnh 5 cm có chu vi là:",
                        Type = "single_choice",
                        Options = new List<string> { "30 cm", "25 cm", "15 cm", "36 cm" },
                        CorrectAnswers = new List<string> { "30 cm" },
                        Explanation = "Chu vi lục giác đều P = 6 × 5 = 30 cm."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Hai đường chéo của hình vuông có đặc điểm gì?",
                        Type = "single_choice",
                        Options = new List<string> { "Bằng nhau và vuông góc với nhau", "Không bằng nhau nhưng vuông góc", "Song song với nhau", "Chỉ cắt nhau không vuông góc" },
                        CorrectAnswers = new List<string> { "Bằng nhau và vuông góc với nhau" },
                        Explanation = "Hai đường chéo của hình vuông bằng nhau và vuông góc với nhau tại trung điểm của mỗi đường."
                    }
                }
            },

            // Bài 2: Hình chữ nhật. Hình thoi
            new Lesson
            {
                LessonCode = "LESSON_6_HCN_THOI",
                GradeLevel = "Lop6",
                TopicCode = "HINH_PHANG_THUC_TIEN_6",
                Title = "Bài 19: Hình chữ nhật. Hình thoi",
                Order = 2,
                Summary = "Nhận biết các yếu tố cạnh đối, góc vuông và đường chéo của hình chữ nhật và hình thoi.",
                Definition = "• Hình chữ nhật ABCD có: 4 đỉnh; 2 cặp cạnh đối diện song song và bằng nhau (AB = CD, BC = AD); 4 góc vuông; 2 đường chéo bằng nhau và cắt nhau tại trung điểm mỗi đường.\n• Hình thoi ABCD có: 4 cạnh bằng nhau (AB = BC = CD = DA); 2 cặp cạnh đối diện song song; các góc đối bằng nhau; 2 đường chéo vuông góc với nhau tại trung điểm mỗi đường.",
                Properties = new List<string>
                {
                    "Hình chữ nhật có 2 đường chéo bằng nhau: AC = BD.",
                    "Hình thoi có 2 đường chéo vuông góc với nhau: AC ⊥ BD.",
                    "Cả hình chữ nhật và hình thoi đều có tâm đối xứng là giao điểm hai đường chéo."
                },
                Formulas = new List<string>
                {
                    "Chu vi hình chữ nhật chiều dài a, chiều rộng b: P = 2 × (a + b)",
                    "Diện tích hình chữ nhật: S = a × b",
                    "Chu vi hình thoi cạnh a: P = 4 × a; Diện tích: S = (m × n) / 2 (m, n là độ dài 2 đường chéo)"
                },
                VisualExample = "Hình chữ nhật có chiều dài 8 cm, chiều rộng 5 cm có chu vi P = 2 × (8 + 5) = 26 cm và diện tích S = 8 × 5 = 40 cm².",
                RealWorldExample = "Mặt bàn học, chiếc tivi màn hình phẳng, khung cửa sổ (hình chữ nhật); họa tiết thổ cẩm Tây Nguyên, con diều giấy (hình thoi).",
                HasGeometryLab = true,
                DefaultLabShape = "HinhChuNhat",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Hình thoi có hai đường chéo dài 6 cm và 8 cm. Diện tích của hình thoi là bao nhiêu cm²?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "24" },
                        Hint1 = "Công thức diện tích hình thoi là S = (m × n) / 2.",
                        Hint2 = "Tính tích hai đường chéo rồi chia đôi: (6 × 8) ÷ 2.",
                        Explanation = "Diện tích hình thoi S = (6 × 8) / 2 = 48 / 2 = 24 cm²."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Đặc điểm nào sau đây phân biệt hình thoi với hình chữ nhật?",
                        Type = "single_choice",
                        Options = new List<string> { "Hình thoi có 4 cạnh bằng nhau và 2 đường chéo vuông góc", "Hình thoi có 4 góc vuông", "Hình thoi có 2 đường chéo bằng nhau", "Hình thoi không có tâm đối xứng" },
                        CorrectAnswers = new List<string> { "Hình thoi có 4 cạnh bằng nhau và 2 đường chéo vuông góc" },
                        Hint1 = "Hình thoi nổi bật với tính chất 4 cạnh có độ dài như nhau.",
                        Hint2 = "Hai đường chéo của hình thoi vuông góc với nhau, trong khi hình chữ nhật có 4 góc vuông.",
                        Explanation = "Hình thoi có 4 cạnh bằng nhau và hai đường chéo vuông góc với nhau."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Một tấm bưu thiếp hình chữ nhật có chu vi 28 cm, chiều dài 8 cm. Chiều rộng của tấm thiệp bằng bao nhiêu cm?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "6" },
                        Hint1 = "Nửa chu vi = Chu vi ÷ 2 = a + b.",
                        Hint2 = "Nửa chu vi = 28 ÷ 2 = 14 cm. Chiều rộng = 14 - 8.",
                        Explanation = "Nửa chu vi = 28 ÷ 2 = 14 cm. Chiều rộng = 14 - 8 = 6 cm."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Hình chữ nhật có 2 kích thước 4 cm và 9 cm. Diện tích là:",
                        Type = "single_choice",
                        Options = new List<string> { "36 cm²", "26 cm²", "18 cm²", "13 cm²" },
                        CorrectAnswers = new List<string> { "36 cm²" },
                        Explanation = "S = a × b = 4 × 9 = 36 cm²."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Hai đường chéo của hình thoi có tính chất gì?",
                        Type = "single_choice",
                        Options = new List<string> { "Vuông góc với nhau tại trung điểm mỗi đường", "Bằng nhau và song song", "Chỉ bằng nhau mà không vuông góc", "Song song với nhau" },
                        CorrectAnswers = new List<string> { "Vuông góc với nhau tại trung điểm mỗi đường" },
                        Explanation = "Hai đường chéo của hình thoi vuông góc với nhau tại trung điểm mỗi đường."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Chu vi hình thoi có độ dài cạnh 7 cm là:",
                        Type = "single_choice",
                        Options = new List<string> { "28 cm", "49 cm", "21 cm", "14 cm" },
                        CorrectAnswers = new List<string> { "28 cm" },
                        Explanation = "P = 4 × a = 4 × 7 = 28 cm."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Điểm giống nhau giữa hình chữ nhật và hình thoi là:",
                        Type = "single_choice",
                        Options = new List<string> { "Các cạnh đối diện song song và bằng nhau", "Bốn góc đều bằng 90°", "Bốn cạnh đều bằng nhau", "Hai đường chéo luôn bằng nhau" },
                        CorrectAnswers = new List<string> { "Các cạnh đối diện song song và bằng nhau" },
                        Explanation = "Cả hình chữ nhật và hình thoi đều là hình bình hành nên có các cạnh đối diện song song và bằng nhau."
                    }
                }
            },

            // Bài 3: Hình bình hành. Hình thang cân
            new Lesson
            {
                LessonCode = "LESSON_6_HBH_THANG_CAN",
                GradeLevel = "Lop6",
                TopicCode = "HINH_PHANG_THUC_TIEN_6",
                Title = "Bài 20: Hình bình hành. Hình thang cân",
                Order = 3,
                Summary = "Nhận biết các yếu tố đặc trưng của hình bình hành và hình thang cân qua cạnh đối, cạnh bên, góc và đường chéo.",
                Definition = "• Hình bình hành ABCD có: 2 cặp cạnh đối diện song song và bằng nhau (AB // CD, AB = CD; BC // AD, BC = AD); 2 góc đối bằng nhau; 2 đường chéo cắt nhau tại trung điểm mỗi đường.\n• Hình thang cân ABCD (đáy AB // CD) có: 2 cạnh đáy song song; 2 cạnh bên bằng nhau (AD = BC); 2 góc kề một đáy bằng nhau; 2 đường chéo bằng nhau (AC = BD).",
                Properties = new List<string>
                {
                    "Hình bình hành có tâm đối xứng là giao điểm hai đường chéo.",
                    "Hình thang cân có trục đối xứng là đường thẳng đi qua trung điểm của hai cạnh đáy.",
                    "Hình thang cân có 2 đường chéo bằng nhau: AC = BD."
                },
                Formulas = new List<string>
                {
                    "Chu vi hình bình hành: P = 2 × (a + b); Diện tích: S = a × h (a là độ dài cạnh đáy, h là chiều cao tương ứng)",
                    "Chu vi hình thang: P = a + b + c + d; Diện tích: S = ((a + b) × h) / 2 (a, b là 2 cạnh đáy, h là chiều cao)"
                },
                VisualExample = "Hình thang cân có 2 đáy là 4 cm và 10 cm, chiều cao 5 cm có diện tích S = ((4 + 10) × 5) / 2 = (14 × 5) / 2 = 35 cm².",
                RealWorldExample = "Cầu thang cuốn, mái nhà tranh truyền thống (hình thang cân); lan can cầu, cánh buồm thuyền nan đẩy gió (hình bình hành).",
                HasGeometryLab = true,
                DefaultLabShape = "HinhBinhHanh",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Hình thang cân có tính chất nào nổi bật về hai đường chéo?",
                        Type = "single_choice",
                        Options = new List<string> { "Hai đường chéo bằng nhau", "Hai đường chéo vuông góc với nhau", "Hai đường chéo song song", "Hai đường chéo không cắt nhau" },
                        CorrectAnswers = new List<string> { "Hai đường chéo bằng nhau" },
                        Hint1 = "Hãy nhớ lại hình thang cân có tính chất đối xứng qua trục nối trung điểm 2 đáy.",
                        Hint2 = "Hai đường chéo AC và BD có độ dài hoàn toàn bằng nhau.",
                        Explanation = "Hình thang cân có hai đường chéo bằng nhau: AC = BD."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Hình bình hành có cạnh đáy a = 12 cm và chiều cao tương ứng h = 5 cm. Diện tích của hình bình hành là bao nhiêu cm²?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "60" },
                        Hint1 = "Công thức tính diện tích hình bình hành là S = a × h.",
                        Hint2 = "Nhân 12 với 5.",
                        Explanation = "Diện tích hình bình hành S = 12 × 5 = 60 cm²."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Hình thang có độ dài hai đáy lần lượt là 6 cm và 10 cm, chiều cao 4 cm. Diện tích là bao nhiêu cm²?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "32" },
                        Hint1 = "Diện tích hình thang S = ((đáy lớn + đáy bé) × chiều cao) / 2.",
                        Hint2 = "Tính ((6 + 10) × 4) / 2 = (16 × 4) / 2.",
                        Explanation = "S = ((6 + 10) × 4) / 2 = 64 / 2 = 32 cm²."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Hình bình hành có đặc điểm nào sau đây?",
                        Type = "single_choice",
                        Options = new List<string> { "Các cạnh đối diện song song và bằng nhau", "Bốn cạnh bằng nhau", "Bốn góc bằng nhau", "Hai đường chéo vuông góc" },
                        CorrectAnswers = new List<string> { "Các cạnh đối diện song song và bằng nhau" },
                        Explanation = "Hình bình hành có các cặp cạnh đối diện song song và bằng nhau."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Hình thang cân là hình thang có:",
                        Type = "single_choice",
                        Options = new List<string> { "Hai góc kề một đáy bằng nhau", "Bốn cạnh bằng nhau", "Một góc vuông", "Hai đường chéo vuông góc" },
                        CorrectAnswers = new List<string> { "Hai góc kề một đáy bằng nhau" },
                        Explanation = "Hình thang cân là hình thang có hai góc kề một đáy bằng nhau và hai đường chéo bằng nhau."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Hình bình hành có hai cạnh kề là 6 cm và 8 cm. Chu vi của hình bình hành là:",
                        Type = "single_choice",
                        Options = new List<string> { "28 cm", "48 cm", "14 cm", "24 cm" },
                        CorrectAnswers = new List<string> { "28 cm" },
                        Explanation = "P = 2 × (6 + 8) = 2 × 14 = 28 cm."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Hình nào sau đây KHÔNG có tâm đối xứng?",
                        Type = "single_choice",
                        Options = new List<string> { "Hình thang cân (không phải HCN)", "Hình bình hành", "Hình thoi", "Hình chữ nhật" },
                        CorrectAnswers = new List<string> { "Hình thang cân (không phải HCN)" },
                        Explanation = "Hình thang cân chỉ có trục đối xứng chứ không có tâm đối xứng."
                    }
                }
            },

            // Bài 4: Chu vi và diện tích một số hình tứ giác đã học
            new Lesson
            {
                LessonCode = "LESSON_6_CHU_VI_DIEN_TICH",
                GradeLevel = "Lop6",
                TopicCode = "HINH_PHANG_THUC_TIEN_6",
                Title = "Bài 21: Chu vi và diện tích của một số hình tứ giác đã học",
                Order = 4,
                Summary = "Tổng hợp hệ thống công thức tính chu vi và diện tích của hình chữ nhật, hình vuông, hình thang cân, hình thoi, hình bình hành và ứng dụng bài toán thực tiễn.",
                Definition = "Chu vi là độ dài đường bao quanh hình phẳng. Diện tích là số đo phần mặt phẳng giới hạn bởi hình đó.\n• Hình chữ nhật: P = 2(a + b), S = a.b\n• Hình vuông: P = 4a, S = a²\n• Hình thang: P = a + b + c + d, S = (a + b)h / 2\n• Hình thoi: P = 4a, S = m.n / 2\n• Hình bình hành: P = 2(a + b), S = a.h",
                Properties = new List<string>
                {
                    "Đơn vị đo chu vi là đơn vị độ dài (m, cm, dm, mm).",
                    "Đơn vị đo diện tích là đơn vị diện tích (m², cm², dm², mm²).",
                    "Khi tính chu vi và diện tích, tất cả kích thước phải được đưa về cùng một đơn vị đo."
                },
                Formulas = new List<string>
                {
                    "1 m² = 100 dm² = 10,000 cm²",
                    "Chia nhỏ hình phức tạp thành các hình quen thuộc để tính tổng diện tích."
                },
                VisualExample = "Một mảnh vườn hình chữ nhật có chiều dài 20 m, chiều rộng 15 m. Người ta làm một lối đi hình bình hành có đáy 2 m cắt ngang qua vườn. Diện tích lối đi là S = 2 × 15 = 30 m².",
                RealWorldExample = "Tính diện tích nền nhà để mua gạch lát nền, tính chu vi khu đất để làm hàng rào rào quanh vườn rau.",
                HasGeometryLab = true,
                DefaultLabShape = "HinhVuong",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Bác Nam có mảnh vườn hình chữ nhật dài 25 m, rộng 10 m. Bác muốn rào lưới xung quanh vườn, chừa lối đi 2 m. Bác cần mua bao nhiêu mét lưới?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "68" },
                        Hint1 = "Tính chu vi mảnh vườn rồi trừ đi chiều rộng lối đi 2 m.",
                        Hint2 = "Chu vi = 2 × (25 + 10) = 70 m. Chiều dài lưới = 70 - 2.",
                        Explanation = "Chu vi khu vườn: 2 × (25 + 10) = 70 m. Số mét lưới cần mua: 70 - 2 = 68 m."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Một gian phòng hình vuông cạnh 6 m. Người ta dùng các viên gạch men hình vuông cạnh 30 cm để lát kín nền. Cần bao nhiêu viên gạch?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "400" },
                        Hint1 = "Đổi 6 m = 600 cm. Diện tích căn phòng = 600 × 600 = 360,000 cm².",
                        Hint2 = "Diện tích 1 viên gạch = 30 × 30 = 900 cm². Số viên gạch = 360,000 ÷ 900.",
                        Explanation = "Diện tích phòng = 600 × 600 = 360,000 cm². Diện tích gạch = 30 × 30 = 900 cm². Số viên gạch = 360,000 / 900 = 400 viên."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Để tính diện tích hình thoi có đường chéo m và n, ta dùng công thức nào?",
                        Type = "single_choice",
                        Options = new List<string> { "S = (m × n) / 2", "S = m × n", "S = 2 × (m + n)", "S = (m + n) / 2" },
                        CorrectAnswers = new List<string> { "S = (m × n) / 2" },
                        Hint1 = "Diện tích hình thoi bằng nửa tích độ dài hai đường chéo.",
                        Hint2 = "Công thức: S = (m × n) / 2.",
                        Explanation = "Công thức diện tích hình thoi là S = (m × n) / 2."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Hình vuông có chu vi 48 cm thì diện tích là:",
                        Type = "single_choice",
                        Options = new List<string> { "144 cm²", "196 cm²", "12 cm²", "96 cm²" },
                        CorrectAnswers = new List<string> { "144 cm²" },
                        Explanation = "Cạnh a = 48 / 4 = 12 cm. Diện tích S = 12 × 12 = 144 cm²."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Một mảnh đất hình thang có đáy lớn 15m, đáy bé 9m, chiều cao 8m. Diện tích là:",
                        Type = "single_choice",
                        Options = new List<string> { "96 m²", "192 m²", "120 m²", "84 m²" },
                        CorrectAnswers = new List<string> { "96 m²" },
                        Explanation = "S = ((15 + 9) × 8) / 2 = (24 × 8) / 2 = 96 m²."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Đổi 5 m² sang dm² ta được:",
                        Type = "single_choice",
                        Options = new List<string> { "500 dm²", "50 dm²", "5,000 dm²", "0.05 dm²" },
                        CorrectAnswers = new List<string> { "500 dm²" },
                        Explanation = "1 m² = 100 dm² nên 5 m² = 500 dm²."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Hình chữ nhật có diện tích 72 cm², chiều rộng 6 cm. Chu vi của nó là:",
                        Type = "single_choice",
                        Options = new List<string> { "36 cm", "18 cm", "24 cm", "48 cm" },
                        CorrectAnswers = new List<string> { "36 cm" },
                        Explanation = "Chiều dài = 72 / 6 = 12 cm. Chu vi P = 2 × (12 + 6) = 36 cm."
                    }
                }
            },

            // Bài 5: Hình có trục đối xứng
            new Lesson
            {
                LessonCode = "LESSON_6_TRUC_DOI_XUNG",
                GradeLevel = "Lop6",
                TopicCode = "TINH_DOI_XUNG_6",
                Title = "Bài 22: Hình có trục đối xứng",
                Order = 5,
                Summary = "Tìm hiểu khái niệm trục đối xứng, cách gấp giấy tìm trục đối xứng và nhận diện các hình có 1, 2, 4 hoặc vô số trục đối xứng.",
                Definition = "Đường thẳng d được gọi là trục đối xứng của hình H nếu khi gấp hình theo đường thẳng d, hai phần của hình trùng khít lên nhau.\n• Đoạn thẳng có 1 trục đối xứng (đường trung trực).\n• Tam giác cân có 1 trục đối xứng.\n• Tam giác đều có 3 trục đối xứng.\n• Hình chữ nhật có 2 trục đối xứng.\n• Hình thoi có 2 trục đối xứng (chính là 2 đường chéo).\n• Hình vuông có 4 trục đối xứng.\n• Hình tròn có vô số trục đối xứng (mỗi đường kính là một trục đối xứng).",
                Properties = new List<string>
                {
                    "Mỗi điểm thuộc hình ban đầu có một điểm đối xứng qua trục cũng thuộc hình đó.",
                    "Khoảng cách từ hai điểm đối xứng đến trục đối xứng luôn bằng nhau."
                },
                Formulas = new List<string>
                {
                    "Số trục đối xứng của đa giác đều n cạnh luôn bằng n (tam giác đều có 3, hình vuông có 4, lục giác đều có 6)."
                },
                VisualExample = "Gấp đôi tờ giấy theo trục thẳng đứng, cắt hình một nửa cánh bướm, khi mở ra ta nhận được một con bướm hoàn chỉnh có trục đối xứng là nếp gấp.",
                RealWorldExample = "Hình ảnh con bướm xòe cánh, lá cây bàng, chiếc lá phong, Tháp Eiffel ở Paris, chữ cái in hoa A, M, T, Y.",
                HasGeometryLab = true,
                DefaultLabShape = "HinhThangCan",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Hình tròn có bao nhiêu trục đối xứng?",
                        Type = "single_choice",
                        Options = new List<string> { "Vô số trục đối xứng", "1 trục đối xứng", "2 trục đối xứng", "4 trục đối xứng" },
                        CorrectAnswers = new List<string> { "Vô số trục đối xứng" },
                        Hint1 = "Mỗi đường kính của hình tròn đều chia hình tròn làm hai nửa trùng khít.",
                        Hint2 = "Có vô số đường kính đi qua tâm, do đó có vô số trục đối xứng.",
                        Explanation = "Bất kỳ đường thẳng nào đi qua tâm (đường kính) đều là trục đối xứng của hình tròn, do đó hình tròn có vô số trục đối xứng."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Hình chữ nhật (không phải hình vuông) có bao nhiêu trục đối xứng?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "2" },
                        Hint1 = "Hãy thử gấp hình chữ nhật theo nếp gấp ngang và nếp gấp dọc.",
                        Hint2 = "Hai đường nối trung điểm các cặp cạnh đối diện là 2 trục đối xứng. Đường chéo không phải là trục đối xứng của hình chữ nhật thường.",
                        Explanation = "Hình chữ nhật có đúng 2 trục đối xứng là hai đường thẳng nối trung điểm các cạnh đối."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Chữ cái in hoa nào sau đây có trục đối xứng thẳng đứng?",
                        Type = "single_choice",
                        Options = new List<string> { "Chữ A", "Chữ F", "Chữ P", "Chữ R" },
                        CorrectAnswers = new List<string> { "Chữ A" },
                        Hint1 = "Chia đôi chữ theo chiều dọc xem nửa bên trái có giống hệt nửa bên phải không.",
                        Hint2 = "Chữ A có trục đối xứng dọc chia đôi đỉnh nhọn xuống đáy.",
                        Explanation = "Chữ A có 1 trục đối xứng thẳng đứng đi qua đỉnh."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Hình vuông có bao nhiêu trục đối xứng?",
                        Type = "single_choice",
                        Options = new List<string> { "4", "2", "1", "Vô số" },
                        CorrectAnswers = new List<string> { "4" },
                        Explanation = "Hình vuông có 4 trục đối xứng: 2 đường chéo và 2 đường trung trực của các cạnh đối."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Tam giác đều có số trục đối xứng là:",
                        Type = "single_choice",
                        Options = new List<string> { "3", "1", "2", "6" },
                        CorrectAnswers = new List<string> { "3" },
                        Explanation = "Tam giác đều có 3 trục đối xứng đi qua mỗi đỉnh và trung điểm cạnh đối."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Hình thang cân có bao nhiêu trục đối xứng?",
                        Type = "single_choice",
                        Options = new List<string> { "1", "2", "0", "4" },
                        CorrectAnswers = new List<string> { "1" },
                        Explanation = "Hình thang cân có 1 trục đối xứng là đường thẳng nối trung điểm hai đáy."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Hình bình hành thông thường (không phải HCN, không phải thoi) có mấy trục đối xứng?",
                        Type = "single_choice",
                        Options = new List<string> { "0", "1", "2", "4" },
                        CorrectAnswers = new List<string> { "0" },
                        Explanation = "Hình bình hành thông thường không có trục đối xứng nào (chỉ có tâm đối xứng)."
                    }
                }
            },

            // Bài 6: Hình có tâm đối xứng
            new Lesson
            {
                LessonCode = "LESSON_6_TAM_DOI_XUNG",
                GradeLevel = "Lop6",
                TopicCode = "TINH_DOI_XUNG_6",
                Title = "Bài 23: Hình có tâm đối xứng",
                Order = 6,
                Summary = "Tìm hiểu khái niệm tâm đối xứng qua phép quay nửa vòng (180°), phát hiện tâm đối xứng trong hình bình hành, hình chữ nhật, hình thoi, hình vuông và hình tròn.",
                Definition = "Điểm O được gọi là tâm đối xứng của hình H nếu khi quay hình H quanh điểm O một góc 180° (nửa vòng quay), hình thu được trùng khít với chính hình ban đầu.\n• Đoạn thẳng có tâm đối xứng là trung điểm của đoạn thẳng.\n• Hình bình hành, hình chữ nhật, hình thoi, hình vuông có tâm đối xứng là giao điểm hai đường chéo.\n• Hình tròn có tâm đối xứng là tâm của đường tròn.\n• Tam giác đều KHÔNG có tâm đối xứng.",
                Properties = new List<string>
                {
                    "Mỗi điểm M trên hình lấy đối xứng qua tâm O sẽ tạo thành điểm M' cũng thuộc hình.",
                    "O luôn là trung điểm của đoạn thẳng nối hai điểm tương ứng đối xứng MM'."
                },
                Formulas = new List<string>
                {
                    "Đa giác đều có số cạnh chẵn (4, 6, 8, ...) luôn có tâm đối xứng.",
                    "Đa giác đều có số cạnh lẻ (3, 5, 7, ...) KHÔNG có tâm đối xứng."
                },
                VisualExample = "Ghim chiếc chong chóng 4 cánh hoặc thẻ bài quân cờ tại tâm O, xoay 180°, hình dạng thẻ bài vẫn như cũ.",
                RealWorldExample = "Cánh quạt trần 4 cánh, chong chóng gió, bông tuyết mùa đông, quân bài tây (J, Q, K), chữ cái S, N, Z, O.",
                HasGeometryLab = true,
                DefaultLabShape = "HinhBinhHanh",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Hình nào sau đây KHÔNG CÓ tâm đối xứng?",
                        Type = "single_choice",
                        Options = new List<string> { "Tam giác đều", "Hình vuông", "Hình tròn", "Hình bình hành" },
                        CorrectAnswers = new List<string> { "Tam giác đều" },
                        Hint1 = "Hãy nhớ quy tắc: đa giác đều có số cạnh lẻ thì không có tâm đối xứng.",
                        Hint2 = "Khi quay tam giác đều một góc 180°, đỉnh hướng lên trên sẽ bị quay lộn ngược xuống dưới nên không trùng khít.",
                        Explanation = "Tam giác đều có 3 cạnh (số cạnh lẻ) nên không có tâm đối xứng."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Tâm đối xứng của hình bình hành là điểm nào?",
                        Type = "single_choice",
                        Options = new List<string> { "Giao điểm của hai đường chéo", "Trung điểm một cạnh đáy", "Một trong bốn đỉnh", "Trọng tâm tam giác" },
                        CorrectAnswers = new List<string> { "Giao điểm của hai đường chéo" },
                        Hint1 = "Hai đường chéo hình bình hành cắt nhau tại trung điểm mỗi đường.",
                        Hint2 = "Chính điểm giao nhau này là tâm đối xứng của hình.",
                        Explanation = "Giao điểm hai đường chéo của hình bình hành là tâm đối xứng của hình đó."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Chữ cái in hoa nào sau đây có TÂM đối xứng?",
                        Type = "single_choice",
                        Options = new List<string> { "Chữ S", "Chữ A", "Chữ M", "Chữ V" },
                        CorrectAnswers = new List<string> { "Chữ S" },
                        Hint1 = "Tưởng tượng lộn ngược chữ cái đó 180° xem có đọc được như cũ không.",
                        Hint2 = "Chữ S khi xoay ngược 180° vẫn là chữ S.",
                        Explanation = "Chữ S có tâm đối xứng tại điểm chính giữa của chữ."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Hình tròn có tâm đối xứng là:",
                        Type = "single_choice",
                        Options = new List<string> { "Tâm của hình tròn", "Điểm bất kỳ trên đường tròn", "Điểm ngoài hình tròn", "Không có tâm đối xứng" },
                        CorrectAnswers = new List<string> { "Tâm của hình tròn" },
                        Explanation = "Tâm của đường tròn chính là tâm đối xứng của hình tròn."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Hình nào sau đây vừa có trục đối xứng vừa có tâm đối xứng?",
                        Type = "single_choice",
                        Options = new List<string> { "Hình chữ nhật", "Tam giác cân", "Hình thang cân thường", "Tam giác vuông" },
                        CorrectAnswers = new List<string> { "Hình chữ nhật" },
                        Explanation = "Hình chữ nhật có 2 trục đối xứng và 1 tâm đối xứng (giao điểm 2 đường chéo)."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Đa giác đều nào sau đây có tâm đối xứng?",
                        Type = "single_choice",
                        Options = new List<string> { "Lục giác đều (6 cạnh)", "Tam giác đều (3 cạnh)", "Ngũ giác đều (5 cạnh)", "Hình thang vuông" },
                        CorrectAnswers = new List<string> { "Lục giác đều (6 cạnh)" },
                        Explanation = "Lục giác đều có 6 cạnh (số cạnh chẵn) nên có tâm đối xứng."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Biển báo giao thông hình chữ nhật có tâm đối xứng tại:",
                        Type = "single_choice",
                        Options = new List<string> { "Giao điểm hai đường chéo", "Góc trên bên phải", "Cạnh dưới", "Không có tâm đối xứng" },
                        CorrectAnswers = new List<string> { "Giao điểm hai đường chéo" },
                        Explanation = "Hình chữ nhật có tâm đối xứng là giao điểm hai đường chéo."
                    }
                }
            }
        };
    }

    private static List<TestQuestion> GenerateTopicTestLop6_HinhPhang()
    {
        return new List<TestQuestion>
        {
            new TestQuestion { QuestionText = "Câu 1: Hình nào sau đây có 4 cạnh bằng nhau và 4 góc vuông?", Options = new List<string> { "Hình chữ nhật", "Hình vuông", "Hình thoi", "Hình thang cân" }, CorrectAnswers = new List<string> { "Hình vuông" }, Explanation = "Hình vuông là tứ giác có 4 cạnh bằng nhau và 4 góc vuông." },
            new TestQuestion { QuestionText = "Câu 2: Diện tích hình vuông có chu vi 36 cm là:", Options = new List<string> { "81 cm²", "36 cm²", "144 cm²", "72 cm²" }, CorrectAnswers = new List<string> { "81 cm²" }, Explanation = "Cạnh a = 36 / 4 = 9 cm. S = 9 × 9 = 81 cm²." },
            new TestQuestion { QuestionText = "Câu 3: Hình chữ nhật có chiều dài 12 cm, diện tích 60 cm². Chu vi hình chữ nhật là:", Options = new List<string> { "34 cm", "17 cm", "24 cm", "40 cm" }, CorrectAnswers = new List<string> { "34 cm" }, Explanation = "Chiều rộng = 60 / 12 = 5 cm. P = 2 × (12 + 5) = 34 cm." },
            new TestQuestion { QuestionText = "Câu 4: Diện tích hình thoi có hai đường chéo lần lượt là 10 cm và 14 cm bằng:", Options = new List<string> { "70 cm²", "140 cm²", "48 cm²", "24 cm²" }, CorrectAnswers = new List<string> { "70 cm²" }, Explanation = "S = (10 × 14) / 2 = 70 cm²." },
            new TestQuestion { QuestionText = "Câu 5: Tam giác đều cạnh 8 cm có chu vi là:", Options = new List<string> { "24 cm", "16 cm", "32 cm", "64 cm" }, CorrectAnswers = new List<string> { "24 cm" }, Explanation = "P = 3 × 8 = 24 cm." },
            new TestQuestion { QuestionText = "Câu 6: Hình thang cân có tính chất nào về hai đường chéo?", Options = new List<string> { "Bằng nhau", "Vuông góc với nhau", "Song song với nhau", "Không bao giờ cắt nhau" }, CorrectAnswers = new List<string> { "Bằng nhau" }, Explanation = "Hai đường chéo của hình thang cân bằng nhau." },
            new TestQuestion { QuestionText = "Câu 7: Diện tích hình bình hành có đáy 15 cm và chiều cao 6 cm là:", Options = new List<string> { "90 cm²", "45 cm²", "42 cm²", "180 cm²" }, CorrectAnswers = new List<string> { "90 cm²" }, Explanation = "S = a × h = 15 × 6 = 90 cm²." },
            new TestQuestion { QuestionText = "Câu 8: Chu vi lục giác đều có cạnh 7 cm là:", Options = new List<string> { "42 cm", "49 cm", "21 cm", "35 cm" }, CorrectAnswers = new List<string> { "42 cm" }, Explanation = "P = 6 × 7 = 42 cm." },
            new TestQuestion { QuestionText = "Câu 9: Diện tích hình thang có đáy 8 cm và 12 cm, chiều cao 5 cm là:", Options = new List<string> { "50 cm²", "100 cm²", "40 cm²", "60 cm²" }, CorrectAnswers = new List<string> { "50 cm²" }, Explanation = "S = ((8 + 12) × 5) / 2 = 50 cm²." },
            new TestQuestion { QuestionText = "Câu 10: Hai đường chéo của hình thoi có đặc điểm gì?", Options = new List<string> { "Vuông góc với nhau tại trung điểm", "Luôn bằng nhau", "Song song với nhau", "Không cắt nhau" }, CorrectAnswers = new List<string> { "Vuông góc với nhau tại trung điểm" }, Explanation = "Hai đường chéo hình thoi vuông góc với nhau tại trung điểm mỗi đường." }
        };
    }

    private static List<TestQuestion> GenerateTopicTestLop6_DoiXung()
    {
        return new List<TestQuestion>
        {
            new TestQuestion { QuestionText = "Câu 1: Hình nào sau đây có vô số trục đối xứng?", Options = new List<string> { "Hình tròn", "Hình vuông", "Hình thoi", "Tam giác đều" }, CorrectAnswers = new List<string> { "Hình tròn" }, Explanation = "Mọi đường kính đều là trục đối xứng của hình tròn." },
            new TestQuestion { QuestionText = "Câu 2: Hình vuông có bao nhiêu trục đối xứng?", Options = new List<string> { "4", "2", "1", "8" }, CorrectAnswers = new List<string> { "4" }, Explanation = "Hình vuông có 4 trục đối xứng." },
            new TestQuestion { QuestionText = "Câu 3: Hình nào sau đây KHÔNG có tâm đối xứng?", Options = new List<string> { "Tam giác đều", "Hình vuông", "Hình bình hành", "Hình chữ nhật" }, CorrectAnswers = new List<string> { "Tam giác đều" }, Explanation = "Tam giác đều không có tâm đối xứng." },
            new TestQuestion { QuestionText = "Câu 4: Tâm đối xứng của hình bình hành là:", Options = new List<string> { "Giao điểm hai đường chéo", "Trung điểm một cạnh", "Một đỉnh của hình", "Điểm bất kỳ" }, CorrectAnswers = new List<string> { "Giao điểm hai đường chéo" }, Explanation = "Giao điểm hai đường chéo hình bình hành là tâm đối xứng." },
            new TestQuestion { QuestionText = "Câu 5: Hình thang cân có bao nhiêu trục đối xứng?", Options = new List<string> { "1", "2", "0", "4" }, CorrectAnswers = new List<string> { "1" }, Explanation = "Hình thang cân có 1 trục đối xứng là đường thẳng nối trung điểm 2 đáy." },
            new TestQuestion { QuestionText = "Câu 6: Chữ cái nào sau đây có trục đối xứng nằm ngang?", Options = new List<string> { "Chữ B", "Chữ A", "Chữ M", "Chữ Y" }, CorrectAnswers = new List<string> { "Chữ B" }, Explanation = "Chữ B in hoa có trục đối xứng nằm ngang." },
            new TestQuestion { QuestionText = "Câu 7: Chữ cái nào sau đây có tâm đối xứng?", Options = new List<string> { "Chữ N", "Chữ A", "Chữ C", "Chữ T" }, CorrectAnswers = new List<string> { "Chữ N" }, Explanation = "Chữ N xoay 180° vẫn là chính nó nên có tâm đối xứng." },
            new TestQuestion { QuestionText = "Câu 8: Hình chữ nhật có mấy trục đối xứng?", Options = new List<string> { "2", "4", "1", "0" }, CorrectAnswers = new List<string> { "2" }, Explanation = "Hình chữ nhật có 2 trục đối xứng nối trung điểm các cạnh đối." },
            new TestQuestion { QuestionText = "Câu 9: Tam giác cân (không đều) có bao nhiêu trục đối xứng?", Options = new List<string> { "1", "3", "2", "0" }, CorrectAnswers = new List<string> { "1" }, Explanation = "Tam giác cân có 1 trục đối xứng đi qua đỉnh và trung điểm đáy." },
            new TestQuestion { QuestionText = "Câu 10: Hình nào sau đây vừa có trục đối xứng vừa có tâm đối xứng?", Options = new List<string> { "Hình thoi", "Tam giác cân", "Hình thang cân", "Hình thang vuông" }, CorrectAnswers = new List<string> { "Hình thoi" }, Explanation = "Hình thoi có 2 trục đối xứng (2 đường chéo) và 1 tâm đối xứng (giao điểm 2 đường chéo)." }
        };
    }
}
