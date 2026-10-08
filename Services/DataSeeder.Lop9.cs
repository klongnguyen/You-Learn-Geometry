using YouLearnGeometry.Models;

namespace YouLearnGeometry.Services;

public partial class DataSeeder
{
    private static CurriculumLevel GetCurriculumLevelLop9()
    {
        return new CurriculumLevel
        {
            GradeLevel = "Lop9",
            Name = "Hình học phẳng Lớp 9 - Kết nối tri thức",
            Description = "Hoàn thiện bức tranh hình học THCS: Tỉ số lượng giác và hệ thức lượng trong tam giác vuông; lý thuyết đường tròn, góc nội tiếp, tứ giác nội tiếp và đa giác đều.",
            Topics = new List<Topic>
            {
                new Topic
                {
                    TopicCode = "HE_THUC_LUONG_9",
                    Title = "Chủ đề 1: Hệ thức lượng trong tam giác vuông",
                    Description = "Khám phá tỉ số lượng giác (Sin, Cos, Tan, Cot), bảng lượng giác góc đặc biệt và phương pháp giải tam giác vuông ứng dụng thực tế.",
                    Order = 1,
                    TopicTest = GenerateTopicTestLop9_HeThucLuong()
                },
                new Topic
                {
                    TopicCode = "DUONG_TRON_9",
                    Title = "Chủ đề 2: Đường tròn, Tứ giác nội tiếp & Đa giác đều",
                    Description = "Tìm hiểu vị trí tương đối của đường thẳng và đường tròn, tính chất tiếp tuyến, góc nội tiếp, điều kiện tứ giác nội tiếp và công thức diện tích hình tròn.",
                    Order = 2,
                    TopicTest = GenerateTopicTestLop9_DuongTron()
                }
            }
        };
    }

