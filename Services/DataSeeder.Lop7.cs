using YouLearnGeometry.Models;

namespace YouLearnGeometry.Services;

public partial class DataSeeder
{
    private static CurriculumLevel GetCurriculumLevelLop7()
    {
        return new CurriculumLevel
        {
            GradeLevel = "Lop7",
            Name = "Hình học phẳng Lớp 7 - Kết nối tri thức",
            Description = "Nghiên cứu nền tảng hình học chứng minh: Góc ở vị trí đặc biệt, tiên đề Euclid, hai đường thẳng song song, tổng góc trong tam giác, các trường hợp bằng nhau của tam giác và tam giác cân.",
            Topics = new List<Topic>
            {
                new Topic
                {
                    TopicCode = "GOC_DUONG_THANG_7",
                    Title = "Chủ đề 1: Góc và hai đường thẳng song song",
                    Description = "Khám phá các góc kề bù, đối đỉnh, so le trong, đồng vị; tia phân giác và dấu hiệu hai đường thẳng song song.",
                    Order = 1,
                    TopicTest = GenerateTopicTestLop7_Goc()
                },
                new Topic
                {
                    TopicCode = "TAM_GIAC_BANG_NHAU_7",
                    Title = "Chủ đề 2: Tam giác bằng nhau và Tam giác cân",
                    Description = "Nắm vững định lý tổng các góc trong tam giác bằng 180°, các trường hợp bằng nhau (c-c-c, c-g-c, g-c-g) và tính chất tam giác cân.",
                    Order = 2,
                    TopicTest = GenerateTopicTestLop7_TamGiac()
                }
            }
        };
    }

