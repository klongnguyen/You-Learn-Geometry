using YouLearnGeometry.Models;

namespace YouLearnGeometry.Services;

public partial class DataSeeder
{
    private static CurriculumLevel GetCurriculumLevelLop8()
    {
        return new CurriculumLevel
        {
            GradeLevel = "Lop8",
            Name = "Hình học phẳng Lớp 8 - Kết nối tri thức",
            Description = "Hệ thống hóa cấu trúc hình học suy luận: Định nghĩa, tính chất, dấu hiệu nhận biết tứ giác và phương pháp chứng minh hình học qua định lí Thalès và Pythagore.",
            Topics = new List<Topic>
            {
                new Topic
                {
                    TopicCode = "TU_GIAC_8",
                    Title = "Chủ đề 1: Tứ giác & Tính chất kế thừa",
                    Description = "Học thuyết về tứ giác lồi, hình thang, hình thang cân, hình bình hành, hình chữ nhật, hình thoi, hình vuông và cây phả hệ kế thừa hình học.",
                    Order = 1,
                    TopicTest = GenerateTopicTestLop8_TuGiac()
                },
                new Topic
                {
                    TopicCode = "DINH_LI_HINH_HOC_8",
                    Title = "Chủ đề 2: Định lí Thalès và Định lí Pythagore",
                    Description = "Nắm vững tỉ số đoạn thẳng, định lí Thalès, đường trung bình tam giác và định lí Pythagore trong tam giác vuông.",
                    Order = 2,
                    TopicTest = GenerateTopicTestLop8_DinhLi()
                }
            }
        };
    }