    private static List<Lesson> GetLessonsLop9()
    {
        return new List<Lesson>
        {
            // Bài 1: Tỉ số lượng giác của góc nhọn
            new Lesson
            {
                LessonCode = "LESSON_9_TI_SO_LUONG_GIAC",
                GradeLevel = "Lop9",
                TopicCode = "HE_THUC_LUONG_9",
                Title = "Bài 11 & 12: Tỉ số lượng giác của góc nhọn",
                Order = 1,
                Summary = "Định nghĩa Sin, Cos, Tan, Cot trong tam giác vuông; mối liên hệ giữa các tỉ số lượng giác của hai góc phụ nhau và bảng giá trị góc đặc biệt (30°, 45°, 60°).",
                Definition = "Cho góc nhọn α trong tam giác vuông ABC (vuông tại A):\n• sin α = Cạnh đối / Cạnh huyền\n• cos α = Cạnh kề / Cạnh huyền\n• tan α = Cạnh đối / Cạnh kề\n• cot α = Cạnh kề / Cạnh đối\n• Bài thơ ghi nhớ dân gian: 'Sao Đi Học (Sin = Đối/Huyền), Cứ Khóc Hoài (Cos = Kề/Huyền), Thôi Đừng Khóc (Tan = Đối/Kề), Có Kẹo Đây (Cot = Kề/Đối)'.\n• Định lý hai góc phụ nhau: Nếu α + β = 90° thì sin α = cos β, cos α = sin β, tan α = cot β, cot α = tan β.",
                Properties = new List<string>
                {
                    "0 < sin α < 1 và 0 < cos α < 1 với mọi góc nhọn α.",
                    "sin² α + cos² α = 1",
                    "tan α = sin α / cos α; cot α = cos α / sin α; tan α × cot α = 1"
                },
                Formulas = new List<string>
                {
                    "sin 30° = 1/2; cos 30° = √3/2; tan 30° = √3/3",
                    "sin 45° = √2/2; cos 45° = √2/2; tan 45° = 1",
                    "sin 60° = √3/2; cos 60° = 1/2; tan 60° = √3"
                },
                VisualExample = "Tam giác ABC vuông tại A có AB = 3 cm, AC = 4 cm, BC = 5 cm. Khi đó sin B = AC/BC = 4/5 = 0.8; cos B = AB/BC = 3/5 = 0.6; tan B = 4/3.",
                RealWorldExample = "Độ dốc của cung đường đèo hiểm trở tính bằng hàm Tan của góc nghiêng; tính góc nâng của tia nắng mặt trời để thiết kế tấm pin năng lượng mặt trời.",
                HasGeometryLab = true,
                DefaultLabShape = "TamGiacVuong",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Cho tam giác ABC vuông tại A có AB = 6 cm, BC = 10 cm. Giá trị của sin C bằng bao nhiêu?",
                        Type = "single_choice",
                        Options = new List<string> { "0.6 (hoặc 3/5)", "0.8 (hoặc 4/5)", "0.75 (hoặc 3/4)", "1.25" },
                        CorrectAnswers = new List<string> { "0.6 (hoặc 3/5)" },
                        Hint1 = "sin C = Cạnh đối / Cạnh huyền = AB / BC.",
                        Hint2 = "Tính 6 ÷ 10 = 0.6.",
                        Explanation = "sin C = AB / BC = 6 / 10 = 3/5 = 0.6."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Giá trị của biểu thức (sin² 35° + cos² 35°) bằng bao nhiêu?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "1" },
                        Hint1 = "Áp dụng hệ thức cơ bản sin² α + cos² α với mọi góc nhọn α.",
                        Hint2 = "Kết quả luôn bằng 1.",
                        Explanation = "Với mọi góc nhọn α, ta luôn có sin² α + cos² α = 1."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Nếu hai góc nhọn α và β phụ nhau (α + β = 90°) thì khẳng định nào sau đây là ĐÚNG?",
                        Type = "single_choice",
                        Options = new List<string> { "sin α = cos β", "sin α = sin β", "tan α = tan β", "cos α = cot β" },
                        CorrectAnswers = new List<string> { "sin α = cos β" },
                        Hint1 = "Định lý: Tỉ số lượng giác của hai góc phụ nhau.",
                        Hint2 = "Sin góc này bằng Cos góc kia, Tan góc này bằng Cot góc kia.",
                        Explanation = "Khi hai góc phụ nhau, sin góc này bằng cos góc kia: sin α = cos β."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Cho góc nhọn α. Khẳng định nào sau đây luôn đúng?",
                        Type = "single_choice",
                        Options = new List<string> { "0 < sin α < 1", "sin α > 1", "sin α < 0", "sin α = 2" },
                        CorrectAnswers = new List<string> { "0 < sin α < 1" },
                        Explanation = "Vì cạnh đối luôn nhỏ hơn cạnh huyền nên 0 < sin α < 1."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Giá trị của tan 45° là:",
                        Type = "single_choice",
                        Options = new List<string> { "1", "√2/2", "√3/2", "1/2" },
                        CorrectAnswers = new List<string> { "1" },
                        Explanation = "Trong tam giác vuông cân, hai cạnh góc vuông bằng nhau nên tan 45° = đối / kề = 1."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Cho biết cos α = 0.8. Giá trị của sin α là (với α nhọn):",
                        Type = "single_choice",
                        Options = new List<string> { "0.6", "0.2", "0.4", "0.75" },
                        CorrectAnswers = new List<string> { "0.6" },
                        Explanation = "sin α = √(1 - cos² α) = √(1 - 0.64) = √0.36 = 0.6."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Tích tan α × cot α bằng:",
                        Type = "single_choice",
                        Options = new List<string> { "1", "0", "2", "sin α" },
                        CorrectAnswers = new List<string> { "1" },
                        Explanation = "Theo định nghĩa: tan α × cot α = (đối/kề) × (kề/đối) = 1."
                    }
                }
            },

            // Bài 2: Hệ thức giữa cạnh và góc trong tam giác vuông
            new Lesson
            {
                LessonCode = "LESSON_9_HE_THUC_TAM_GIAC_VUONG",
                GradeLevel = "Lop9",
                TopicCode = "HE_THUC_LUONG_9",
                Title = "Bài 13: Hệ thức giữa cạnh và góc trong tam giác vuông",
                Order = 2,
                Summary = "Thiết lập hệ thức tính cạnh góc vuông qua cạnh huyền và các tỉ số lượng giác Sin, Cos, Tan, Cot; bài toán giải tam giác vuông và ứng dụng đo đạc thực tế.",
                Definition = "Trong tam giác vuông ABC (vuông tại A, cạnh huyền a, hai cạnh góc vuông b và c):\n• Cạnh góc vuông bằng cạnh huyền nhân với sin góc đối hoặc nhân với cos góc kề:\n  b = a × sin B = a × cos C\n  c = a × sin C = a × cos B\n• Cạnh góc vuông bằng cạnh góc vuông kia nhân với tan góc đối hoặc nhân với cot góc kề:\n  b = c × tan B = c × cot C\n  c = b × tan C = b × cot B\n• Giải tam giác vuông là tìm tất cả các cạnh và các góc còn lại của tam giác khi đã biết hai yếu tố (trong đó có ít nhất một cạnh).",
                Properties = new List<string>
                {
                    "Biết 1 cạnh và 1 góc nhọn => Giải được toàn bộ tam giác.",
                    "Biết 2 cạnh => Giải được toàn bộ các góc nhọn của tam giác."
                },
                Formulas = new List<string>
                {
                    "b = a.sin B = a.cos C",
                    "b = c.tan B = c.cot C",
                    "Chiều cao h = d × tan α (d là khoảng cách từ chân đến vật quan sát, α là góc nâng)"
                },
                VisualExample = "Người quan sát đứng cách chân tòa tháp 50 m nhìn lên đỉnh tháp dưới góc nâng 30°. Chiều cao ngọn tháp tính xấp xỉ: h = 50 × tan 30° = 50 × (√3/3) ≈ 28.87 m.",
                RealWorldExample = "Đo chiều cao cột cờ Lăng Bác, đo chiều cao ngọn hải đăng Vũng Tàu, đo bề rộng con sông Hồng mà không cần bơi qua sông.",
                HasGeometryLab = true,
                DefaultLabShape = "TamGiacVuong",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Tam giác ABC vuông tại A có cạnh huyền a = 12 cm, góc B = 30°. Độ dài cạnh đối diện b (AC) bằng bao nhiêu cm?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "6" },
                        Hint1 = "Áp dụng công thức: b = a × sin B.",
                        Hint2 = "sin 30° = 1/2. Tính 12 × 1/2.",
                        Explanation = "b = a × sin B = 12 × sin 30° = 12 × 0.5 = 6 cm."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Một chiếc thang dài 4 m tựa vào tường tạo với mặt đất góc 60°. Chiều cao của bức tường mà thang chạm tới là bao nhiêu mét (lấy √3 ≈ 1.73)?",
                        Type = "single_choice",
                        Options = new List<string> { "2√3 m (≈ 3.46 m)", "2 m", "4 m", "3 m" },
                        CorrectAnswers = new List<string> { "2√3 m (≈ 3.46 m)" },
                        Hint1 = "Chiều cao h = chiều dài thang × sin 60°.",
                        Hint2 = "h = 4 × (√3/2) = 2√3 m ≈ 3.46 m.",
                        Explanation = "Chiều cao h = 4 × sin 60° = 4 × (√3/2) = 2√3 m ≈ 3.46 m."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Để giải một tam giác vuông, ta cần biết tối thiểu bao nhiêu yếu tố (trong đó có ít nhất một cạnh)?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "2" },
                        Hint1 = "Một góc nhọn và một cạnh, hoặc hai cạnh.",
                        Hint2 = "Cần tối thiểu 2 yếu tố.",
                        Explanation = "Cần biết ít nhất 2 yếu tố (trong đó có ít nhất 1 yếu tố về độ dài cạnh) để giải được một tam giác vuông."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Trong tam giác vuông, cạnh góc vuông bằng cạnh huyền nhân với:",
                        Type = "single_choice",
                        Options = new List<string> { "Sin góc đối hoặc Cos góc kề", "Tan góc đối", "Cot góc kề", "Cos góc đối" },
                        CorrectAnswers = new List<string> { "Sin góc đối hoặc Cos góc kề" },
                        Explanation = "Hệ thức lượng: b = a.sin B = a.cos C."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Tam giác ABC vuông tại A có c = 8 cm, tan B = 1.25. Độ dài cạnh b là:",
                        Type = "single_choice",
                        Options = new List<string> { "10 cm", "6.4 cm", "16 cm", "12 cm" },
                        CorrectAnswers = new List<string> { "10 cm" },
                        Explanation = "b = c × tan B = 8 × 1.25 = 10 cm."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Một cây cối cao 6 m đổ bóng trên mặt đất dài 6 m. Góc nghiêng của tia nắng mặt trời là:",
                        Type = "single_choice",
                        Options = new List<string> { "45°", "30°", "60°", "90°" },
                        CorrectAnswers = new List<string> { "45°" },
                        Explanation = "tan α = đối / kề = 6 / 6 = 1 => α = 45°."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Tam giác vuông có cạnh huyền bằng 10 cm và một góc nhọn 45°. Tam giác đó là:",
                        Type = "single_choice",
                        Options = new List<string> { "Tam giác vuông cân", "Tam giác đều", "Tam giác nhọn", "Tam giác tù" },
                        CorrectAnswers = new List<string> { "Tam giác vuông cân" },
                        Explanation = "Tam giác vuông có 1 góc nhọn 45° thì góc nhọn còn lại cũng là 45°, nên là tam giác vuông cân."
                    }
                }
            },

            // Bài 3: Đường tròn. Dây và tiếp tuyến
            new Lesson
            {
                LessonCode = "LESSON_9_DUONG_TRON_TIEP_TUYEN",
                GradeLevel = "Lop9",
                TopicCode = "DUONG_TRON_9",
                Title = "Bài 14 & 15: Đường tròn. Dây và tiếp tuyến của đường tròn",
                Order = 3,
                Summary = "Định nghĩa đường tròn (O; R), quan hệ vuông góc giữa đường kính và dây cung, dấu hiệu tiếp tuyến và tính chất hai tiếp tuyến cắt nhau.",
                Definition = "• Đường tròn tâm O bán kính R là hình gồm các điểm cách O một khoảng bằng R (kí hiệu (O; R)).\n• Dây và đường kính: Đường kính là dây cung lớn nhất đi qua tâm (d = 2R). Đường kính vuông góc với dây thì đi qua trung điểm của dây đó.\n• Tiếp tuyến của đường tròn: Là đường thẳng chỉ có một điểm chung với đường tròn (điểm đó gọi là tiếp điểm).\n  + Tính chất tiếp tuyến: Tiếp tuyến luôn vuông góc với bán kính đi qua tiếp điểm: d ⊥ OA tại A.\n  + Tính chất hai tiếp tuyến cắt nhau: Nếu hai tiếp tuyến của đường tròn cắt nhau tại một điểm M ngoài đường tròn:\n    1. MA = MB (khoảng cách từ điểm đó đến 2 tiếp điểm bằng nhau).\n    2. Tia MO là tia phân giác của góc AMB.\n    3. Tia OM là tia phân giác của góc AOB.",
                Properties = new List<string>
                {
                    "Khoảng cách từ tâm đến dây càng nhỏ thì dây càng lớn (dây đi qua tâm là lớn nhất).",
                    "Đoạn thẳng nối điểm chung M và tâm O là đường trung trực của đoạn thẳng nối hai tiếp điểm AB."
                },
                Formulas = new List<string>
                {
                    "Đường kính AB ⊥ dây CD tại H => HC = HD",
                    "Tiếp tuyến d tại A => OA ⊥ d",
                    "Hai tiếp tuyến MA, MB cắt nhau tại M => MA = MB và MO là phân giác ∠AMB"
                },
                VisualExample = "Cho đường tròn (O; 5 cm) và dây CD = 8 cm. Hạ OH ⊥ CD, ta có H là trung điểm CD => HC = 4 cm. Theo Pytago: OH = √(5² - 4²) = 3 cm.",
                RealWorldExample = "Bánh đà dây curoa truyền động cơ khí (dây curoa tiếp xúc với bánh đà tạo thành các tiếp tuyến chung); mặt đồng hồ kim tròn.",
                HasGeometryLab = true,
                DefaultLabShape = "DuongTron",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Đường kính vuông góc với một dây cung thì có tính chất gì đối với dây cung đó?",
                        Type = "single_choice",
                        Options = new List<string> { "Đi qua trung điểm của dây đó", "Bằng hai lần dây đó", "Song song với dây đó", "Trùng với dây đó" },
                        CorrectAnswers = new List<string> { "Đi qua trung điểm của dây đó" },
                        Hint1 = "Định lý cơ bản về quan hệ đường kính và dây cung.",
                        Hint2 = "Đường kính chia đôi dây cung tại trung điểm.",
                        Explanation = "Định lý: Trong một đường tròn, đường kính vuông góc với một dây thì đi qua trung điểm của dây ấy."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Từ điểm M ở ngoài đường tròn (O) kẻ hai tiếp tuyến MA và MB (A, B là tiếp điểm). Biết MA = 9 cm. Độ dài tiếp tuyến MB bằng bao nhiêu cm?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "9" },
                        Hint1 = "Tính chất của hai tiếp tuyến cắt nhau.",
                        Hint2 = "MA = MB.",
                        Explanation = "Theo tính chất hai tiếp tuyến cắt nhau, khoảng cách từ M đến hai tiếp điểm bằng nhau: MB = MA = 9 cm."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Góc tạo bởi tiếp tuyến và bán kính đi qua tiếp điểm bằng bao nhiêu độ?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "90" },
                        Hint1 = "Tiếp tuyến luôn vuông góc với bán kính tại tiếp điểm.",
                        Hint2 = "Góc vuông có số đo 90°.",
                        Explanation = "Tiếp tuyến của đường tròn luôn vuông góc với bán kính đi qua tiếp điểm, do đó góc tạo thành bằng 90°."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Dây cung lớn nhất của đường tròn là:",
                        Type = "single_choice",
                        Options = new List<string> { "Đường kính", "Bán kính", "Tiếp tuyến", "Cung tròn" },
                        CorrectAnswers = new List<string> { "Đường kính" },
                        Explanation = "Đường kính là dây cung dài nhất của đường tròn (độ dài bằng 2R)."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Cho đường tròn (O; 10 cm), dây AB = 16 cm. Khoảng cách từ tâm O đến dây AB là:",
                        Type = "single_choice",
                        Options = new List<string> { "6 cm", "8 cm", "4 cm", "5 cm" },
                        CorrectAnswers = new List<string> { "6 cm" },
                        Explanation = "H là trung điểm AB => AH = 8 cm. OH = √(10² - 8²) = 6 cm."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Đường thẳng và đường tròn tiếp xúc nhau khi số điểm chung là:",
                        Type = "single_choice",
                        Options = new List<string> { "1 điểm chung", "2 điểm chung", "0 điểm chung", "Vô số điểm" },
                        CorrectAnswers = new List<string> { "1 điểm chung" },
                        Explanation = "Tiếp tuyến có duy nhất 1 điểm chung với đường tròn (tiếp điểm)."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Cho hai tiếp tuyến MA, MB cắt nhau tại M. Tia MO là:",
                        Type = "single_choice",
                        Options = new List<string> { "Tia phân giác góc AMB", "Đường cao của tam giác", "Tiếp tuyến thứ ba", "Đường kính" },
                        CorrectAnswers = new List<string> { "Tia phân giác góc AMB" },
                        Explanation = "Tia MO là tia phân giác của góc tạo bởi hai tiếp tuyến (∠AMB)."
                    }
                }
            },

            // Bài 4: Góc ở tâm, góc nội tiếp và Tứ giác nội tiếp
            new Lesson
            {
                LessonCode = "LESSON_9_GOC_NOI_TIEP_TU_GIAC",
                GradeLevel = "Lop9",
                TopicCode = "DUONG_TRON_9",
                Title = "Bài 27 & 28: Góc ở tâm, góc nội tiếp và Tứ giác nội tiếp",
                Order = 4,
                Summary = "Định nghĩa góc ở tâm, góc nội tiếp; định lý góc nội tiếp chắn nửa đường tròn là góc vuông và định lý điều kiện tứ giác nội tiếp (tổng hai góc đối bằng 180°).",
                Definition = "• Góc ở tâm là góc có đỉnh trùng với tâm đường tròn: Số đo góc ở tâm bằng số đo cung bị chắn: ∠AOB = sđ cung AB.\n• Góc nội tiếp là góc có đỉnh nằm trên đường tròn và hai cạnh chứa hai dây cung: Số đo góc nội tiếp bằng nửa số đo cung bị chắn: ∠ACB = ½ sđ cung AB = ½ ∠AOB.\n  + Hệ quả vàng: Góc nội tiếp chắn nửa đường tròn là góc vuông (90°).\n• Tứ giác nội tiếp: Là tứ giác có bốn đỉnh cùng nằm trên một đường tròn.\n  + Định lý thuận: Trong một tứ giác nội tiếp, tổng số đo hai góc đối diện bằng 180°: ∠A + ∠C = 180°, ∠B + ∠D = 180°.\n  + Dấu hiệu nhận biết tứ giác nội tiếp:\n    1. Tứ giác có tổng hai góc đối bằng 180°.\n    2. Tứ giác có góc ngoài tại một đỉnh bằng góc trong của đỉnh đối diện.\n    3. Tứ giác có hai đỉnh kề nhau cùng nhìn cạnh chứa hai đỉnh còn lại dưới hai góc bằng nhau.",
                Properties = new List<string>
                {
                    "Mọi hình chữ nhật, hình vuông, hình thang cân đều là tứ giác nội tiếp.",
                    "Các góc nội tiếp cùng chắn một cung hoặc chắn hai cung bằng nhau thì bằng nhau."
                },
                Formulas = new List<string>
                {
                    "∠nội tiếp = ½ ∠ở tâm (cùng chắn 1 cung)",
                    "Góc nội tiếp chắn nửa đường tròn = 90°",
                    "Tứ giác nội tiếp <=> ∠A + ∠C = 180°"
                },
                VisualExample = "Tam giác ABC nội tiếp đường tròn có đường kính BC. Khi đó góc A chắn nửa đường tròn nên ∠BAC = 90° (tam giác ABC vuông tại A).",
                RealWorldExample = "Ống kính quang học phân tích chùm sáng hội tụ trên vòng tròn thị giác; 4 trụ chân tháp Eiffel tạo thành hình chiếu tứ giác nội tiếp vững chắc.",
                HasGeometryLab = true,
                DefaultLabShape = "DuongTron",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Góc nội tiếp chắn nửa đường tròn có số đo bằng bao nhiêu độ?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "90" },
                        Hint1 = "Nửa đường tròn có số đo bằng 180°.",
                        Hint2 = "Số đo góc nội tiếp bằng nửa số đo cung bị chắn = 180° ÷ 2.",
                        Explanation = "Hệ quả nổi tiếng: Góc nội tiếp chắn nửa đường tròn luôn là góc vuông 90°."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Cho tứ giác ABCD nội tiếp đường tròn (O). Biết góc ∠A = 75°. Số đo của góc đối diện ∠C bằng bao nhiêu độ?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "105" },
                        Hint1 = "Trong tứ giác nội tiếp, tổng hai góc đối diện luôn bằng 180°.",
                        Hint2 = "∠C = 180° - 75°.",
                        Explanation = "Vì ABCD là tứ giác nội tiếp nên ∠A + ∠C = 180° => ∠C = 180° - 75° = 105°."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Góc ở tâm chắn cung AB có số đo bằng 80°. Góc nội tiếp cùng chắn cung AB có số đo bằng bao nhiêu độ?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "40" },
                        Hint1 = "Số đo góc nội tiếp bằng một nửa số đo góc ở tâm cùng chắn một cung.",
                        Hint2 = "Tính 80° ÷ 2.",
                        Explanation = "Góc nội tiếp bằng nửa góc ở tâm cùng chắn cung: 80° / 2 = 40°."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Hình nào sau đây LUÔN LÀ tứ giác nội tiếp?",
                        Type = "single_choice",
                        Options = new List<string> { "Hình chữ nhật", "Hình bình hành không vuông", "Hình thoi không vuông", "Hình thang thường" },
                        CorrectAnswers = new List<string> { "Hình chữ nhật" },
                        Explanation = "Hình chữ nhật có tổng 2 góc đối = 90° + 90° = 180° nên luôn nội tiếp được trong đường tròn."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Số đo của góc nội tiếp bằng:",
                        Type = "single_choice",
                        Options = new List<string> { "Nửa số đo cung bị chắn", "Số đo cung bị chắn", "Gấp đôi số đo cung bị chắn", "180°" },
                        CorrectAnswers = new List<string> { "Nửa số đo cung bị chắn" },
                        Explanation = "Định lý: Số đo góc nội tiếp bằng nửa số đo của cung bị chắn."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Cho tứ giác ABCD nội tiếp đường tròn. Nếu ∠B = 110° thì ∠D bằng:",
                        Type = "single_choice",
                        Options = new List<string> { "70°", "110°", "90°", "80°" },
                        CorrectAnswers = new List<string> { "70°" },
                        Explanation = "∠D = 180° - ∠B = 180° - 110° = 70°."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Hai góc nội tiếp cùng chắn một cung thì:",
                        Type = "single_choice",
                        Options = new List<string> { "Bằng nhau", "Bù nhau", "Phụ nhau", "Khác nhau" },
                        CorrectAnswers = new List<string> { "Bằng nhau" },
                        Explanation = "Hệ quả: Các góc nội tiếp cùng chắn một cung thì bằng nhau."
                    }
                }
            },

            // Bài 5: Đa giác đều, chu vi và diện tích hình tròn
            new Lesson
            {
                LessonCode = "LESSON_9_DA_GIAC_DEU",
                GradeLevel = "Lop9",
                TopicCode = "DUONG_TRON_9",
                Title = "Bài 30: Đa giác đều, chu vi và diện tích hình tròn",
                Order = 5,
                Summary = "Định nghĩa đa giác đều, đường tròn nội tiếp và ngoại tiếp đa giác đều; công thức độ dài đường tròn, độ dài cung tròn và diện tích hình tròn, hình quạt tròn.",
                Definition = "• Đa giác đều là đa giác có tất cả các cạnh bằng nhau và tất cả các góc bằng nhau.\n  + Mọi đa giác đều luôn có một đường tròn ngoại tiếp và một đường tròn nội tiếp cùng chung tâm.\n  + Số đo mỗi góc của đa giác đều n cạnh: α = (n - 2) × 180° / n.\n• Độ dài đường tròn (chu vi hình tròn bán kính R): C = 2πR = πd.\n• Độ dài cung tròn n°: l = (πRn) / 180.\n• Diện tích hình tròn: S = πR².\n• Diện tích hình quạt tròn cung n°: Squạt = (πR²n) / 360 = (l × R) / 2.",
                Properties = new List<string>
                {
                    "Số π (pi) xấp xỉ bằng 3.14159... là tỉ số giữa chu vi đường tròn và đường kính của nó.",
                    "Lục giác đều nội tiếp đường tròn bán kính R có cạnh bằng đúng R."
                },
                Formulas = new List<string>
                {
                    "Chu vi đường tròn: C = 2πR",
                    "Diện tích hình tròn: S = πR²",
                    "Độ dài cung tròn n°: l = πRn / 180",
                    "Diện tích quạt tròn: Squạt = πR²n / 360"
                },
                VisualExample = "Hình tròn có bán kính R = 10 cm có chu vi C = 2 × 3.14 × 10 = 62.8 cm và diện tích S = 3.14 × 10² = 314 cm².",
                RealWorldExample = "Bánh xe đạp lăn 1 vòng đi được quãng đường bằng chu vi bánh xe; cánh quạt gió quét tròn tạo vùng năng lượng hình tròn; chiếc quạt nan xòe tạo thành hình quạt tròn.",
                HasGeometryLab = true,
                DefaultLabShape = "LucGiacDeu",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Hình tròn có bán kính R = 7 cm (lấy π ≈ 22/7). Chu vi của hình tròn là bao nhiêu cm?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "44" },
                        Hint1 = "Công thức chu vi đường tròn C = 2 × π × R.",
                        Hint2 = "C = 2 × (22/7) × 7.",
                        Explanation = "Chu vi đường tròn C = 2 × (22/7) × 7 = 44 cm."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Diện tích của hình tròn có đường kính d = 6 cm (bán kính R = 3 cm) bằng bao nhiêu cm² (theo π)?",
                        Type = "single_choice",
                        Options = new List<string> { "9π cm²", "36π cm²", "6π cm²", "12π cm²" },
                        CorrectAnswers = new List<string> { "9π cm²" },
                        Hint1 = "Bán kính R = d ÷ 2 = 3 cm. Diện tích S = π × R².",
                        Hint2 = "S = π × 3² = 9π cm².",
                        Explanation = "Bán kính R = 6 / 2 = 3 cm. Diện tích S = π × R² = π × 3² = 9π cm²."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Mỗi góc trong của hình lục giác đều (6 cạnh) có số đo bằng bao nhiêu độ?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "120" },
                        Hint1 = "Áp dụng công thức: (n - 2) × 180° ÷ n với n = 6.",
                        Hint2 = "Góc = (6 - 2) × 180° ÷ 6 = 4 × 180° ÷ 6 = 120°.",
                        Explanation = "Số đo mỗi góc của lục giác đều: (6 - 2) × 180° / 6 = 720° / 6 = 120°."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Công thức tính diện tích hình tròn bán kính R là:",
                        Type = "single_choice",
                        Options = new List<string> { "S = πR²", "S = 2πR", "S = 4πR²", "S = πd" },
                        CorrectAnswers = new List<string> { "S = πR²" },
                        Explanation = "Diện tích hình tròn là S = πR²."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Bán kính hình tròn tăng lên 2 lần thì diện tích hình tròn tăng lên mấy lần?",
                        Type = "single_choice",
                        Options = new List<string> { "4 lần", "2 lần", "8 lần", "Không đổi" },
                        CorrectAnswers = new List<string> { "4 lần" },
                        Explanation = "Vì diện tích tỉ lệ thuận với R² nên R tăng 2 thì S tăng 2² = 4 lần."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Lục giác đều nội tiếp đường tròn (O; 6 cm) có độ dài cạnh bằng:",
                        Type = "single_choice",
                        Options = new List<string> { "6 cm", "3 cm", "12 cm", "6√3 cm" },
                        CorrectAnswers = new List<string> { "6 cm" },
                        Explanation = "Cạnh của lục giác đều nội tiếp bằng đúng bán kính đường tròn ngoại tiếp: a = R = 6 cm."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Bánh xe có đường kính 0.5 m lăn được 100 vòng thì đi được quãng đường khoảng (lấy π ≈ 3.14):",
                        Type = "single_choice",
                        Options = new List<string> { "157 m", "314 m", "78.5 m", "50 m" },
                        CorrectAnswers = new List<string> { "157 m" },
                        Explanation = "Quãng đường = 100 × (π × d) = 100 × 3.14 × 0.5 = 157 m."
                    }
                }
            }
        };
    }

    private static List<TestQuestion> GenerateTopicTestLop9_HeThucLuong()
    {
        return new List<TestQuestion>
        {
            new TestQuestion { QuestionText = "Câu 1: Cho góc nhọn α, công thức đúng của sin α là:", Options = new List<string> { "Cạnh đối / Cạnh huyền", "Cạnh kề / Cạnh huyền", "Cạnh đối / Cạnh kề", "Cạnh kề / Cạnh đối" }, CorrectAnswers = new List<string> { "Cạnh đối / Cạnh huyền" }, Explanation = "Định nghĩa sin = đối / huyền." },
            new TestQuestion { QuestionText = "Câu 2: Giá trị của sin 30° bằng:", Options = new List<string> { "1/2", "√3/2", "√2/2", "1" }, CorrectAnswers = new List<string> { "1/2" }, Explanation = "sin 30° = 1/2." },
            new TestQuestion { QuestionText = "Câu 3: Cho cos α = 3/5 (α nhọn). Khi đó sin α bằng:", Options = new List<string> { "4/5", "3/4", "1/5", "2/5" }, CorrectAnswers = new List<string> { "4/5" }, Explanation = "sin α = √(1 - 9/25) = 4/5." },
            new TestQuestion { QuestionText = "Câu 4: Giá trị biểu thức sin² 20° + sin² 70° bằng:", Options = new List<string> { "1", "0", "2", "sin 90° / 2" }, CorrectAnswers = new List<string> { "1" }, Explanation = "Vì 70° phụ với 20° nên sin 70° = cos 20°. Biểu thức = sin² 20° + cos² 20° = 1." },
            new TestQuestion { QuestionText = "Câu 5: Trong tam giác vuông, cạnh góc vuông c tính theo cạnh huyền a và góc C là:", Options = new List<string> { "c = a × sin C", "c = a × cos C", "c = a × tan C", "c = a / sin C" }, CorrectAnswers = new List<string> { "c = a × sin C" }, Explanation = "Cạnh góc vuông bằng cạnh huyền nhân sin góc đối." },
            new TestQuestion { QuestionText = "Câu 6: Tam giác ABC vuông tại A có AC = 5 cm, góc C = 60°. Cạnh huyền BC là:", Options = new List<string> { "10 cm", "5√3 cm", "2.5 cm", "5√2 cm" }, CorrectAnswers = new List<string> { "10 cm" }, Explanation = "AC = BC × cos 60° => BC = 5 / (1/2) = 10 cm." },
            new TestQuestion { QuestionText = "Câu 7: Tỉ số lượng giác nào luôn dương và nhỏ hơn 1 với mọi góc nhọn?", Options = new List<string> { "Sin và Cos", "Tan và Cot", "Chỉ có Tan", "Tất cả" }, CorrectAnswers = new List<string> { "Sin và Cos" }, Explanation = "0 < sin α < 1 và 0 < cos α < 1." },
            new TestQuestion { QuestionText = "Câu 8: Giá trị của tan 60° bằng:", Options = new List<string> { "√3", "1", "√3/3", "1/2" }, CorrectAnswers = new List<string> { "√3" }, Explanation = "tan 60° = √3." },
            new TestQuestion { QuestionText = "Câu 9: Một cột cờ cao 7 m đổ bóng trên mặt đất dài 7√3 m. Góc tạo bởi tia nắng mặt trời và mặt đất là:", Options = new List<string> { "30°", "45°", "60°", "15°" }, CorrectAnswers = new List<string> { "30°" }, Explanation = "tan α = 7 / (7√3) = 1/√3 = √3/3 => α = 30°." },
            new TestQuestion { QuestionText = "Câu 10: Tích tan 15° × cot 15° bằng:", Options = new List<string> { "1", "0", "15", "Không xác định" }, CorrectAnswers = new List<string> { "1" }, Explanation = "tan α × cot α = 1." }
        };
    }

    private static List<TestQuestion> GenerateTopicTestLop9_DuongTron()
    {
        return new List<TestQuestion>
        {
            new TestQuestion { QuestionText = "Câu 1: Góc nội tiếp chắn nửa đường tròn là góc:", Options = new List<string> { "Vuông (90°)", "Nhọn", "Tù", "Bẹt" }, CorrectAnswers = new List<string> { "Vuông (90°)" }, Explanation = "Góc nội tiếp chắn nửa đường tròn bằng 90°." },
            new TestQuestion { QuestionText = "Câu 2: Trong tứ giác nội tiếp, tổng hai góc đối diện luôn bằng:", Options = new List<string> { "180°", "360°", "90°", "270°" }, CorrectAnswers = new List<string> { "180°" }, Explanation = "Tổng hai góc đối diện của tứ giác nội tiếp bằng 180°." },
            new TestQuestion { QuestionText = "Câu 3: Tiếp tuyến của đường tròn vuông góc với bán kính tại:", Options = new List<string> { "Tiếp điểm", "Tâm đường tròn", "Điểm bất kỳ ngoài đường tròn", "Trung điểm bán kính" }, CorrectAnswers = new List<string> { "Tiếp điểm" }, Explanation = "Tiếp tuyến vuông góc với bán kính đi qua tiếp điểm." },
            new TestQuestion { QuestionText = "Câu 4: Diện tích hình tròn bán kính 4 cm là:", Options = new List<string> { "16π cm²", "8π cm²", "4π cm²", "64π cm²" }, CorrectAnswers = new List<string> { "16π cm²" }, Explanation = "S = π × 4² = 16π cm²." },
            new TestQuestion { QuestionText = "Câu 5: Chu vi đường tròn có bán kính 5 cm là:", Options = new List<string> { "10π cm", "25π cm", "5π cm", "20π cm" }, CorrectAnswers = new List<string> { "10π cm" }, Explanation = "C = 2 × π × 5 = 10π cm." },
            new TestQuestion { QuestionText = "Câu 6: Cho tứ giác ABCD nội tiếp có góc A = 80°. Góc C đối diện bằng:", Options = new List<string> { "100°", "80°", "90°", "110°" }, CorrectAnswers = new List<string> { "100°" }, Explanation = "180° - 80° = 100°." },
            new TestQuestion { QuestionText = "Câu 7: Số điểm chung của tiếp tuyến và đường tròn là:", Options = new List<string> { "1", "2", "0", "Vô số" }, CorrectAnswers = new List<string> { "1" }, Explanation = "Tiếp tuyến chỉ có 1 điểm chung duy nhất." },
            new TestQuestion { QuestionText = "Câu 8: Đường kính vuông góc với dây cung thì:", Options = new List<string> { "Đi qua trung điểm của dây đó", "Bằng dây đó", "Song song với dây", "Không cắt dây" }, CorrectAnswers = new List<string> { "Đi qua trung điểm của dây đó" }, Explanation = "Quan hệ vuông góc giữa đường kính và dây." },
            new TestQuestion { QuestionText = "Câu 9: Góc ở tâm bằng 100° thì góc nội tiếp cùng chắn cung đó bằng:", Options = new List<string> { "50°", "100°", "200°", "25°" }, CorrectAnswers = new List<string> { "50°" }, Explanation = "100° / 2 = 50°." },
            new TestQuestion { QuestionText = "Câu 10: Tứ giác nào sau đây KHÔNG THỂ nội tiếp đường tròn?", Options = new List<string> { "Hình thoi có góc nhọn 60° (không vuông)", "Hình vuông", "Hình chữ nhật", "Hình thang cân" }, CorrectAnswers = new List<string> { "Hình thoi có góc nhọn 60° (không vuông)" }, Explanation = "Hình thoi có 2 góc đối bằng 60° thì tổng = 120° ≠ 180° nên không nội tiếp được." }
        };
    }
}