    private static List<Lesson> GetLessonsLop7()
    {
        return new List<Lesson>
        {
            // Bài 1: Góc ở vị trí đặc biệt. Tia phân giác
            new Lesson
            {
                LessonCode = "LESSON_7_GOC_VI_TRI_DAC_BIET",
                GradeLevel = "Lop7",
                TopicCode = "GOC_DUONG_THANG_7",
                Title = "Góc ở vị trí đặc biệt. Tia phân giác của một góc",
                Order = 1,
                Summary = "Tìm hiểu hai góc kề bù (tổng bằng 180°), hai góc đối đỉnh (bằng nhau) và tính chất của tia phân giác chia góc thành hai góc bằng nhau.",
                Definition = "• Hai góc kề bù là hai góc vừa kề nhau vừa bù nhau, có tổng số đo bằng 180°.\n• Hai góc đối đỉnh là hai góc mà mỗi cạnh của góc này là tia đối của một cạnh của góc kia. Hai góc đối đỉnh thì bằng nhau: ∠O1 = ∠O2.\n• Tia phân giác của một góc là tia nằm giữa hai cạnh của góc và tạo với hai cạnh ấy hai góc bằng nhau: ∠xOz = ∠zOy = ∠xOy / 2.",
                Properties = new List<string>
                {
                    "Hai góc đối đỉnh luôn có số đo bằng nhau.",
                    "Hai góc kề bù có tổng số đo bằng 180°.",
                    "Đường phân giác chia góc thành 2 phần cân đối bằng nhau."
                },
                Formulas = new List<string>
                {
                    "Nếu tia Oz là phân giác của ∠xOy thì: ∠xOz = ∠zOy = ½ ∠xOy",
                    "Hai góc bù nhau: ∠A + ∠B = 180°"
                },
                VisualExample = "Hai đường thẳng cắt nhau tạo thành 4 góc. Nếu biết ∠O1 = 60° thì góc đối đỉnh ∠O3 = 60°, góc kề bù ∠O2 = 180° - 60° = 120°.",
                RealWorldExample = "Kéo cắt giấy mở ra hai lưỡi kéo tạo thành hai góc đối đỉnh bằng nhau; kim đồng hồ lúc 6 giờ tạo thành góc bẹt 180°.",
                HasGeometryLab = true,
                DefaultLabShape = "TamGiacThuong",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Cho góc xOy có số đo bằng 80°. Tia Oz là tia phân giác của góc xOy. Số đo của góc xOz bằng bao nhiêu độ?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "40" },
                        Hint1 = "Tia phân giác chia đôi góc ban đầu.",
                        Hint2 = "Số đo ∠xOz = 80° ÷ 2.",
                        Explanation = "Vì Oz là tia phân giác của ∠xOy nên ∠xOz = 80° / 2 = 40°."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Hai đường thẳng xx' và yy' cắt nhau tại O. Biết ∠xOy = 110°. Góc đối đỉnh với góc xOy có số đo bằng:",
                        Type = "single_choice",
                        Options = new List<string> { "110°", "70°", "90°", "180°" },
                        CorrectAnswers = new List<string> { "110°" },
                        Hint1 = "Định lý: Hai góc đối đỉnh thì bằng nhau.",
                        Hint2 = "Góc đối đỉnh với ∠xOy cũng có số đo bằng chính ∠xOy.",
                        Explanation = "Theo tính chất, hai góc đối đỉnh thì bằng nhau nên góc đối đỉnh có số đo 110°."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Hai góc kề bù có tổng số đo bằng bao nhiêu độ?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "180" },
                        Hint1 = "Hai góc kề bù tạo thành một góc bẹt.",
                        Hint2 = "Góc bẹt có số đo 180°.",
                        Explanation = "Tổng số đo của hai góc kề bù luôn bằng 180°."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Cho góc ∠AOB = 120°. Góc kề bù với ∠AOB có số đo là:",
                        Type = "single_choice",
                        Options = new List<string> { "60°", "120°", "90°", "30°" },
                        CorrectAnswers = new List<string> { "60°" },
                        Explanation = "Số đo góc kề bù = 180° - 120° = 60°."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Khẳng định nào sau đây là ĐÚNG?",
                        Type = "single_choice",
                        Options = new List<string> { "Hai góc đối đỉnh thì bằng nhau", "Hai góc bằng nhau thì luôn đối đỉnh", "Hai góc kề bù có tổng bằng 90°", "Hai góc nhọn luôn bù nhau" },
                        CorrectAnswers = new List<string> { "Hai góc đối đỉnh thì bằng nhau" },
                        Explanation = "Hai góc đối đỉnh thì chắc chắn bằng nhau (chiều ngược lại không phải lúc nào cũng đúng)."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Tia Ot là phân giác của góc xOy = 70°. Số đo góc xOt là:",
                        Type = "single_choice",
                        Options = new List<string> { "35°", "70°", "140°", "40°" },
                        CorrectAnswers = new List<string> { "35°" },
                        Explanation = "∠xOt = 70° / 2 = 35°."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Hai đường thẳng cắt nhau tạo thành mấy cặp góc đối đỉnh (khác góc bẹt)?",
                        Type = "single_choice",
                        Options = new List<string> { "2 cặp", "4 cặp", "1 cặp", "3 cặp" },
                        CorrectAnswers = new List<string> { "2 cặp" },
                        Explanation = "Hai đường thẳng cắt nhau tạo thành 2 cặp góc đối đỉnh."
                    }
                }
            },

            // Bài 2: Hai đường thẳng song song và dấu hiệu nhận biết
            new Lesson
            {
                LessonCode = "LESSON_7_HAI_DT_SONG_SONG",
                GradeLevel = "Lop7",
                TopicCode = "GOC_DUONG_THANG_7",
                Title = "Hai đường thẳng song song và dấu hiệu nhận biết",
                Order = 2,
                Summary = "Tìm hiểu dấu hiệu nhận biết hai đường thẳng song song qua cặp góc so le trong, đồng vị và tiên đề Euclid về đường thẳng song song.",
                Definition = "• Hai đường thẳng song song là hai đường thẳng không có điểm chung (kí hiệu a // b).\n• Dấu hiệu nhận biết: Nếu đường thẳng c cắt hai đường thẳng a và b, trong các góc tạo thành có:\n  1. Một cặp góc so le trong bằng nhau; HOẶC\n  2. Một cặp góc đồng vị bằng nhau; HOẶC\n  3. Một cặp góc trong cùng phía bù nhau (tổng bằng 180°)\n  => Thì a và b song song với nhau.\n• Tiên đề Euclid: Qua một điểm ở ngoài một đường thẳng, chỉ có một đường thẳng song song với đường thẳng đó.",
                Properties = new List<string>
                {
                    "Nếu hai đường thẳng phân biệt cùng song song với đường thẳng thứ ba thì chúng song song với nhau (a // c và b // c => a // b).",
                    "Nếu một đường thẳng vuông góc với một trong hai đường thẳng song song thì nó cũng vuông góc với đường thẳng kia."
                },
                Formulas = new List<string>
                {
                    "a // b và c cắt a, b => Cặp góc so le trong bằng nhau, cặp góc đồng vị bằng nhau."
                },
                VisualExample = "Đường ray xe lửa gồm hai thanh sắt thẳng song song, các thanh tà vẹt gỗ vuông góc với cả hai thanh sắt.",
                RealWorldExample = "Các dòng kẻ trong vở ô li học sinh, hai mép đối diện của thước kẻ thẳng, lan can cầu thang.",
                HasGeometryLab = true,
                DefaultLabShape = "HinhBinhHanh",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Đường thẳng c cắt hai đường thẳng a và b. Nếu có một cặp góc so le trong có số đo cùng bằng 55° thì quan hệ giữa a và b là gì?",
                        Type = "single_choice",
                        Options = new List<string> { "a song song với b (a // b)", "a vuông góc với b", "a cắt b tại 1 điểm", "a trùng với b" },
                        CorrectAnswers = new List<string> { "a song song với b (a // b)" },
                        Hint1 = "Theo dấu hiệu nhận biết hai đường thẳng song song khi có cặp góc so le trong bằng nhau.",
                        Hint2 = "Hai góc so le trong bằng nhau thì hai đường thẳng a và b song song.",
                        Explanation = "Theo dấu hiệu nhận biết: nếu đường thẳng thứ ba cắt hai đường thẳng tạo ra một cặp góc so le trong bằng nhau thì hai đường thẳng đó song song."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Cho a // b, đường thẳng c cắt a và b. Biết một góc đồng vị có số đo 75°. Số đo của góc đồng vị còn lại bằng bao nhiêu độ?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "75" },
                        Hint1 = "Tính chất hai đường thẳng song song: các cặp góc đồng vị bằng nhau.",
                        Hint2 = "Số đo bằng đúng 75°.",
                        Explanation = "Khi a // b thì hai góc đồng vị bằng nhau, do đó góc còn lại bằng 75°."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Tiên đề Euclid khẳng định: Qua một điểm ở ngoài một đường thẳng, vẽ được bao nhiêu đường thẳng song song với đường thẳng đó?",
                        Type = "single_choice",
                        Options = new List<string> { "Chỉ duy nhất 1 đường thẳng", "Có 2 đường thẳng", "Vô số đường thẳng", "Không vẽ được đường thẳng nào" },
                        CorrectAnswers = new List<string> { "Chỉ duy nhất 1 đường thẳng" },
                        Hint1 = "Đây là tiên đề nền tảng trong hình học phẳng.",
                        Hint2 = "Chỉ có duy nhất một đường thẳng song song.",
                        Explanation = "Theo Tiên đề Euclid: Qua một điểm ở ngoài một đường thẳng, chỉ có duy nhất một đường thẳng song song với đường thẳng đã cho."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Nếu đường thẳng c cắt hai đường thẳng a và b sao cho có một cặp góc trong cùng phía bù nhau thì:",
                        Type = "single_choice",
                        Options = new List<string> { "a // b", "a ⊥ b", "a cắt b", "a trùng b" },
                        CorrectAnswers = new List<string> { "a // b" },
                        Explanation = "Cặp góc trong cùng phía bù nhau là dấu hiệu nhận biết hai đường thẳng song song."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Nếu a // b và b // c thì quan hệ giữa a và c là:",
                        Type = "single_choice",
                        Options = new List<string> { "a // c", "a ⊥ c", "a cắt c", "Không xác định" },
                        CorrectAnswers = new List<string> { "a // c" },
                        Explanation = "Tính chất bắc cầu: hai đường thẳng cùng song song với đường thẳng thứ ba thì song song với nhau."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Cho a // b. Đường thẳng d vuông góc với a (d ⊥ a). Khi đó d có quan hệ gì với b?",
                        Type = "single_choice",
                        Options = new List<string> { "d ⊥ b", "d // b", "d trùng b", "d không cắt b" },
                        CorrectAnswers = new List<string> { "d ⊥ b" },
                        Explanation = "Đường thẳng vuông góc với một trong hai đường thẳng song song thì cũng vuông góc với đường thẳng kia."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Hai góc so le trong tạo bởi đường thẳng c cắt hai đường thẳng song song a và b thì:",
                        Type = "single_choice",
                        Options = new List<string> { "Bằng nhau", "Bù nhau", "Phụ nhau", "Có tổng bằng 360°" },
                        CorrectAnswers = new List<string> { "Bằng nhau" },
                        Explanation = "Tính chất hai đường thẳng song song: hai góc so le trong luôn bằng nhau."
                    }
                }
            },

            // Bài 3: Tổng các góc trong một tam giác
            new Lesson
            {
                LessonCode = "LESSON_7_TONG_GOC_TAM_GIAC",
                GradeLevel = "Lop7",
                TopicCode = "TAM_GIAC_BANG_NHAU_7",
                Title = "Tổng các góc trong một tam giác",
                Order = 3,
                Summary = "Nắm vững định lý tổng ba góc của một tam giác bằng 180°, tính chất góc nhọn trong tam giác vuông và định lý góc ngoài của tam giác.",
                Definition = "• Định lý: Tổng ba góc của một tam giác bằng 180°: ∠A + ∠B + ∠C = 180°.\n• Trong tam giác vuông, hai góc nhọn phụ nhau (tổng bằng 90°): ∠B + ∠C = 90°.\n• Góc ngoài của tam giác là góc kề bù với một góc trong của tam giác. Mỗi góc ngoài bằng tổng của hai góc trong không kề với nó.",
                Properties = new List<string>
                {
                    "Tam giác không thể có quá 1 góc tù hoặc quá 1 góc vuông.",
                    "Số đo góc ngoài luôn lớn hơn số đo của mỗi góc trong không kề với nó."
                },
                Formulas = new List<string>
                {
                    "∠A + ∠B + ∠C = 180°",
                    "Góc ngoài tại đỉnh C: ∠ACx = ∠A + ∠B"
                },
                VisualExample = "Tam giác ABC có ∠A = 70°, ∠B = 60°. Khi đó ∠C = 180° - (70° + 60°) = 50°. Góc ngoài tại C có số đo = 70° + 60° = 130°.",
                RealWorldExample = "Kê góc mái ngói tam giác dốc để thoát nước mưa, khung tam giác giàn cẩu tháp xây dựng chịu lực tốt nhất.",
                HasGeometryLab = true,
                DefaultLabShape = "TamGiacThuong",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Tam giác MNP có ∠M = 50°, ∠N = 70°. Số đo góc P bằng bao nhiêu độ?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "60" },
                        Hint1 = "Tổng ba góc của một tam giác luôn bằng 180°.",
                        Hint2 = "Số đo ∠P = 180° - (50° + 70°).",
                        Explanation = "∠P = 180° - 50° - 70° = 60°."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Trong tam giác vuông ABC vuông tại A, biết ∠B = 35°. Số đo của góc nhọn C là:",
                        Type = "single_choice",
                        Options = new List<string> { "55°", "65°", "45°", "145°" },
                        CorrectAnswers = new List<string> { "55°" },
                        Hint1 = "Trong tam giác vuông, hai góc nhọn có tổng bằng 90°.",
                        Hint2 = "Số đo ∠C = 90° - 35°.",
                        Explanation = "Trong tam giác vuông, hai góc nhọn phụ nhau: ∠C = 90° - 35° = 55°."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Góc ngoài tại đỉnh A của tam giác ABC bằng tổng của hai góc nào sau đây?",
                        Type = "single_choice",
                        Options = new List<string> { "∠B + ∠C", "∠A + ∠B", "∠A + ∠C", "180° - ∠B" },
                        CorrectAnswers = new List<string> { "∠B + ∠C" },
                        Hint1 = "Định lý: Góc ngoài bằng tổng của hai góc trong không kề với nó.",
                        Hint2 = "Góc ngoài tại A không kề với ∠B và ∠C.",
                        Explanation = "Góc ngoài của tam giác bằng tổng hai góc trong không kề với nó: Góc ngoài tại A = ∠B + ∠C."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Tam giác ABC có ∠A = 90°, ∠B = ∠C. Số đo góc B là:",
                        Type = "single_choice",
                        Options = new List<string> { "45°", "60°", "30°", "90°" },
                        CorrectAnswers = new List<string> { "45°" },
                        Explanation = "Tam giác vuông cân có hai góc nhọn bằng nhau: ∠B = 90° / 2 = 45°."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Tổng 3 góc của bất kỳ tam giác nào đều bằng:",
                        Type = "single_choice",
                        Options = new List<string> { "180°", "360°", "90°", "270°" },
                        CorrectAnswers = new List<string> { "180°" },
                        Explanation = "Định lý: Tổng ba góc của một tam giác luôn bằng 180°."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Tam giác có 3 góc tỉ lệ với 1:2:3. Số đo góc lớn nhất là:",
                        Type = "single_choice",
                        Options = new List<string> { "90°", "60°", "120°", "100°" },
                        CorrectAnswers = new List<string> { "90°" },
                        Explanation = "Tổng số phần: 1 + 2 + 3 = 6. Giá trị 1 phần: 180° / 6 = 30°. Góc lớn nhất: 3 × 30° = 90°."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Một góc ngoài của tam giác bằng 115°, một góc trong không kề bằng 45°. Góc trong còn lại không kề bằng:",
                        Type = "single_choice",
                        Options = new List<string> { "70°", "65°", "160°", "55°" },
                        CorrectAnswers = new List<string> { "70°" },
                        Explanation = "Góc trong còn lại = 115° - 45° = 70°."
                    }
                }
            },

            // Bài 4: Các trường hợp bằng nhau của hai tam giác
            new Lesson
            {
                LessonCode = "LESSON_7_TAM_GIAC_BANG_NHAU",
                GradeLevel = "Lop7",
                TopicCode = "TAM_GIAC_BANG_NHAU_7",
                Title = "Các trường hợp bằng nhau của hai tam giác",
                Order = 4,
                Summary = "Học thuyết chứng minh hai tam giác bằng nhau qua ba trường hợp cơ bản: cạnh-cạnh-cạnh (c-c-c), cạnh-góc-cạnh (c-g-c), góc-cạnh-góc (g-c-g) và các trường hợp của tam giác vuông.",
                Definition = "Hai tam giác bằng nhau là hai tam giác có các cạnh tương ứng bằng nhau và các góc tương ứng bằng nhau (ΔABC = ΔA'B'C').\n• Trường hợp 1 (c-c-c): Nếu ba cạnh của tam giác này bằng ba cạnh của tam giác kia thì hai tam giác bằng nhau.\n• Trường hợp 2 (c-g-c): Nếu hai cạnh và góc xen giữa của tam giác này bằng hai cạnh và góc xen giữa của tam giác kia thì hai tam giác bằng nhau.\n• Trường hợp 3 (g-c-g): Nếu một cạnh và hai góc kề của tam giác này bằng một cạnh và hai góc kề của tam giác kia thì hai tam giác bằng nhau.",
                Properties = new List<string>
                {
                    "Hệ quả tam giác vuông: Cạnh huyền - cạnh góc vuông; Cạnh huyền - góc nhọn; Hai cạnh góc vuông.",
                    "Khi hai tam giác bằng nhau, suy ra các cạnh và góc tương ứng bằng nhau để giải quyết bài toán chứng minh song song hoặc vuông góc."
                },
                Formulas = new List<string>
                {
                    "Kí hiệu: ΔABC = ΔA'B'C' (phải viết các đỉnh tương ứng theo cùng thứ tự)"
                },
                VisualExample = "Vẽ đường chéo chia hình chữ nhật ABCD thành hai tam giác vuông ΔABC và ΔCDA bằng nhau theo trường hợp cạnh - góc - cạnh hoặc c-c-c.",
                RealWorldExample = "Thiết kế cầu giàn sắt Truss Bridge: các thanh sắt ghép thành các cặp tam giác bằng nhau giúp phân bổ lực đều khắp mặt cầu.",
                HasGeometryLab = true,
                DefaultLabShape = "TamGiacThuong",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Cho ΔABC và ΔMNP có AB = MN, BC = NP, AC = MP. Hai tam giác này bằng nhau theo trường hợp nào?",
                        Type = "single_choice",
                        Options = new List<string> { "cạnh - cạnh - cạnh (c-c-c)", "cạnh - góc - cạnh (c-g-c)", "góc - cạnh - góc (g-c-g)", "cạnh huyền - góc nhọn" },
                        CorrectAnswers = new List<string> { "cạnh - cạnh - cạnh (c-c-c)" },
                        Hint1 = "Ba cặp cạnh tương ứng đều bằng nhau.",
                        Hint2 = "Trường hợp này viết tắt là c-c-c.",
                        Explanation = "Ba cặp cạnh tương ứng bằng nhau nên ΔABC = ΔMNP (c-c-c)."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Để chứng minh hai tam giác bằng nhau theo trường hợp cạnh - góc - cạnh (c-g-c), góc bằng nhau phải là góc nào?",
                        Type = "single_choice",
                        Options = new List<string> { "Góc xen giữa hai cạnh bằng nhau", "Góc đối diện cạnh lớn nhất", "Góc nhọn bất kỳ", "Góc vuông" },
                        CorrectAnswers = new List<string> { "Góc xen giữa hai cạnh bằng nhau" },
                        Hint1 = "Góc phải nằm giữa hai cạnh đã biết bằng nhau.",
                        Hint2 = "Từ 'xen giữa' là điều kiện bắt buộc trong trường hợp c-g-c.",
                        Explanation = "Trong trường hợp c-g-c, góc bằng nhau bắt buộc phải là góc xen giữa hai cạnh tương ứng bằng nhau."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Cho ΔABC = ΔDEF. Biết góc A = 60°, góc E = 70°. Số đo của góc C bằng bao nhiêu độ?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "50" },
                        Hint1 = "Vì hai tam giác bằng nhau nên góc B = góc E = 70°.",
                        Hint2 = "Tổng ba góc ΔABC bằng 180°: ∠C = 180° - 60° - 70°.",
                        Explanation = "Vì ΔABC = ΔDEF nên ∠B = ∠E = 70°. Suy ra ∠C = 180° - (60° + 70°) = 50°."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Cho ΔABC = ΔXYZ. Cạnh tương ứng với cạnh AC là:",
                        Type = "single_choice",
                        Options = new List<string> { "XZ", "XY", "YZ", "ZY" },
                        CorrectAnswers = new List<string> { "XZ" },
                        Explanation = "Đỉnh tương ứng: A ứng với X, C ứng với Z, nên cạnh AC tương ứng với XZ."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Hai tam giác vuông bằng nhau khi có:",
                        Type = "single_choice",
                        Options = new List<string> { "Cạnh huyền và một góc nhọn bằng nhau", "Chỉ một góc nhọn bằng nhau", "Diện tích bằng nhau", "Chu vi bằng nhau" },
                        CorrectAnswers = new List<string> { "Cạnh huyền và một góc nhọn bằng nhau" },
                        Explanation = "Trường hợp bằng nhau đặc biệt của tam giác vuông: Cạnh huyền - góc nhọn."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Muốn chứng minh ΔABC = ΔMNP theo trường hợp (g-c-g), cạnh bằng nhau phải là:",
                        Type = "single_choice",
                        Options = new List<string> { "Cạnh kề hai góc bằng nhau", "Cạnh đối diện góc lớn nhất", "Cạnh bất kỳ", "Đường cao" },
                        CorrectAnswers = new List<string> { "Cạnh kề hai góc bằng nhau" },
                        Explanation = "Cạnh phải là cạnh nối giữa hai đỉnh của hai góc tương ứng bằng nhau."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Cho ΔABC = ΔDEF, biết chu vi ΔABC = 24 cm. Chu vi ΔDEF là:",
                        Type = "single_choice",
                        Options = new List<string> { "24 cm", "12 cm", "48 cm", "Không xác định" },
                        CorrectAnswers = new List<string> { "24 cm" },
                        Explanation = "Hai tam giác bằng nhau thì ba cạnh tương ứng bằng nhau, do đó chu vi bằng nhau."
                    }
                }
            },

            // Bài 5: Tam giác cân. Đường trung trực
            new Lesson
            {
                LessonCode = "LESSON_7_TAM_GIAC_CAN",
                GradeLevel = "Lop7",
                TopicCode = "TAM_GIAC_BANG_NHAU_7",
                Title = "Tam giác cân. Đường trung trực của đoạn thẳng",
                Order = 5,
                Summary = "Định nghĩa, tính chất và dấu hiệu nhận biết tam giác cân, tam giác đều; tính chất đường trung trực của đoạn thẳng.",
                Definition = "• Tam giác cân là tam giác có hai cạnh bằng nhau: AB = AC (tam giác ABC cân tại A).\n  + Tính chất: Hai góc ở đáy bằng nhau: ∠B = ∠C.\n  + Dấu hiệu nhận biết: Tam giác có hai cạnh bằng nhau HOẶC có hai góc bằng nhau là tam giác cân.\n• Tam giác đều: Tam giác có 3 cạnh bằng nhau, 3 góc bằng 60°. Tam giác cân có 1 góc bằng 60° là tam giác đều.\n• Đường trung trực của đoạn thẳng: Là đường thẳng vuông góc với đoạn thẳng tại trung điểm của nó. Điểm nằm trên đường trung trực thì cách đều hai đầu mút đoạn thẳng: MA = MB.",
                Properties = new List<string>
                {
                    "Trong tam giác cân, đường phân giác của góc ở đỉnh đồng thời là đường trung tuyến, đường cao và đường trung trực của cạnh đáy.",
                    "Mỗi điểm cách đều hai đầu mút của một đoạn thẳng thì nằm trên đường trung trực của đoạn thẳng đó."
                },
                Formulas = new List<string>
                {
                    "Số đo góc ở đáy tam giác cân tại A: ∠B = ∠C = (180° - ∠A) / 2",
                    "Số đo góc ở đỉnh: ∠A = 180° - 2 × ∠B"
                },
                VisualExample = "Tam giác ABC cân tại A có góc ở đỉnh ∠A = 40°. Hai góc ở đáy có số đo bằng: ∠B = ∠C = (180° - 40°) / 2 = 70°.",
                RealWorldExample = "Kim tự tháp Ai Cập có 4 mặt bên là các tam giác cân đối xứng hoàn hảo; cầu treo dây văng tạo thành các tam giác cân hai bên trụ tháp.",
                HasGeometryLab = true,
                DefaultLabShape = "TamGiacCan",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Tam giác ABC cân tại A có góc ở đỉnh ∠A = 80°. Số đo của góc B ở đáy bằng bao nhiêu độ?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "50" },
                        Hint1 = "Trong tam giác cân, hai góc ở đáy bằng nhau: ∠B = ∠C.",
                        Hint2 = "Tính (180° - 80°) ÷ 2.",
                        Explanation = "∠B = (180° - 80°) / 2 = 100° / 2 = 50°."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Một tam giác cân có một góc bằng 60° thì tam giác đó là hình gì?",
                        Type = "single_choice",
                        Options = new List<string> { "Tam giác đều", "Tam giác vuông", "Tam giác vuông cân", "Tam giác tù" },
                        CorrectAnswers = new List<string> { "Tam giác đều" },
                        Hint1 = "Dấu hiệu nhận biết tam giác đều: Tam giác cân có một góc bằng 60°.",
                        Hint2 = "Nếu 1 góc 60°, 2 góc còn lại cũng sẽ đều bằng 60°.",
                        Explanation = "Theo dấu hiệu nhận biết: Tam giác cân có một góc bằng 60° là tam giác đều."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Đường thẳng d là đường trung trực của đoạn thẳng AB. Điểm M nằm trên d thì khẳng định nào sau đây là ĐÚNG?",
                        Type = "single_choice",
                        Options = new List<string> { "MA = MB", "MA > MB", "MA < MB", "MA + MB = AB" },
                        CorrectAnswers = new List<string> { "MA = MB" },
                        Hint1 = "Tính chất đường trung trực: cách đều hai đầu mút.",
                        Hint2 = "Khoảng cách từ M đến A bằng khoảng cách từ M đến B.",
                        Explanation = "Điểm nằm trên đường trung trực của đoạn thẳng thì cách đều hai đầu mút của đoạn thẳng đó: MA = MB."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Tam giác ABC cân tại A có ∠B = 65°. Số đo góc đỉnh A là:",
                        Type = "single_choice",
                        Options = new List<string> { "50°", "65°", "70°", "55°" },
                        CorrectAnswers = new List<string> { "50°" },
                        Explanation = "∠A = 180° - 2 × 65° = 180° - 130° = 50°."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Tam giác vuông cân có hai góc nhọn bằng bao nhiêu độ?",
                        Type = "single_choice",
                        Options = new List<string> { "45°", "60°", "30°", "90°" },
                        CorrectAnswers = new List<string> { "45°" },
                        Explanation = "Tam giác vuông cân có hai góc ở đáy bằng nhau: 90° / 2 = 45°."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Trong tam giác cân, đường thẳng nào sau đây đồng thời là đường cao xuất phát từ đỉnh?",
                        Type = "single_choice",
                        Options = new List<string> { "Đường phân giác góc ở đỉnh", "Đường kính", "Cạnh đáy", "Cạnh bên" },
                        CorrectAnswers = new List<string> { "Đường phân giác góc ở đỉnh" },
                        Explanation = "Trong tam giác cân, đường phân giác góc ở đỉnh đồng thời là đường trung tuyến, đường cao."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Điều kiện nào sau đây chứng minh tam giác ABC là tam giác cân?",
                        Type = "single_choice",
                        Options = new List<string> { "Có hai cạnh bằng nhau hoặc hai góc bằng nhau", "Có một góc vuông", "Có một góc tù", "Có chu vi chia hết cho 3" },
                        CorrectAnswers = new List<string> { "Có hai cạnh bằng nhau hoặc hai góc bằng nhau" },
                        Explanation = "Tam giác có 2 cạnh bằng nhau hoặc 2 góc bằng nhau là tam giác cân."
                    }
                }
            }
        };
    }

    private static List<TestQuestion> GenerateTopicTestLop7_Goc()
    {
        return new List<TestQuestion>
        {
            new TestQuestion { QuestionText = "Câu 1: Hai góc đối đỉnh có số đo như thế nào?", Options = new List<string> { "Bằng nhau", "Bù nhau", "Phụ nhau", "Khác nhau" }, CorrectAnswers = new List<string> { "Bằng nhau" }, Explanation = "Hai góc đối đỉnh thì bằng nhau." },
            new TestQuestion { QuestionText = "Câu 2: Cho góc xOy = 100°. Góc kề bù với góc xOy có số đo là:", Options = new List<string> { "80°", "100°", "90°", "180°" }, CorrectAnswers = new List<string> { "80°" }, Explanation = "180° - 100° = 80°." },
            new TestQuestion { QuestionText = "Câu 3: Tia Oz là phân giác của góc xOy = 130°. Số đo góc xOz là:", Options = new List<string> { "65°", "130°", "50°", "75°" }, CorrectAnswers = new List<string> { "65°" }, Explanation = "130° / 2 = 65°." },
            new TestQuestion { QuestionText = "Câu 4: Nếu một đường thẳng cắt hai đường thẳng song song thì hai góc so le trong:", Options = new List<string> { "Bằng nhau", "Bù nhau", "Phụ nhau", "Có tổng bằng 90°" }, CorrectAnswers = new List<string> { "Bằng nhau" }, Explanation = "Hai góc so le trong bằng nhau." },
            new TestQuestion { QuestionText = "Câu 5: Hai góc đồng vị có số đo 60° thì hai đường thẳng:", Options = new List<string> { "Song song với nhau", "Vuông góc với nhau", "Cắt nhau", "Trùng nhau" }, CorrectAnswers = new List<string> { "Song song với nhau" }, Explanation = "Cặp góc đồng vị bằng nhau thì hai đường thẳng song song." },
            new TestQuestion { QuestionText = "Câu 6: Tiên đề Euclid đề cập đến điều gì?", Options = new List<string> { "Qua một điểm ngoài đường thẳng chỉ có 1 đường thẳng song song với nó", "Tổng 3 góc tam giác", "Hai góc đối đỉnh", "Tam giác vuông" }, CorrectAnswers = new List<string> { "Qua một điểm ngoài đường thẳng chỉ có 1 đường thẳng song song với nó" }, Explanation = "Tiên đề Euclid về đường thẳng song song." },
            new TestQuestion { QuestionText = "Câu 7: Hai đường thẳng cùng vuông góc với đường thẳng thứ ba thì:", Options = new List<string> { "Song song với nhau", "Vuông góc với nhau", "Cắt nhau", "Trùng nhau" }, CorrectAnswers = new List<string> { "Song song với nhau" }, Explanation = "Hai đường thẳng phân biệt cùng vuông góc với đường thẳng thứ ba thì song song." },
            new TestQuestion { QuestionText = "Câu 8: Cho a // b, đường thẳng c cắt a tại A và b tại B. Nếu góc A1 = 45° thì góc trong cùng phía B2 có số đo là:", Options = new List<string> { "135°", "45°", "90°", "60°" }, CorrectAnswers = new List<string> { "135°" }, Explanation = "Hai góc trong cùng phía bù nhau: 180° - 45° = 135°." },
            new TestQuestion { QuestionText = "Câu 9: Cho hai góc kề bù xOy và yOz, biết xOy = 3 yOz. Số đo góc yOz là:", Options = new List<string> { "45°", "135°", "60°", "30°" }, CorrectAnswers = new List<string> { "45°" }, Explanation = "xOy + yOz = 180° => 4 yOz = 180° => yOz = 45°." },
            new TestQuestion { QuestionText = "Câu 10: Khẳng định nào sau đây SAI?", Options = new List<string> { "Hai góc bằng nhau thì đối đỉnh", "Hai góc đối đỉnh thì bằng nhau", "Hai góc kề bù có tổng bằng 180°", "Qua 1 điểm ngoài d chỉ có 1 đường thẳng song song với d" }, CorrectAnswers = new List<string> { "Hai góc bằng nhau thì đối đỉnh" }, Explanation = "Hai góc bằng nhau chưa chắc đã đối đỉnh (chúng có thể ở vị trí khác nhau)." }
        };
    }

    private static List<TestQuestion> GenerateTopicTestLop7_TamGiac()
    {
        return new List<TestQuestion>
        {
            new TestQuestion { QuestionText = "Câu 1: Tổng ba góc của một tam giác bằng:", Options = new List<string> { "180°", "360°", "90°", "270°" }, CorrectAnswers = new List<string> { "180°" }, Explanation = "Tổng ba góc tam giác luôn bằng 180°." },
            new TestQuestion { QuestionText = "Câu 2: Tam giác có 1 góc 90° và 1 góc 40° thì góc còn lại là:", Options = new List<string> { "50°", "40°", "60°", "70°" }, CorrectAnswers = new List<string> { "50°" }, Explanation = "90° - 40° = 50°." },
            new TestQuestion { QuestionText = "Câu 3: Tam giác cân có hai góc ở đáy bằng 70°. Góc ở đỉnh bằng:", Options = new List<string> { "40°", "55°", "70°", "110°" }, CorrectAnswers = new List<string> { "40°" }, Explanation = "180° - 2 × 70° = 40°." },
            new TestQuestion { QuestionText = "Câu 4: Để chứng minh hai tam giác bằng nhau theo trường hợp (c-g-c), góc bằng nhau phải là:", Options = new List<string> { "Góc xen giữa hai cạnh bằng nhau", "Góc bất kỳ", "Góc đối diện", "Góc nhọn" }, CorrectAnswers = new List<string> { "Góc xen giữa hai cạnh bằng nhau" }, Explanation = "Góc xen giữa hai cạnh tương ứng bằng nhau." },
            new TestQuestion { QuestionText = "Câu 5: Tam giác đều có mỗi góc bằng bao nhiêu độ?", Options = new List<string> { "60°", "45°", "90°", "120°" }, CorrectAnswers = new List<string> { "60°" }, Explanation = "180° / 3 = 60°." },
            new TestQuestion { QuestionText = "Câu 6: Điểm nằm trên đường trung trực của đoạn thẳng AB thì:", Options = new List<string> { "Cách đều hai điểm A và B", "Nằm giữa A và B", "Trùng với A", "Gần B hơn A" }, CorrectAnswers = new List<string> { "Cách đều hai điểm A và B" }, Explanation = "Tính chất đường trung trực: MA = MB." },
            new TestQuestion { QuestionText = "Câu 7: Cho ΔABC = ΔMNP, biết AB = 5cm, góc B = 45°. Khẳng định nào đúng?", Options = new List<string> { "MN = 5cm, góc N = 45°", "MP = 5cm, góc P = 45°", "NP = 5cm", "Góc M = 45°" }, CorrectAnswers = new List<string> { "MN = 5cm, góc N = 45°" }, Explanation = "Đỉnh tương ứng B với N nên góc N = 45°, cạnh AB tương ứng với MN = 5cm." },
            new TestQuestion { QuestionText = "Câu 8: Tam giác vuông cân có góc ở đỉnh vuông bằng 90°. Hai góc nhọn bằng:", Options = new List<string> { "45° và 45°", "30° và 60°", "50° và 40°", "60° và 60°" }, CorrectAnswers = new List<string> { "45° và 45°" }, Explanation = "Hai góc nhọn bằng nhau: 90° / 2 = 45°." },
            new TestQuestion { QuestionText = "Câu 9: Góc ngoài của tam giác tại một đỉnh bằng:", Options = new List<string> { "Tổng hai góc trong không kề với nó", "Tổng ba góc trong", "Góc trong kề với nó", "180°" }, CorrectAnswers = new List<string> { "Tổng hai góc trong không kề với nó" }, Explanation = "Tính chất góc ngoài của tam giác." },
            new TestQuestion { QuestionText = "Câu 10: Tam giác cân có góc ở đỉnh bằng 60° thì tam giác đó là:", Options = new List<string> { "Tam giác đều", "Tam giác vuông", "Tam giác tù", "Tam giác vuông cân" }, CorrectAnswers = new List<string> { "Tam giác đều" }, Explanation = "Tam giác cân có 1 góc 60° là tam giác đều." }
        };
    }
}
