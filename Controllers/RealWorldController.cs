using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using YouLearnGeometry.Models;
using YouLearnGeometry.Services;
using YouLearnGeometry.ViewModels;

namespace YouLearnGeometry.Controllers;

[Authorize]
public class RealWorldController : Controller
{
    private readonly IMongoDbService _mongo;

    public RealWorldController(IMongoDbService mongo)
    {
        _mongo = mongo;
    }

    [HttpGet]
    public IActionResult Index(string? sample = null)
    {
        var samples = GetPresetSamples();
        var activeId = string.IsNullOrEmpty(sample) ? "tile_square" : sample;
        if (!samples.Any(s => s.Id == activeId))
        {
            activeId = "tile_square";
        }

        var vm = new RealWorldViewModel
        {
            Samples = samples,
            ActiveSampleId = activeId
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Analyze([FromBody] RealWorldAnalyzeRequest request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Dữ liệu không hợp lệ." });
        }

        var result = new RealWorldAnalyzeResult();

        if (request.Mode == "circle")
        {
            AnalyzeCircle(request, result);
        }
        else
        {
            AnalyzePolygon(request, result);
        }

        // Link with MongoDB Lesson
        if (!string.IsNullOrEmpty(result.SuggestedLessonCode))
        {
            var lesson = await _mongo.Lessons
                .Find(l => l.LessonCode == result.SuggestedLessonCode)
                .FirstOrDefaultAsync();

            if (lesson != null)
            {
                result.SuggestedLessonId = lesson.Id;
                result.SuggestedLessonTitle = lesson.Title;
            }
        }

        return Json(result);
    }

    private void AnalyzeCircle(RealWorldAnalyzeRequest req, RealWorldAnalyzeResult res)
    {
        double r = req.CircleRadius;
        double perimeter = 2 * Math.PI * r;
        double area = Math.PI * r * r;

        res.ShapeCode = "HinhTron";
        res.ShapeName = "Hình tròn";
        res.Confidence = 98;
        res.Characteristics = new List<string>
        {
            "Tập hợp tất cả các điểm trên mặt phẳng cách đều tâm O một khoảng cố định bằng R.",
            "Có vô số trục đối xứng (mọi đường thẳng đi qua tâm đều là trục đối xứng).",
            "Tâm O là tâm đối xứng của hình tròn."
        };
        res.SideLengths = new List<double> { Math.Round(r, 1) };
        res.Angles = new List<double> { 360.0 };
        res.Perimeter = Math.Round(perimeter, 1);
        res.Area = Math.Round(area, 1);
        res.FormulaPerimeter = "C = 2 * π * R = π * d";
        res.FormulaArea = "S = π * R²";
        res.SuggestedLessonCode = "LESSON_9_DUONG_TRON_TIEP_TUYEN";
        res.SuggestedLessonTitle = "Đường tròn và tiếp tuyến của đường tròn";
        res.KnowledgeTip = "Hình tròn xuất hiện ở hầu hết các vật chuyển động lăn (bánh xe) nhờ khoảng cách từ tâm đến mặt đất luôn không đổi.";
    }

    private void AnalyzePolygon(RealWorldAnalyzeRequest req, RealWorldAnalyzeResult res)
    {
        var pts = req.Points;
        if (pts == null || pts.Count < 3)
        {
            res.ShapeName = "Chưa đủ đỉnh để nhận diện";
            res.Confidence = 0;
            return;
        }

        int n = pts.Count;
        var lengths = new List<double>();
        var vectors = new List<(double x, double y)>();

        for (int i = 0; i < n; i++)
        {
            var p1 = pts[i];
            var p2 = pts[(i + 1) % n];
            double vx = p2.X - p1.X;
            double vy = p2.Y - p1.Y;
            double len = Math.Sqrt(vx * vx + vy * vy);
            vectors.Add((vx, vy));
            lengths.Add(len);
        }

        // Interior angles
        var angles = new List<double>();
        for (int i = 0; i < n; i++)
        {
            int prevIdx = (i - 1 + n) % n;
            var vPrev = (-vectors[prevIdx].x, -vectors[prevIdx].y);
            var vNext = vectors[i];

            double dot = vPrev.Item1 * vNext.x + vPrev.Item2 * vNext.y;
            double mag1 = Math.Sqrt(vPrev.Item1 * vPrev.Item1 + vPrev.Item2 * vPrev.Item2);
            double mag2 = Math.Sqrt(vNext.x * vNext.x + vNext.y * vNext.y);
            double cosVal = Math.Clamp(dot / (mag1 * mag2), -1.0, 1.0);
            double angleDeg = Math.Acos(cosVal) * (180.0 / Math.PI);
            angles.Add(Math.Round(angleDeg, 1));
        }

        // Shoelace area
        double areaSum = 0;
        for (int i = 0; i < n; i++)
        {
            int nextIdx = (i + 1) % n;
            areaSum += (pts[i].X * pts[nextIdx].Y) - (pts[nextIdx].X * pts[i].Y);
        }
        double area = Math.Abs(areaSum) / 2.0;
        double perimeter = lengths.Sum();

        res.SideLengths = lengths.Select(l => Math.Round(l, 1)).ToList();
        res.Angles = angles;
        res.Perimeter = Math.Round(perimeter, 1);
        res.Area = Math.Round(area, 1);

        if (n == 3)
        {
            ClassifyTriangle(lengths, angles, res);
        }
        else if (n == 4)
        {
            ClassifyQuadrilateral(pts, lengths, angles, vectors, res);
        }
        else if (n == 6)
        {
            ClassifyHexagon(lengths, angles, res);
        }
        else
        {
            res.ShapeCode = "DaGiac";
            res.ShapeName = $"Đa giác {n} cạnh";
            res.Confidence = 85;
            res.Characteristics.Add($"Đa giác có {n} đỉnh và {n} cạnh.");
            res.FormulaPerimeter = "P = Tổng độ dài tất cả các cạnh";
            res.FormulaArea = "S = Tổng diện tích các tam giác cấu thành";
            res.SuggestedLessonCode = "LESSON_9_DA_GIAC_DEU";
            res.SuggestedLessonTitle = "Đa giác đều";
        }
    }

    private void ClassifyTriangle(List<double> len, List<double> ang, RealWorldAnalyzeResult res)
    {
        double maxL = len.Max();
        double minL = len.Min();
        double avgL = len.Average();

        bool isEquilateral = (maxL - minL) / avgL < 0.15 && ang.All(a => Math.Abs(a - 60) < 10);
        bool isRight = ang.Any(a => Math.Abs(a - 90) < 8);
        bool isIsosceles = (Math.Abs(len[0] - len[1]) / avgL < 0.12) ||
                           (Math.Abs(len[1] - len[2]) / avgL < 0.12) ||
                           (Math.Abs(len[0] - len[2]) / avgL < 0.12);

        if (isEquilateral)
        {
            res.ShapeCode = "TamGiacDeu";
            res.ShapeName = "Tam giác đều";
            res.Confidence = 96;
            res.Characteristics.AddRange(new[]
            {
                "3 cạnh có độ dài bằng nhau.",
                "3 góc trong đều bằng 60°.",
                "Có 3 trục đối xứng và tâm đối xứng quay 120°."
            });
            res.FormulaPerimeter = "P = 3 * a";
            res.FormulaArea = "S = (a² * √3) / 4";
            res.SuggestedLessonCode = "LESSON_6_TAM_GIAC_DEU_VUONG_LUC_GIAC";
            res.SuggestedLessonTitle = "Tam giác đều, Hình vuông, Lục giác đều";
            res.KnowledgeTip = "Biển báo nguy hiểm tam giác dùng hình tam giác đều vì có trọng tâm vững chãi và dễ nhận biết từ xa.";
        }
        else if (isRight)
        {
            res.ShapeCode = "TamGiacVuong";
            res.ShapeName = "Tam giác vuông";
            res.Confidence = 94;
            res.Characteristics.AddRange(new[]
            {
                "Có 1 góc vuông bằng 90°.",
                "Thỏa mãn định lý Pythagore: Bình phương cạnh huyền bằng tổng bình phương hai cạnh góc vuông."
            });
            res.FormulaPerimeter = "P = a + b + c";
            res.FormulaArea = "S = 1/2 * a * b (với a, b là hai cạnh góc vuông)";
            res.SuggestedLessonCode = "LESSON_9_HE_THUC_TAM_GIAC_VUONG";
            res.SuggestedLessonTitle = "Hệ thức lượng trong tam giác vuông";
            res.KnowledgeTip = "Thước ê-ke trong cặp sách là ví dụ thực tế chuẩn xác của tam giác vuông.";
        }
        else if (isIsosceles)
        {
            res.ShapeCode = "TamGiacCan";
            res.ShapeName = "Tam giác cân";
            res.Confidence = 92;
            res.Characteristics.AddRange(new[]
            {
                "Có hai cạnh bên bằng nhau.",
                "Có hai góc ở đáy bằng nhau.",
                "Đường cao ứng với đáy đồng thời là đường trung tuyến, trung trực và phân giác."
            });
            res.FormulaPerimeter = "P = 2 * a + b";
            res.FormulaArea = "S = 1/2 * đáy * chiều cao";
            res.SuggestedLessonCode = "LESSON_7_TAM_GIAC_CAN";
            res.SuggestedLessonTitle = "Tam giác cân và đường trung trực";
            res.KnowledgeTip = "Mái nhà ngói chữ A là ứng dụng kinh điển của tam giác cân để thoát nước mưa đều về hai phía.";
        }
        else
        {
            res.ShapeCode = "TamGiac";
            res.ShapeName = "Tam giác thường";
            res.Confidence = 88;
            res.Characteristics.Add("Tổng 3 góc trong của một tam giác luôn bằng 180°.");
            res.FormulaPerimeter = "P = a + b + c";
            res.FormulaArea = "S = 1/2 * đáy * chiều cao";
            res.SuggestedLessonCode = "LESSON_7_TONG_GOC_TAM_GIAC";
            res.SuggestedLessonTitle = "Tổng các góc trong một tam giác";
            res.KnowledgeTip = "Cấu trúc tam giác là cấu trúc chịu lực vững chắc nhất trong kỹ thuật xây dựng cầu đường và cần cẩu.";
        }
    }

    private void ClassifyQuadrilateral(List<PointDto> pts, List<double> len, List<double> ang, List<(double x, double y)> vec, RealWorldAnalyzeResult res)
    {
        double avgL = len.Average();
        double maxL = len.Max();
        double minL = len.Min();

        // Opposite sides parallel test (cross product normalized)
        double cross02 = Math.Abs(vec[0].x * vec[2].y - vec[0].y * vec[2].x) / (len[0] * len[2]);
        double cross13 = Math.Abs(vec[1].x * vec[3].y - vec[1].y * vec[3].x) / (len[1] * len[3]);

        bool pair02Parallel = cross02 < 0.22; // opposite sides 0 and 2 parallel
        bool pair13Parallel = cross13 < 0.22; // opposite sides 1 and 3 parallel
        bool bothPairsParallel = pair02Parallel && pair13Parallel;
        bool onePairParallel = pair02Parallel || pair13Parallel;

        // Opposite sides length equality
        bool opp02Equal = Math.Abs(len[0] - len[2]) / avgL < 0.16;
        bool opp13Equal = Math.Abs(len[1] - len[3]) / avgL < 0.16;
        bool allSidesEqual = (maxL - minL) / avgL < 0.15;

        // Angles test: right angles ~ 90 deg
        bool allAnglesRight = ang.All(a => Math.Abs(a - 90) < 14);

        // Diagonals
        double d1 = Math.Sqrt(Math.Pow(pts[2].X - pts[0].X, 2) + Math.Pow(pts[2].Y - pts[0].Y, 2));
        double d2 = Math.Sqrt(Math.Pow(pts[3].X - pts[1].X, 2) + Math.Pow(pts[3].Y - pts[1].Y, 2));
        bool diagsEqual = Math.Abs(d1 - d2) / Math.Max(d1, d2) < 0.14;

        // Diagonals perpendicular
        var diagVec1 = (x: pts[2].X - pts[0].X, y: pts[2].Y - pts[0].Y);
        var diagVec2 = (x: pts[3].X - pts[1].X, y: pts[3].Y - pts[1].Y);
        double diagDot = Math.Abs(diagVec1.x * diagVec2.x + diagVec1.y * diagVec2.y);
        bool diagsPerpendicular = (diagDot / (d1 * d2)) < 0.22;

        if (allSidesEqual && (allAnglesRight || diagsEqual))
        {
            res.ShapeCode = "HinhVuong";
            res.ShapeName = "Hình vuông";
            res.Confidence = 97;
            res.Characteristics.AddRange(new[]
            {
                "4 cạnh bằng nhau.",
                "4 góc vuông bằng 90°.",
                "2 đường chéo bằng nhau, vuông góc tại trung điểm mỗi đường.",
                "Là hình chữ nhật có 4 cạnh bằng nhau, đồng thời là hình thoi có 4 góc vuông."
            });
            res.FormulaPerimeter = "P = 4 * a";
            res.FormulaArea = "S = a²";
            res.SuggestedLessonCode = "LESSON_8_HINH_THOI_VUONG";
            res.SuggestedLessonTitle = "Hình thoi và Hình vuông";
            res.KnowledgeTip = "Gạch lát nền hình vuông giúp ghép khít không để lại khe hở (tessellation) nhờ tổng 4 góc tại một đỉnh là 360°.";
        }
        else if (allSidesEqual || (bothPairsParallel && diagsPerpendicular))
        {
            res.ShapeCode = "HinhThoi";
            res.ShapeName = "Hình thoi";
            res.Confidence = 95;
            res.Characteristics.AddRange(new[]
            {
                "4 cạnh có độ dài bằng nhau.",
                "Các cặp cạnh đối song song.",
                "Hai đường chéo vuông góc với nhau tại trung điểm mỗi đường.",
                "Hai đường chéo là các đường phân giác của các góc của hình thoi."
            });
            res.FormulaPerimeter = "P = 4 * a";
            res.FormulaArea = "S = 1/2 * d1 * d2 (với d1, d2 là hai đường chéo)";
            res.SuggestedLessonCode = "LESSON_8_HINH_THOI_VUONG";
            res.SuggestedLessonTitle = "Hình thoi và Hình vuông";
            res.KnowledgeTip = "Cánh diều hình thoi có khung 2 thanh tre vuông góc giúp chịu lực gió phân bổ đều sang 4 góc.";
        }
        else if (allAnglesRight || (bothPairsParallel && diagsEqual))
        {
            res.ShapeCode = "HinhChuNhat";
            res.ShapeName = "Hình chữ nhật";
            res.Confidence = 96;
            res.Characteristics.AddRange(new[]
            {
                "Có 4 góc vuông bằng 90°.",
                "Hai cặp cạnh đối song song và bằng nhau.",
                "Hai đường chéo bằng nhau và cắt nhau tại trung điểm mỗi đường."
            });
            res.FormulaPerimeter = "P = 2 * (dài + rộng)";
            res.FormulaArea = "S = dài * rộng";
            res.SuggestedLessonCode = "LESSON_8_HCN";
            res.SuggestedLessonTitle = "Hình chữ nhật và dấu hiệu nhận biết";
            res.KnowledgeTip = "Bảng lớp, trang sách, màn hình máy tính đều chọn hình chữ nhật để tối ưu diện tích hiển thị văn bản theo dòng ngang.";
        }
        else if (bothPairsParallel && opp02Equal && opp13Equal)
        {
            res.ShapeCode = "HinhBinhHanh";
            res.ShapeName = "Hình bình hành";
            res.Confidence = 93;
            res.Characteristics.AddRange(new[]
            {
                "Các cặp cạnh đối song song và bằng nhau.",
                "Các góc đối bằng nhau.",
                "Hai đường chéo cắt nhau tại trung điểm của mỗi đường."
            });
            res.FormulaPerimeter = "P = 2 * (a + b)";
            res.FormulaArea = "S = cạnh đáy * chiều cao tương ứng";
            res.SuggestedLessonCode = "LESSON_8_HINH_BINH_HANH";
            res.SuggestedLessonTitle = "Hình bình hành và tính chất";
            res.KnowledgeTip = "Thang xếp và giàn nâng thủy lực hoạt động theo nguyên lý chuyển động hình bình hành giúp giữ thăng bằng.";
        }
        else if (onePairParallel)
        {
            bool legsEqual = pair02Parallel
                ? (Math.Abs(len[1] - len[3]) / avgL < 0.15)
                : (Math.Abs(len[0] - len[2]) / avgL < 0.15);

            if (legsEqual || diagsEqual)
            {
                res.ShapeCode = "HinhThangCan";
                res.ShapeName = "Hình thang cân";
                res.Confidence = 94;
                res.Characteristics.AddRange(new[]
                {
                    "Có hai đáy song song với nhau.",
                    "Hai cạnh bên bằng nhau.",
                    "Hai góc kề một đáy bằng nhau.",
                    "Hai đường chéo bằng nhau."
                });
                res.FormulaPerimeter = "P = đáy lớn + đáy nhỏ + 2 * cạnh bên";
                res.FormulaArea = "S = 1/2 * (đáy lớn + đáy nhỏ) * chiều cao";
                res.SuggestedLessonCode = "LESSON_8_HINH_THANG_CAN";
                res.SuggestedLessonTitle = "Hình thang cân và dấu hiệu nhận biết";
                res.KnowledgeTip = "Khung giàn cầu sắt và bờ đê thiết kế hình thang cân để chân đế rộng chịu áp lực lớn nhất.";
            }
            else
            {
                res.ShapeCode = "HinhThang";
                res.ShapeName = "Hình thang";
                res.Confidence = 90;
                res.Characteristics.AddRange(new[]
                {
                    "Tứ giác có hai cạnh đối song song (hai đáy)."
                });
                res.FormulaPerimeter = "P = Tổng 4 cạnh";
                res.FormulaArea = "S = 1/2 * (a + b) * h";
                res.SuggestedLessonCode = "LESSON_6_HBH_THANG_CAN";
                res.SuggestedLessonTitle = "Hình bình hành và Hình thang cân";
                res.KnowledgeTip = "Thửa ruộng bậc thang hoặc mái dốc một bên thường có hình thang.";
            }
        }
        else
        {
            res.ShapeCode = "TuGiac";
            res.ShapeName = "Tứ giác";
            res.Confidence = 85;
            res.Characteristics.AddRange(new[]
            {
                "Tứ giác lồi có 4 đỉnh và 4 cạnh.",
                "Tổng 4 góc trong của một tứ giác luôn bằng 360°."
            });
            res.FormulaPerimeter = "P = a + b + c + d";
            res.FormulaArea = "S = Chia thành 2 tam giác để tính";
            res.SuggestedLessonCode = "LESSON_8_TU_GIAC_LOI";
            res.SuggestedLessonTitle = "Tứ giác và định lý tổng các góc";
            res.KnowledgeTip = "Bất kỳ tứ giác nào cũng có thể chia thành hai tam giác qua một đường chéo.";
        }
    }

    private void ClassifyHexagon(List<double> len, List<double> ang, RealWorldAnalyzeResult res)
    {
        double avgL = len.Average();
        bool allSidesEqual = (len.Max() - len.Min()) / avgL < 0.16;
        bool allAngles120 = ang.All(a => Math.Abs(a - 120) < 15);

        if (allSidesEqual || allAngles120)
        {
            res.ShapeCode = "LucGiacDeu";
            res.ShapeName = "Lục giác đều";
            res.Confidence = 95;
            res.Characteristics.AddRange(new[]
            {
                "6 cạnh có độ dài bằng nhau.",
                "6 góc trong đều bằng 120°.",
                "Được ghép từ 6 tam giác đều chung một đỉnh tại tâm."
            });
            res.FormulaPerimeter = "P = 6 * a";
            res.FormulaArea = "S = (3 * √3 * a²) / 2";
            res.SuggestedLessonCode = "LESSON_6_TAM_GIAC_DEU_VUONG_LUC_GIAC";
            res.SuggestedLessonTitle = "Tam giác đều, Hình vuông, Lục giác đều";
            res.KnowledgeTip = "Tổ ong có cấu trúc lục giác đều vì đây là hình tiết kiệm sáp ong nhất nhưng chứa được thể tích mật tối đa.";
        }
        else
        {
            res.ShapeCode = "LucGiac";
            res.ShapeName = "Lục giác";
            res.Confidence = 88;
            res.Characteristics.Add("Đa giác có 6 cạnh và 6 đỉnh.");
            res.FormulaPerimeter = "P = Tổng 6 cạnh";
            res.FormulaArea = "S = Tổng diện tích các tam giác cấu thành";
            res.SuggestedLessonCode = "LESSON_9_DA_GIAC_DEU";
            res.SuggestedLessonTitle = "Đa giác đều";
        }
    }

    private List<RealWorldSampleItem> GetPresetSamples()
    {
        return new List<RealWorldSampleItem>
        {
            new RealWorldSampleItem
            {
                Id = "tile_square",
                Title = "Gạch hoa văn lát nền",
                ExpectedShape = "Hình vuông",
                ImageUrl = "/images/realworld/tile_square.jpg",
                Description = "Gạch men trang trí đối xứng 4 góc, các cạnh thẳng tắp ghép liền kề.",
                Mode = "polygon",
                DefaultVertices = new List<PointDto>
                {
                    new() { X = 8.5, Y = 6.5 },
                    new() { X = 91.5, Y = 6.5 },
                    new() { X = 91.5, Y = 93.0 },
                    new() { X = 8.5, Y = 93.0 }
                },
                SuggestedLessonCode = "LESSON_8_HINH_THOI_VUONG",
                SuggestedLessonTitle = "Hình thoi và Hình vuông"
            },
            new RealWorldSampleItem
            {
                Id = "frame_rectangle",
                Title = "Bảng lớp học phấn trắng",
                ExpectedShape = "Hình chữ nhật",
                ImageUrl = "/images/realworld/frame_rectangle.jpg",
                Description = "Khung bảng gỗ chữ nhật với 4 góc vuông và hai cặp cạnh đối song song.",
                Mode = "polygon",
                DefaultVertices = new List<PointDto>
                {
                    new() { X = 17.5, Y = 19.5 },
                    new() { X = 89.0, Y = 22.5 },
                    new() { X = 89.0, Y = 84.5 },
                    new() { X = 17.5, Y = 81.5 }
                },
                SuggestedLessonCode = "LESSON_8_HCN",
                SuggestedLessonTitle = "Hình chữ nhật và dấu hiệu nhận biết"
            },
            new RealWorldSampleItem
            {
                Id = "kite_rhombus",
                Title = "Cánh diều truyền thống",
                ExpectedShape = "Hình thoi",
                ImageUrl = "/images/realworld/kite_rhombus.jpg",
                Description = "Cánh diều 4 cánh đối xứng với 2 nan tre chéo nhau vuông góc ở tâm.",
                Mode = "polygon",
                DefaultVertices = new List<PointDto>
                {
                    new() { X = 41.0, Y = 16.5 },
                    new() { X = 71.5, Y = 25.0 },
                    new() { X = 64.0, Y = 64.5 },
                    new() { X = 33.5, Y = 42.5 }
                },
                SuggestedLessonCode = "LESSON_8_HINH_THOI_VUONG",
                SuggestedLessonTitle = "Hình thoi và Hình vuông"
            },
            new RealWorldSampleItem
            {
                Id = "sign_triangle",
                Title = "Biển báo nguy hiểm đường bộ",
                ExpectedShape = "Tam giác đều",
                ImageUrl = "/images/realworld/sign_triangle.jpg",
                Description = "Biển cảnh báo viền đỏ nền vàng hình tam giác đều kiên cố trên cột thép.",
                Mode = "polygon",
                DefaultVertices = new List<PointDto>
                {
                    new() { X = 50.0, Y = 21.5 },
                    new() { X = 27.0, Y = 61.5 },
                    new() { X = 73.0, Y = 61.5 }
                },
                SuggestedLessonCode = "LESSON_6_TAM_GIAC_DEU_VUONG_LUC_GIAC",
                SuggestedLessonTitle = "Tam giác đều, Hình vuông, Lục giác đều"
            },
            new RealWorldSampleItem
            {
                Id = "roof_trapezoid",
                Title = "Khung giàn cầu sắt vượt sông",
                ExpectedShape = "Hình thang cân",
                ImageUrl = "/images/realworld/roof_trapezoid.jpg",
                Description = "Mặt đứng kết cấu dầm thép chịu lực với hai đáy song song và hai dầm nghiêng bằng nhau.",
                Mode = "polygon",
                DefaultVertices = new List<PointDto>
                {
                    new() { X = 28.5, Y = 10.5 },
                    new() { X = 71.5, Y = 10.5 },
                    new() { X = 95.0, Y = 87.0 },
                    new() { X = 5.0, Y = 87.0 }
                },
                SuggestedLessonCode = "LESSON_8_HINH_THANG_CAN",
                SuggestedLessonTitle = "Hình thang cân và dấu hiệu nhận biết"
            },
            new RealWorldSampleItem
            {
                Id = "clock_circle",
                Title = "Đồng hồ treo tường tròn",
                ExpectedShape = "Hình tròn",
                ImageUrl = "/images/realworld/clock_circle.jpg",
                Description = "Mặt đồng hồ tròn đối xứng hoàn hảo, các vạch số cách đều tâm O.",
                Mode = "circle",
                CircleCenter = new PointDto { X = 50.0, Y = 50.0 },
                CircleRadius = 35.5,
                SuggestedLessonCode = "LESSON_9_DUONG_TRON_TIEP_TUYEN",
                SuggestedLessonTitle = "Đường tròn và tiếp tuyến của đường tròn"
            }
        };
    }
}