    private static List<Lesson> GetLessonsLop8()
    {
        return new List<Lesson>
        {
            // Bài 1: Tứ giác lồi
            new Lesson
            {
                LessonCode = "LESSON_8_TU_GIAC_LOI",
                GradeLevel = "Lop8",
                TopicCode = "TU_GIAC_8",
                Title = "Tứ giác",
                Order = 1,
                Summary = "Nắm vững định nghĩa tứ giác, tứ giác lồi và định lý tổng bốn góc trong một tứ giác luôn bằng 360°.",
                Definition = "• Tứ giác ABCD là hình gồm bốn đoạn thẳng AB, BC, CD, DA, trong đó bất kì hai đoạn thẳng nào cũng không cùng nằm trên một đường thẳng.\n• Tứ giác lồi là tứ giác luôn nằm trong một nửa mặt phẳng có bờ là đường thẳng chứa bất kì cạnh nào của tứ giác.\n• Định lý: Tổng các góc của một tứ giác luôn bằng 360°: ∠A + ∠B + ∠C + ∠D = 360°.",
                Properties = new List<string>
                {
                    "Từ nay khi nói đến tứ giác mà không giải thích gì thêm, ta hiểu đó là tứ giác lồi.",
                    "Một tứ giác có thể có nhiều nhất 3 góc nhọn hoặc nhiều nhất 3 góc tù."
                },
                Formulas = new List<string>
                {
                    "∠A + ∠B + ∠C + ∠D = 360°",
                    "Góc ngoài của tứ giác tại 1 đỉnh = 180° - Góc trong tương ứng"
                },
                VisualExample = "Tứ giác ABCD có ∠A = 70°, ∠B = 110°, ∠C = 80°. Khi đó góc D = 360° - (70° + 110° + 80°) = 100°.",
                RealWorldExample = "Mặt bàn trà tứ giác vát góc nghệ thuật, cánh buồm tứ giác trên du thuyền hiện đại.",
                HasGeometryLab = true,
                DefaultLabShape = "HinhTuGiac",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Cho tứ giác ABCD có ∠A = 65°, ∠B = 115°, ∠C = 80°. Số đo của góc D bằng bao nhiêu độ?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "100" },
                        Hint1 = "Tổng bốn góc của tứ giác luôn bằng 360°.",
                        Hint2 = "Tính 360° - (65° + 115° + 80°).",
                        Explanation = "∠D = 360° - (65° + 115° + 80°) = 360° - 260° = 100°."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Một tứ giác có thể có nhiều nhất bao nhiêu góc vuông?",
                        Type = "single_choice",
                        Options = new List<string> { "4 góc vuông", "3 góc vuông", "2 góc vuông", "1 góc vuông" },
                        CorrectAnswers = new List<string> { "4 góc vuông" },
                        Hint1 = "Hình chữ nhật và hình vuông đều là tứ giác.",
                        Hint2 = "Hình chữ nhật có 4 góc vuông (4 × 90° = 360°).",
                        Explanation = "Tứ giác có thể có tối đa 4 góc vuông (chính là hình chữ nhật hoặc hình vuông)."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Tứ giác lồi là tứ giác luôn thỏa mãn điều kiện nào sau đây?",
                        Type = "single_choice",
                        Options = new List<string> { "Luôn nằm về một phía của bất kỳ đường thẳng nào chứa một cạnh của nó", "Có 4 cạnh bằng nhau", "Có 2 đường chéo vuông góc", "Có ít nhất 1 góc nhọn" },
                        CorrectAnswers = new List<string> { "Luôn nằm về một phía của bất kỳ đường thẳng nào chứa một cạnh của nó" },
                        Hint1 = "Đây là định nghĩa chuẩn của tứ giác lồi.",
                        Hint2 = "Toàn bộ tứ giác nằm trọn trong một nửa mặt phẳng có bờ là đường thẳng chứa cạnh bất kỳ.",
                        Explanation = "Theo định nghĩa SGK: Tứ giác lồi là tứ giác luôn nằm trong một nửa mặt phẳng có bờ là đường thẳng chứa bất kì cạnh nào của tứ giác."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Tổng 4 góc của một tứ giác lồi luôn bằng:",
                        Type = "single_choice",
                        Options = new List<string> { "360°", "180°", "540°", "720°" },
                        CorrectAnswers = new List<string> { "360°" },
                        Explanation = "Định lý: Tổng các góc của một tứ giác luôn bằng 360°."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Tứ giác có 4 góc bằng nhau thì số đo mỗi góc là:",
                        Type = "single_choice",
                        Options = new List<string> { "90°", "60°", "120°", "45°" },
                        CorrectAnswers = new List<string> { "90°" },
                        Explanation = "Mỗi góc = 360° / 4 = 90°."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Tứ giác ABCD có ∠A = 100°, ∠B = 120°, ∠C = 70°. Số đo góc D là:",
                        Type = "single_choice",
                        Options = new List<string> { "70°", "80°", "60°", "90°" },
                        CorrectAnswers = new List<string> { "70°" },
                        Explanation = "∠D = 360° - (100° + 120° + 70°) = 70°."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Tứ giác có 3 góc nhọn thì góc thứ tư bắt buộc phải là:",
                        Type = "single_choice",
                        Options = new List<string> { "Góc tù", "Góc nhọn", "Góc vuông", "Góc bẹt" },
                        CorrectAnswers = new List<string> { "Góc tù" },
                        Explanation = "Nếu cả 4 góc đều nhọn thì tổng < 360°, vô lý. Do đó góc còn lại phải là góc tù (> 90°)."
                    }
                }
            },

            // Bài 2: Hình thang cân
            new Lesson
            {
                LessonCode = "LESSON_8_HINH_THANG_CAN",
                GradeLevel = "Lop8",
                TopicCode = "TU_GIAC_8",
                Title = "Hình thang cân",
                Order = 2,
                Summary = "Định nghĩa hình thang, hình thang cân; tính chất 2 cạnh bên, 2 đường chéo, 2 góc kề một đáy và các dấu hiệu nhận biết hình thang cân.",
                Definition = "• Hình thang là tứ giác có hai cạnh đối song song (gọi là hai cạnh đáy).\n• Hình thang cân là hình thang có hai góc kề một đáy bằng nhau.\n• Tính chất:\n  1. Hai cạnh bên bằng nhau: AD = BC.\n  2. Hai đường chéo bằng nhau: AC = BD.\n• Dấu hiệu nhận biết:\n  1. Hình thang có hai góc kề một đáy bằng nhau là hình thang cân.\n  2. Hình thang có hai đường chéo bằng nhau là hình thang cân.",
                Properties = new List<string>
                {
                    "Nếu một hình thang có hai cạnh bên song song thì hai cạnh bên đó bằng nhau và hai cạnh đáy bằng nhau (nó trở thành hình bình hành).",
                    "Đường thẳng đi qua trung điểm hai đáy là trục đối xứng của hình thang cân."
                },
                Formulas = new List<string>
                {
                    "AB // CD và ∠C = ∠D (hoặc ∠A = ∠B) => ABCD là hình thang cân",
                    "AB // CD và AC = BD => ABCD là hình thang cân"
                },
                VisualExample = "Hình thang ABCD (AB // CD) có ∠D = 70°. Vì ABCD cân nên ∠C = ∠D = 70°. Do AB // CD nên ∠A = 180° - 70° = 110° và ∠B = 110°.",
                RealWorldExample = "Chậu hoa kiểng hình thang cân ngược, mặt cắt của đập nước thủy điện Hòa Bình chịu áp lực nước.",
                HasGeometryLab = true,
                DefaultLabShape = "HinhThangCan",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Cho hình thang cân ABCD (AB // CD) có ∠C = 60°. Số đo của góc kề bù A ở đáy trên bằng bao nhiêu độ?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "120" },
                        Hint1 = "Vì AB // CD nên hai góc trong cùng phía ∠A và ∠D bù nhau (tổng = 180°).",
                        Hint2 = "Trong hình thang cân ∠D = ∠C = 60°. Suy ra ∠A = 180° - 60°.",
                        Explanation = "Vì ABCD là hình thang cân nên ∠D = ∠C = 60°. Hai góc trong cùng phía bù nhau: ∠A = 180° - 60° = 120°."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Hình thang có hai đường chéo bằng nhau thì kết luận nào sau đây là ĐÚNG?",
                        Type = "single_choice",
                        Options = new List<string> { "Nó là hình thang cân", "Nó là hình bình hành", "Nó là hình chữ nhật", "Nó là hình thoi" },
                        CorrectAnswers = new List<string> { "Nó là hình thang cân" },
                        Hint1 = "Đây là dấu hiệu nhận biết số 2 của hình thang cân.",
                        Hint2 = "Hình thang có hai đường chéo bằng nhau thì luôn là hình thang cân.",
                        Explanation = "Dấu hiệu nhận biết: Hình thang có hai đường chéo bằng nhau là hình thang cân."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Một hình thang có hai cạnh bên bằng nhau thì có chắc chắn là hình thang cân không?",
                        Type = "single_choice",
                        Options = new List<string> { "Không chắc chắn, vì nó có thể là hình bình hành", "Chắc chắn là hình thang cân", "Chắc chắn là hình thoi", "Chắc chắn là hình chữ nhật" },
                        CorrectAnswers = new List<string> { "Không chắc chắn, vì nó có thể là hình bình hành" },
                        Hint1 = "Hãy nhớ trường hợp hai cạnh bên song song và bằng nhau.",
                        Hint2 = "Khi hai cạnh bên song song, nó là hình bình hành chứ không nhất thiết là hình thang cân.",
                        Explanation = "Hình thang có hai cạnh bên bằng nhau chưa chắc là hình thang cân (ví dụ: hình bình hành có 2 cạnh bên bằng nhau nhưng không cân)."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Hình thang cân là hình thang có:",
                        Type = "single_choice",
                        Options = new List<string> { "Hai góc kề một đáy bằng nhau", "Hai cạnh bên vuông góc", "Hai đường chéo vuông góc", "Hai đáy bằng nhau" },
                        CorrectAnswers = new List<string> { "Hai góc kề một đáy bằng nhau" },
                        Explanation = "Định nghĩa: Hình thang cân là hình thang có hai góc kề một đáy bằng nhau."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Hình thang cân ABCD (đáy AB, CD) có AC = 8 cm. Độ dài đường chéo BD là:",
                        Type = "single_choice",
                        Options = new List<string> { "8 cm", "4 cm", "16 cm", "Không xác định" },
                        CorrectAnswers = new List<string> { "8 cm" },
                        Explanation = "Hai đường chéo của hình thang cân bằng nhau: BD = AC = 8 cm."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Cho hình thang ABCD (AB // CD) có ∠A = 110°, ∠C = 70°. Khi đó:",
                        Type = "single_choice",
                        Options = new List<string> { "ABCD là hình thang cân", "ABCD là hình chữ nhật", "ABCD là hình thoi", "Không phải hình thang cân" },
                        CorrectAnswers = new List<string> { "ABCD là hình thang cân" },
                        Explanation = "∠D = 180° - ∠A = 70°. Ta có ∠C = ∠D = 70° nên ABCD là hình thang cân."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Trục đối xứng của hình thang cân là:",
                        Type = "single_choice",
                        Options = new List<string> { "Đường thẳng nối trung điểm hai đáy", "Đường chéo", "Đường nối trung điểm 2 cạnh bên", "Cạnh đáy lớn" },
                        CorrectAnswers = new List<string> { "Đường thẳng nối trung điểm hai đáy" },
                        Explanation = "Đường thẳng nối trung điểm hai đáy là trục đối xứng của hình thang cân."
                    }
                }
            },

            // Bài 3: Hình bình hành
            new Lesson
            {
                LessonCode = "LESSON_8_HINH_BINH_HANH",
                GradeLevel = "Lop8",
                TopicCode = "TU_GIAC_8",
                Title = "Hình bình hành",
                Order = 3,
                Summary = "Định nghĩa, tính chất về cạnh, góc, đường chéo và 5 dấu hiệu nhận biết hình bình hành trong hình học chứng minh.",
                Definition = "• Hình bình hành là tứ giác có các cạnh đối song song.\n• Tính chất: Trong hình bình hành:\n  1. Các cạnh đối bằng nhau (AB = CD, AD = BC).\n  2. Các góc đối bằng nhau (∠A = ∠C, ∠B = ∠D).\n  3. Hai đường chéo cắt nhau tại trung điểm của mỗi đường.\n• 5 Dấu hiệu nhận biết:\n  1. Tứ giác có các cạnh đối song song là HBH.\n  2. Tứ giác có các cạnh đối bằng nhau là HBH.\n  3. Tứ giác có một cặp cạnh đối song song VÀ bằng nhau là HBH.\n  4. Tứ giác có các góc đối bằng nhau là HBH.\n  5. Tứ giác có hai đường chéo cắt nhau tại trung điểm mỗi đường là HBH.",
                Properties = new List<string>
                {
                    "Giao điểm hai đường chéo là tâm đối xứng của hình bình hành.",
                    "Hai góc kề một cạnh của hình bình hành luôn bù nhau: ∠A + ∠B = 180°."
                },
                Formulas = new List<string>
                {
                    "AB // CD và AD // BC <=> ABCD là hình bình hành",
                    "AB // CD và AB = CD <=> ABCD là hình bình hành"
                },
                VisualExample = "Cho tứ giác ABCD có AB = CD = 10 cm và AB // CD. Theo dấu hiệu 3, ABCD lập tức là hình bình hành, suy ra AD = BC và AD // BC.",
                RealWorldExample = "Cần gạt nước kính ô tô chuyển động tịnh tiến theo cơ cấu liên kết hình bình hành; khung xếp cửa sắt kéo.",
                HasGeometryLab = true,
                DefaultLabShape = "HinhBinhHanh",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Trong hình bình hành ABCD, biết ∠A = 70°. Số đo của góc B bằng bao nhiêu độ?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "110" },
                        Hint1 = "Hai góc kề một cạnh của hình bình hành bù nhau (tổng = 180°).",
                        Hint2 = "Tính 180° - 70°.",
                        Explanation = "Vì AD // BC nên hai góc trong cùng phía bù nhau: ∠B = 180° - ∠A = 180° - 70° = 110°."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Dấu hiệu nào sau đây KHÔNG PHẢI là dấu hiệu nhận biết hình bình hành?",
                        Type = "single_choice",
                        Options = new List<string> { "Tứ giác có hai đường chéo bằng nhau", "Tứ giác có các cạnh đối bằng nhau", "Tứ giác có một cặp cạnh đối song song và bằng nhau", "Tứ giác có hai đường chéo cắt nhau tại trung điểm mỗi đường" },
                        CorrectAnswers = new List<string> { "Tứ giác có hai đường chéo bằng nhau" },
                        Hint1 = "Hai đường chéo bằng nhau là dấu hiệu của hình thang cân hoặc hình chữ nhật.",
                        Hint2 = "Hình bình hành thông thường hai đường chéo không nhất thiết bằng nhau.",
                        Explanation = "Hai đường chéo bằng nhau là tính chất của hình chữ nhật/hình thang cân, không phải dấu hiệu nhận biết hình bình hành."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Cho hình bình hành ABCD có chu vi 36 cm, cạnh AB = 10 cm. Độ dài cạnh BC bằng bao nhiêu cm?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "8" },
                        Hint1 = "Chu vi hình bình hành P = 2 × (AB + BC).",
                        Hint2 = "Nửa chu vi = 36 ÷ 2 = 18 cm. BC = 18 - 10.",
                        Explanation = "Nửa chu vi = 36 / 2 = 18 cm. Suy ra BC = 18 - 10 = 8 cm."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Hình bình hành là tứ giác có:",
                        Type = "single_choice",
                        Options = new List<string> { "Các cạnh đối song song", "Bốn cạnh bằng nhau", "Bốn góc bằng nhau", "Hai đường chéo vuông góc" },
                        CorrectAnswers = new List<string> { "Các cạnh đối song song" },
                        Explanation = "Định nghĩa: Hình bình hành là tứ giác có các cạnh đối song song."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Hai đường chéo của hình bình hành có tính chất:",
                        Type = "single_choice",
                        Options = new List<string> { "Cắt nhau tại trung điểm mỗi đường", "Bằng nhau", "Vuông góc với nhau", "Song song với nhau" },
                        CorrectAnswers = new List<string> { "Cắt nhau tại trung điểm mỗi đường" },
                        Explanation = "Tính chất cốt lõi: Hai đường chéo cắt nhau tại trung điểm của mỗi đường."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Tứ giác ABCD có AB // CD và AB = CD thì ABCD là:",
                        Type = "single_choice",
                        Options = new List<string> { "Hình bình hành", "Hình thang cân", "Hình thoi", "Hình thang vuông" },
                        CorrectAnswers = new List<string> { "Hình bình hành" },
                        Explanation = "Dấu hiệu: Tứ giác có 1 cặp cạnh đối song song và bằng nhau là hình bình hành."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Tâm đối xứng của hình bình hành là:",
                        Type = "single_choice",
                        Options = new List<string> { "Giao điểm hai đường chéo", "Một đỉnh của hình", "Trung điểm một cạnh", "Không có tâm đối xứng" },
                        CorrectAnswers = new List<string> { "Giao điểm hai đường chéo" },
                        Explanation = "Giao điểm hai đường chéo của hình bình hành là tâm đối xứng của hình đó."
                    }
                }
            },

            // Bài 4: Hình chữ nhật
            new Lesson
            {
                LessonCode = "LESSON_8_HCN",
                GradeLevel = "Lop8",
                TopicCode = "TU_GIAC_8",
                Title = "Hình chữ nhật",
                Order = 4,
                Summary = "Định nghĩa hình chữ nhật (tứ giác có 4 góc vuông), tính chất hai đường chéo bằng nhau và 4 dấu hiệu chuyển hóa từ hình bình hành/hình thang cân sang hình chữ nhật.",
                Definition = "• Hình chữ nhật là tứ giác có bốn góc vuông: ∠A = ∠B = ∠C = ∠D = 90°.\n• Tính chất: Hình chữ nhật có tất cả tính chất của hình bình hành và hình thang cân:\n  1. Hai đường chéo bằng nhau VÀ cắt nhau tại trung điểm mỗi đường: AC = BD.\n  2. Trong tam giác vuông, đường trung tuyến ứng với cạnh huyền bằng nửa cạnh huyền: AM = BC / 2.\n• Dấu hiệu nhận biết:\n  1. Tứ giác có 3 góc vuông là hình chữ nhật.\n  2. Hình thang cân có một góc vuông là hình chữ nhật.\n  3. Hình bình hành có một góc vuông là hình chữ nhật.\n  4. Hình bình hành có hai đường chéo bằng nhau là hình chữ nhật.",
                Properties = new List<string>
                {
                    "Hình chữ nhật vừa có 1 tâm đối xứng (giao điểm 2 đường chéo) vừa có 2 trục đối xứng.",
                    "Nếu một tam giác có đường trung tuyến ứng với một cạnh bằng nửa cạnh đó thì tam giác đó là tam giác vuông."
                },
                Formulas = new List<string>
                {
                    "HBH + 1 góc vuông => Hình chữ nhật",
                    "HBH + 2 đường chéo bằng nhau => Hình chữ nhật",
                    "ΔABC vuông tại A => Trung tuyến AM = ½ BC"
                },
                VisualExample = "Tam giác vuông ABC vuông tại A có cạnh huyền BC = 10 cm. Trung tuyến AM nối từ đỉnh A đến trung điểm M của BC có độ dài đúng bằng AM = 10 / 2 = 5 cm.",
                RealWorldExample = "Mặt bàn học, màn hình máy tính, tờ giấy A4, khung ảnh tiêu chuẩn.",
                HasGeometryLab = true,
                DefaultLabShape = "HinhChuNhat",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Tam giác ABC vuông tại A có cạnh huyền BC = 14 cm. Độ dài đường trung tuyến AM ứng với cạnh huyền bằng bao nhiêu cm?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "7" },
                        Hint1 = "Định lý: Trong tam giác vuông, đường trung tuyến ứng với cạnh huyền bằng nửa cạnh huyền.",
                        Hint2 = "Tính 14 ÷ 2.",
                        Explanation = "AM = BC / 2 = 14 / 2 = 7 cm."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Hình bình hành ABCD cần thêm điều kiện nào sau đây để trở thành hình chữ nhật?",
                        Type = "single_choice",
                        Options = new List<string> { "Có một góc vuông hoặc hai đường chéo bằng nhau", "Có hai đường chéo vuông góc", "Có bốn cạnh bằng nhau", "Có hai góc kề bù nhau" },
                        CorrectAnswers = new List<string> { "Có một góc vuông hoặc hai đường chéo bằng nhau" },
                        Hint1 = "Xem dấu hiệu chuyển hóa từ HBH sang HCN.",
                        Hint2 = "Thêm 1 góc vuông hoặc thêm tính chất 2 đường chéo bằng nhau.",
                        Explanation = "Hình bình hành có 1 góc vuông HOẶC có 2 đường chéo bằng nhau sẽ trở thành hình chữ nhật."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Một tứ giác chỉ cần có tối thiểu bao nhiêu góc vuông để chắc chắn là hình chữ nhật?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "3" },
                        Hint1 = "Tổng 4 góc là 360°. Nếu có 3 góc 90° thì góc thứ tư bắt buộc phải là 360° - 270° = 90°.",
                        Hint2 = "Dấu hiệu nhận biết số 1: Tứ giác có 3 góc vuông là hình chữ nhật.",
                        Explanation = "Tứ giác có 3 góc vuông thì góc thứ tư tự động bằng 90°, do đó là hình chữ nhật."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Hình chữ nhật là tứ giác có:",
                        Type = "single_choice",
                        Options = new List<string> { "Bốn góc vuông", "Bốn cạnh bằng nhau", "Hai đường chéo vuông góc", "Hai cạnh bên song song" },
                        CorrectAnswers = new List<string> { "Bốn góc vuông" },
                        Explanation = "Định nghĩa: Hình chữ nhật là tứ giác có 4 góc vuông."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Hai đường chéo của hình chữ nhật có tính chất:",
                        Type = "single_choice",
                        Options = new List<string> { "Bằng nhau và cắt nhau tại trung điểm mỗi đường", "Vuông góc với nhau", "Song song với nhau", "Không bằng nhau" },
                        CorrectAnswers = new List<string> { "Bằng nhau và cắt nhau tại trung điểm mỗi đường" },
                        Explanation = "Hai đường chéo của hình chữ nhật vừa bằng nhau vừa cắt nhau tại trung điểm mỗi đường."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Hình thang cân có thêm điều kiện nào thì trở thành hình chữ nhật?",
                        Type = "single_choice",
                        Options = new List<string> { "Có một góc vuông", "Có hai đường chéo bằng nhau", "Có hai cạnh đáy bằng nhau", "Có 4 cạnh bằng nhau" },
                        CorrectAnswers = new List<string> { "Có một góc vuông" },
                        Explanation = "Hình thang cân có 1 góc vuông sẽ trở thành hình chữ nhật."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Số trục đối xứng của hình chữ nhật (không phải hình vuông) là:",
                        Type = "single_choice",
                        Options = new List<string> { "2", "4", "1", "Vô số" },
                        CorrectAnswers = new List<string> { "2" },
                        Explanation = "Hình chữ nhật có 2 trục đối xứng là hai đường trung trực của hai cặp cạnh đối diện."
                    }
                }
            },

            // Bài 5: Hình thoi và Hình vuông
            new Lesson
            {
                LessonCode = "LESSON_8_HINH_THOI_VUONG",
                GradeLevel = "Lop8",
                TopicCode = "TU_GIAC_8",
                Title = "Hình thoi và Hình vuông",
                Order = 5,
                Summary = "Định nghĩa hình thoi (4 cạnh bằng nhau, 2 đường chéo vuông góc là phân giác); định nghĩa hình vuông (đỉnh cao kế thừa: vừa là HCN vừa là hình thoi) và toàn bộ dấu hiệu chuyển hóa.",
                Definition = "• Hình thoi là tứ giác có bốn cạnh bằng nhau.\n  + Tính chất: Hai đường chéo vuông góc với nhau và là các đường phân giác của các góc trong hình thoi.\n  + Dấu hiệu: HBH có 2 cạnh kề bằng nhau / HBH có 2 đường chéo vuông góc / HBH có 1 đường chéo là phân giác.\n• Hình vuông là tứ giác có bốn góc vuông và bốn cạnh bằng nhau.\n  + Hình vuông là hình vừa là hình chữ nhật, vừa là hình thoi.\n  + Dấu hiệu nhận biết hình vuông:\n    1. Hình chữ nhật có hai cạnh kề bằng nhau là hình vuông.\n    2. Hình chữ nhật có hai đường chéo vuông góc là hình vuông.\n    3. Hình chữ nhật có một đường chéo là đường phân giác của một góc là hình vuông.\n    4. Hình thoi có một góc vuông là hình vuông.\n    5. Hình thoi có hai đường chéo bằng nhau là hình vuông.",
                Properties = new List<string>
                {
                    "Hình vuông là hình đối xứng nhất trong các tứ giác: 4 trục đối xứng và 1 tâm đối xứng.",
                    "Hai đường chéo hình vuông: vừa bằng nhau, vừa vuông góc tại trung điểm, vừa là đường phân giác."
                },
                Formulas = new List<string>
                {
                    "HCN + 2 cạnh kề bằng nhau => Hình vuông",
                    "HCN + 2 đường chéo vuông góc => Hình vuông",
                    "Hình thoi + 1 góc vuông => Hình vuông",
                    "Hình thoi + 2 đường chéo bằng nhau => Hình vuông"
                },
                VisualExample = "Cho hình chữ nhật ABCD có đường chéo AC vuông góc với BD. Khi đó ABCD lập tức trở thành hình vuông.",
                RealWorldExample = "Viên gạch gốm Bát Tràng lát nền, ô bàn cờ vua quốc tế, con tem bưu chính hình vuông.",
                HasGeometryLab = true,
                DefaultLabShape = "HinhVuong",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Hình thoi ABCD cần có thêm điều kiện nào để trở thành hình vuông?",
                        Type = "single_choice",
                        Options = new List<string> { "Có một góc vuông hoặc có hai đường chéo bằng nhau", "Có hai đường chéo vuông góc", "Có 4 cạnh bằng nhau", "Có hai cạnh kề bằng nhau" },
                        CorrectAnswers = new List<string> { "Có một góc vuông hoặc có hai đường chéo bằng nhau" },
                        Hint1 = "Hình thoi đã sẵn có 4 cạnh bằng nhau và 2 đường chéo vuông góc.",
                        Hint2 = "Cần thêm đặc tính của hình chữ nhật: 1 góc vuông hoặc 2 đường chéo bằng nhau.",
                        Explanation = "Hình thoi có thêm 1 góc vuông HOẶC có 2 đường chéo bằng nhau sẽ trở thành hình vuông."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Hình vuông cạnh 6 cm có đường chéo dài bao nhiêu cm (làm tròn số theo định lý Pytago d = a√2)?",
                        Type = "single_choice",
                        Options = new List<string> { "6√2 cm", "12 cm", "36 cm", "6 cm" },
                        CorrectAnswers = new List<string> { "6√2 cm" },
                        Hint1 = "Theo Pytago: d² = a² + a² = 2a².",
                        Hint2 = "d = a√2 = 6√2 cm.",
                        Explanation = "Độ dài đường chéo hình vuông cạnh a là d = a√2. Với a = 6 cm thì d = 6√2 cm."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Hai đường chéo của hình thoi có vai trò gì đối với các góc của hình thoi?",
                        Type = "single_choice",
                        Options = new List<string> { "Là các đường phân giác của các góc", "Là các đường trung trực", "Là các đường cao ngoài", "Không có vai trò gì" },
                        CorrectAnswers = new List<string> { "Là các đường phân giác của các góc" },
                        Hint1 = "Đường chéo chia đôi góc ở mỗi đỉnh.",
                        Hint2 = "Đó chính là đường phân giác của góc.",
                        Explanation = "Tính chất đặc trưng của hình thoi: hai đường chéo là các đường phân giác của các góc trong hình thoi."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Hình thoi là tứ giác có:",
                        Type = "single_choice",
                        Options = new List<string> { "Bốn cạnh bằng nhau", "Bốn góc vuông", "Hai đường chéo bằng nhau", "Hai góc đối bù nhau" },
                        CorrectAnswers = new List<string> { "Bốn cạnh bằng nhau" },
                        Explanation = "Định nghĩa: Hình thoi là tứ giác có 4 cạnh bằng nhau."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Hình nào sau đây vừa là hình chữ nhật vừa là hình thoi?",
                        Type = "single_choice",
                        Options = new List<string> { "Hình vuông", "Hình bình hành", "Hình thang cân", "Hình tam giác đều" },
                        CorrectAnswers = new List<string> { "Hình vuông" },
                        Explanation = "Hình vuông kết hợp đầy đủ tất cả các tính chất của hình chữ nhật và hình thoi."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Hình chữ nhật có hai đường chéo vuông góc với nhau là:",
                        Type = "single_choice",
                        Options = new List<string> { "Hình vuông", "Hình thoi thường", "Hình thang", "Hình bình hành thường" },
                        CorrectAnswers = new List<string> { "Hình vuông" },
                        Explanation = "Dấu hiệu: Hình chữ nhật có 2 đường chéo vuông góc là hình vuông."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Nhận định nào sau đây là SAI?",
                        Type = "single_choice",
                        Options = new List<string> { "Mọi hình chữ nhật đều là hình vuông", "Mọi hình vuông đều là hình chữ nhật", "Mọi hình vuông đều là hình thoi", "Mọi hình vuông đều là hình bình hành" },
                        CorrectAnswers = new List<string> { "Mọi hình chữ nhật đều là hình vuông" },
                        Explanation = "Hình chữ nhật chỉ là hình vuông khi có thêm 2 cạnh kề bằng nhau hoặc 2 đường chéo vuông góc."
                    }
                }
            },

            // Bài 6: Định lí Thalès và Định lí Pythagore
            new Lesson
            {
                LessonCode = "LESSON_8_THALES_PYTHAGORE",
                GradeLevel = "Lop8",
                TopicCode = "DINH_LI_HINH_HOC_8",
                Title = "Định lí Thalès và Định lí Pythagore",
                Order = 6,
                Summary = "Hai định lý cột trụ của hình học phẳng: Định lý Thalès (đoạn thẳng tỉ lệ, đường trung bình) và Định lý Pythagore trong tam giác vuông.",
                Definition = "• Định lí Thalès: Nếu một đường thẳng song song với một cạnh của tam giác và cắt hai cạnh còn lại thì nó định ra trên hai cạnh đó những đoạn thẳng tương ứng tỉ lệ: AB'/AB = AC'/AC = B'C'/BC.\n• Đường trung bình của tam giác là đoạn thẳng nối trung điểm hai cạnh: MN // BC và MN = BC / 2.\n• Định lí Pythagore: Trong một tam giác vuông, bình phương của cạnh huyền bằng tổng bình phương của hai cạnh góc vuông: a² + b² = c² (với c là cạnh huyền).\n• Định lí Pythagore đảo: Nếu một tam giác có bình phương một cạnh bằng tổng bình phương hai cạnh kia thì tam giác đó là tam giác vuông.",
                Properties = new List<string>
                {
                    "Bộ ba số Pythagore kinh điển: (3, 4, 5); (5, 12, 13); (6, 8, 10); (7, 24, 25); (8, 15, 17).",
                    "Định lý Thalès dùng để tính khoảng cách không thể đo trực tiếp (như đo chiều cao kim tự tháp, đo bề rộng con sông)."
                },
                Formulas = new List<string>
                {
                    "Pythagore: c² = a² + b² => c = √(a² + b²)",
                    "Đường trung bình tam giác: d // cạnh đáy và d = ½ cạnh đáy",
                    "Thalès: B'C' // BC => AB'/AB = AC'/AC"
                },
                VisualExample = "Tam giác ABC vuông tại A có hai cạnh góc vuông AB = 3 cm, AC = 4 cm. Cạnh huyền BC² = 3² + 4² = 9 + 16 = 25 => BC = √25 = 5 cm.",
                RealWorldExample = "Thalès đã đo chiều cao Kim tự tháp Ai Cập bằng cách đo bóng nắng của cây gậy và bóng nắng của kim tự tháp; thợ xây dùng sợi dây thắt nút tỉ lệ 3-4-5 để ke góc vuông móng nhà.",
                HasGeometryLab = true,
                DefaultLabShape = "TamGiacVuong",
                HasMiniTest = true,
                PracticeExercises = new List<PracticeExercise>
                {
                    new PracticeExercise
                    {
                        QuestionText = "Tam giác vuông có hai cạnh góc vuông dài 6 cm và 8 cm. Độ dài cạnh huyền bằng bao nhiêu cm?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "10" },
                        Hint1 = "Áp dụng định lý Pythagore: c² = a² + b².",
                        Hint2 = "c² = 6² + 8² = 36 + 64 = 100. Căn bậc hai của 100 là 10.",
                        Explanation = "Theo định lý Pytago: c² = 6² + 8² = 36 + 64 = 100 => c = 10 cm."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Đoạn thẳng MN là đường trung bình của tam giác ABC (nối trung điểm AB và AC). Biết cạnh đáy BC = 16 cm. Độ dài MN bằng bao nhiêu cm?",
                        Type = "numeric",
                        Options = new List<string>(),
                        CorrectAnswers = new List<string> { "8" },
                        Hint1 = "Đường trung bình của tam giác bằng nửa cạnh đáy tương ứng.",
                        Hint2 = "MN = BC ÷ 2 = 16 ÷ 2.",
                        Explanation = "Tính chất đường trung bình tam giác: MN = BC / 2 = 16 / 2 = 8 cm."
                    },
                    new PracticeExercise
                    {
                        QuestionText = "Tam giác có độ dài 3 cạnh là 5 cm, 12 cm, 13 cm có phải là tam giác vuông không?",
                        Type = "single_choice",
                        Options = new List<string> { "Là tam giác vuông (theo Pytago đảo)", "Không phải tam giác vuông", "Là tam giác tù", "Là tam giác cân" },
                        CorrectAnswers = new List<string> { "Là tam giác vuông (theo Pytago đảo)" },
                        Hint1 = "Kiểm tra xem 5² + 12² có bằng 13² không.",
                        Hint2 = "5² + 12² = 25 + 144 = 169. 13² = 169. Hai vế bằng nhau.",
                        Explanation = "Vì 5² + 12² = 25 + 144 = 169 = 13² nên theo định lý Pytago đảo, đây là tam giác vuông."
                    }
                },
                MiniTest = new List<TestQuestion>
                {
                    new TestQuestion
                    {
                        QuestionText = "Câu 1: Trong tam giác vuông, hệ thức Pythagore đúng là:",
                        Type = "single_choice",
                        Options = new List<string> { "c² = a² + b² (c là cạnh huyền)", "c = a + b", "c² = a² - b²", "a² = b² + c²" },
                        CorrectAnswers = new List<string> { "c² = a² + b² (c là cạnh huyền)" },
                        Explanation = "Bình phương cạnh huyền bằng tổng bình phương hai cạnh góc vuông."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 2: Tam giác vuông có cạnh huyền 15 cm, một cạnh góc vuông 9 cm. Cạnh góc vuông còn lại là:",
                        Type = "single_choice",
                        Options = new List<string> { "12 cm", "6 cm", "10 cm", "8 cm" },
                        CorrectAnswers = new List<string> { "12 cm" },
                        Explanation = "b² = 15² - 9² = 225 - 81 = 144 => b = 12 cm."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 3: Đường thẳng d song song với cạnh BC của tam giác ABC và cắt AB tại M, cắt AC tại N. Theo định lí Thalès:",
                        Type = "single_choice",
                        Options = new List<string> { "AM/AB = AN/AC", "AM/AC = AN/AB", "AM/AN = AB/BC", "AM + AN = BC" },
                        CorrectAnswers = new List<string> { "AM/AB = AN/AC" },
                        Explanation = "Định lý Thalès trong tam giác: AM/AB = AN/AC."
                    },
                    new TestQuestion
                    {
                        QuestionText = "Câu 4: Bộ ba cạnh nào sau đây KHÔNG tạo thành tam giác vuông?",
                        Type = "single_choice",
                        Options = new List<string> { "4 cm, 5 cm, 6 cm", "3 cm, 4 cm, 5 cm", "6 cm, 8 cm, 10 cm", "5 cm, 12 cm, 13 cm" },
                        CorrectAnswers = new List<string> { "4 cm, 5 cm, 6 cm" },
                        Explanation = "4² + 5² = 16 + 25 = 41 ≠ 6² (36) nên không phải tam giác vuông."
                    }
                }
            }
        };
    }

    private static List<TestQuestion> GenerateTopicTestLop8_TuGiac()
    {
        return new List<TestQuestion>
        {
            new TestQuestion { QuestionText = "Câu 1: Tổng các góc của một tứ giác luôn bằng:", Options = new List<string> { "360°", "180°", "540°", "720°" }, CorrectAnswers = new List<string> { "360°" }, Explanation = "Tổng các góc của một tứ giác bằng 360°." },
            new TestQuestion { QuestionText = "Câu 2: Hình thang là tứ giác có:", Options = new List<string> { "Hai cạnh đối song song", "Bốn cạnh bằng nhau", "Bốn góc vuông", "Hai đường chéo bằng nhau" }, CorrectAnswers = new List<string> { "Hai cạnh đối song song" }, Explanation = "Định nghĩa hình thang: tứ giác có 2 cạnh đối song song." },
            new TestQuestion { QuestionText = "Câu 3: Hình thang cân là hình thang có:", Options = new List<string> { "Hai góc kề một đáy bằng nhau", "Hai cạnh bên vuông góc", "Hai cạnh đáy bằng nhau", "Bốn cạnh bằng nhau" }, CorrectAnswers = new List<string> { "Hai góc kề một đáy bằng nhau" }, Explanation = "Hình thang có 2 góc kề một đáy bằng nhau là hình thang cân." },
            new TestQuestion { QuestionText = "Câu 4: Tứ giác có hai đường chéo cắt nhau tại trung điểm mỗi đường là:", Options = new List<string> { "Hình bình hành", "Hình thang cân", "Hình thoi", "Hình vuông" }, CorrectAnswers = new List<string> { "Hình bình hành" }, Explanation = "Dấu hiệu nhận biết hình bình hành." },
            new TestQuestion { QuestionText = "Câu 5: Hình bình hành có một góc vuông là:", Options = new List<string> { "Hình chữ nhật", "Hình thoi", "Hình thang cân", "Hình vuông" }, CorrectAnswers = new List<string> { "Hình chữ nhật" }, Explanation = "Hình bình hành có 1 góc vuông là hình chữ nhật." },
            new TestQuestion { QuestionText = "Câu 6: Hình bình hành có hai đường chéo vuông góc với nhau là:", Options = new List<string> { "Hình thoi", "Hình chữ nhật", "Hình thang cân", "Hình vuông" }, CorrectAnswers = new List<string> { "Hình thoi" }, Explanation = "Hình bình hành có hai đường chéo vuông góc là hình thoi." },
            new TestQuestion { QuestionText = "Câu 7: Hình chữ nhật có hai cạnh kề bằng nhau là:", Options = new List<string> { "Hình vuông", "Hình thoi", "Hình thang", "Hình bình hành" }, CorrectAnswers = new List<string> { "Hình vuông" }, Explanation = "Hình chữ nhật có 2 cạnh kề bằng nhau là hình vuông." },
            new TestQuestion { QuestionText = "Câu 8: Hình thoi có hai đường chéo bằng nhau là:", Options = new List<string> { "Hình vuông", "Hình chữ nhật", "Hình thang", "Không tồn tại" }, CorrectAnswers = new List<string> { "Hình vuông" }, Explanation = "Hình thoi có 2 đường chéo bằng nhau là hình vuông." },
            new TestQuestion { QuestionText = "Câu 9: Trong tam giác vuông, đường trung tuyến ứng với cạnh huyền:", Options = new List<string> { "Bằng nửa cạnh huyền", "Bằng cạnh huyền", "Bằng cạnh góc vuông", "Vuông góc cạnh huyền" }, CorrectAnswers = new List<string> { "Bằng nửa cạnh huyền" }, Explanation = "Tính chất: trung tuyến ứng với cạnh huyền bằng nửa cạnh huyền." },
            new TestQuestion { QuestionText = "Câu 10: Tứ giác vừa là hình chữ nhật vừa là hình thoi là:", Options = new List<string> { "Hình vuông", "Hình bình hành", "Hình thang cân", "Không tồn tại" }, CorrectAnswers = new List<string> { "Hình vuông" }, Explanation = "Hình vuông vừa là hình chữ nhật vừa là hình thoi." }
        };
    }

    private static List<TestQuestion> GenerateTopicTestLop8_DinhLi()
    {
        return new List<TestQuestion>
        {
            new TestQuestion { QuestionText = "Câu 1: Trong tam giác vuông có hai cạnh góc vuông là a và b, cạnh huyền c tính theo công thức:", Options = new List<string> { "c² = a² + b²", "c = a + b", "c² = a² - b²", "c = a² + b²" }, CorrectAnswers = new List<string> { "c² = a² + b²" }, Explanation = "Định lý Pythagore: c² = a² + b²." },
            new TestQuestion { QuestionText = "Câu 2: Tam giác có 3 cạnh là 9 cm, 12 cm, 15 cm là tam giác gì?", Options = new List<string> { "Tam giác vuông", "Tam giác nhọn", "Tam giác tù", "Tam giác cân" }, CorrectAnswers = new List<string> { "Tam giác vuông" }, Explanation = "9² + 12² = 81 + 144 = 225 = 15² nên là tam giác vuông." },
            new TestQuestion { QuestionText = "Câu 3: Đường trung bình của tam giác có tính chất gì với cạnh đáy tương ứng?", Options = new List<string> { "Song song và bằng nửa cạnh đáy", "Vuông góc và bằng cạnh đáy", "Bằng một phần ba cạnh đáy", "Trùng với cạnh đáy" }, CorrectAnswers = new List<string> { "Song song và bằng nửa cạnh đáy" }, Explanation = "Đường trung bình song song và bằng nửa cạnh đáy." },
            new TestQuestion { QuestionText = "Câu 4: Tam giác ABC có cạnh BC = 20 cm. Độ dài đường trung bình song song với BC là:", Options = new List<string> { "10 cm", "5 cm", "40 cm", "15 cm" }, CorrectAnswers = new List<string> { "10 cm" }, Explanation = "20 / 2 = 10 cm." },
            new TestQuestion { QuestionText = "Câu 5: Định lí Thalès thuận phát biểu rằng:", Options = new List<string> { "Đường thẳng song song với 1 cạnh tam giác định ra trên 2 cạnh kia các đoạn thẳng tương ứng tỉ lệ", "Tổng 3 góc tam giác bằng 180°", "Bình phương cạnh huyền bằng tổng bình phương 2 cạnh góc vuông", "Đường trung tuyến bằng nửa cạnh huyền" }, CorrectAnswers = new List<string> { "Đường thẳng song song với 1 cạnh tam giác định ra trên 2 cạnh kia các đoạn thẳng tương ứng tỉ lệ" }, Explanation = "Nội dung định lý Thalès." },
            new TestQuestion { QuestionText = "Câu 6: Tam giác vuông có cạnh góc vuông là 5 cm, cạnh huyền 13 cm. Cạnh góc vuông còn lại là:", Options = new List<string> { "12 cm", "8 cm", "10 cm", "11 cm" }, CorrectAnswers = new List<string> { "12 cm" }, Explanation = "√(13² - 5²) = √(169 - 25) = √144 = 12 cm." },
            new TestQuestion { QuestionText = "Câu 7: Độ dài đường chéo hình vuông cạnh 5 cm là:", Options = new List<string> { "5√2 cm", "10 cm", "25 cm", "5 cm" }, CorrectAnswers = new List<string> { "5√2 cm" }, Explanation = "d = a√2 = 5√2 cm." },
            new TestQuestion { QuestionText = "Câu 8: Cho ΔABC có MN // BC (M thuộc AB, N thuộc AC). Biết AM = 2cm, MB = 4cm, AN = 3cm. Độ dài NC là:", Options = new List<string> { "6 cm", "4 cm", "5 cm", "9 cm" }, CorrectAnswers = new List<string> { "6 cm" }, Explanation = "Theo Thalès: AM/MB = AN/NC => 2/4 = 3/NC => NC = 6 cm." },
            new TestQuestion { QuestionText = "Câu 9: Tam giác đều cạnh 6 cm có chiều cao bằng:", Options = new List<string> { "3√3 cm", "3 cm", "6 cm", "3√2 cm" }, CorrectAnswers = new List<string> { "3√3 cm" }, Explanation = "h = √(6² - 3²) = √(36 - 9) = √27 = 3√3 cm." },
            new TestQuestion { QuestionText = "Câu 10: Định lí Pythagore đảo dùng để:", Options = new List<string> { "Chứng minh một tam giác là tam giác vuông", "Tính chu vi tam giác", "Chứng minh hai đường thẳng song song", "Tính diện tích hình thang" }, CorrectAnswers = new List<string> { "Chứng minh một tam giác là tam giác vuông" }, Explanation = "Định lý Pythagore đảo dùng để nhận biết và chứng minh tam giác vuông." }
        };
    }
}
