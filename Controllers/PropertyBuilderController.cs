using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouLearnGeometry.ViewModels;

namespace YouLearnGeometry.Controllers;

[Authorize]
public class PropertyBuilderController : Controller
{
    private static readonly List<string> AllProperties = new()
    {
        "Bốn cạnh bằng nhau",
        "Bốn góc vuông (bằng 90°)",
        "Hai cặp cạnh đối song song",
        "Một cặp cạnh đối song song (hai đáy)",
        "Hai đường chéo bằng nhau",
        "Hai đường chéo vuông góc với nhau",
        "Hai đường chéo cắt nhau tại trung điểm mỗi đường",
        "Các cạnh đối bằng nhau",
        "Hai góc kề một đáy bằng nhau"
    };

    [HttpGet]
    public IActionResult Index()
    {
        var vm = new PropertyBuilderViewModel
        {
            AvailableProperties = AllProperties,
            DeduceState = "Initial"
        };
        return View(vm);
    }

    [HttpPost]
    public IActionResult Deduce([FromBody] List<string> selectedProps)
    {
        selectedProps ??= new List<string>();

        if (!selectedProps.Any())
        {
            return Json(new PropertyBuilderViewModel
            {
                AvailableProperties = AllProperties,
                SelectedProperties = selectedProps,
                DeduceState = "Initial",
                Explanation = "Vui lòng chọn ít nhất một tính chất để hệ thống thực hiện suy luận hình học."
            });
        }

        bool has4EqualSides = selectedProps.Contains("Bốn cạnh bằng nhau");
        bool has4RightAngles = selectedProps.Contains("Bốn góc vuông (bằng 90°)");
        bool has2PairsParallel = selectedProps.Contains("Hai cặp cạnh đối song song");
        bool has1PairParallel = selectedProps.Contains("Một cặp cạnh đối song song (hai đáy)");
        bool hasDiagEqual = selectedProps.Contains("Hai đường chéo bằng nhau");
        bool hasDiagPerp = selectedProps.Contains("Hai đường chéo vuông góc với nhau");
        bool hasDiagMidpoint = selectedProps.Contains("Hai đường chéo cắt nhau tại trung điểm mỗi đường");
        bool hasOppSidesEqual = selectedProps.Contains("Các cạnh đối bằng nhau");
        bool hasAdjacentBaseAngles = selectedProps.Contains("Hai góc kề một đáy bằng nhau");

        // 1. Hình vuông (Đủ điều kiện)
        if ((has4EqualSides && has4RightAngles) ||
            (has4RightAngles && hasDiagPerp) ||
            (has4EqualSides && hasDiagEqual) ||
            (has2PairsParallel && has4RightAngles && has4EqualSides))
        {
            return Json(new PropertyBuilderViewModel
            {
                AvailableProperties = AllProperties,
                SelectedProperties = selectedProps,
                DeduceState = "Sufficient",
                MatchedShapeName = "Hình vuông",
                MatchedShapeCode = "HinhVuong",
                Explanation = "Dữ kiện hoàn toàn đầy đủ để xác định hình vuông. Hình vuông kế thừa toàn bộ tính chất của Hình chữ nhật và Hình thoi."
            });
        }

        // 2. Hình chữ nhật
        if (has4RightAngles || (has2PairsParallel && hasDiagEqual) || (hasDiagMidpoint && hasDiagEqual))
        {
            return Json(new PropertyBuilderViewModel
            {
                AvailableProperties = AllProperties,
                SelectedProperties = selectedProps,
                DeduceState = "Sufficient",
                MatchedShapeName = "Hình chữ nhật",
                MatchedShapeCode = "HinhChuNhat",
                Explanation = "Dữ kiện đầy đủ để xác định hình chữ nhật. Nếu thêm điều kiện 2 cạnh kề bằng nhau hoặc 2 đường chéo vuông góc, hình sẽ trở thành Hình vuông."
            });
        }

        // 3. Hình thoi
        if (has4EqualSides || (has2PairsParallel && hasDiagPerp) || (hasDiagMidpoint && hasDiagPerp))
        {
            return Json(new PropertyBuilderViewModel
            {
                AvailableProperties = AllProperties,
                SelectedProperties = selectedProps,
                DeduceState = "Sufficient",
                MatchedShapeName = "Hình thoi",
                MatchedShapeCode = "HinhThoi",
                Explanation = "Dữ kiện đầy đủ để xác định hình thoi. Nếu có thêm 1 góc vuông hoặc 2 đường chéo bằng nhau, hình sẽ trở thành Hình vuông."
            });
        }

        // 4. Hình thang cân
        if ((has1PairParallel && hasAdjacentBaseAngles) || (has1PairParallel && hasDiagEqual))
        {
            return Json(new PropertyBuilderViewModel
            {
                AvailableProperties = AllProperties,
                SelectedProperties = selectedProps,
                DeduceState = "Sufficient",
                MatchedShapeName = "Hình thang cân",
                MatchedShapeCode = "HinhThangCan",
                Explanation = "Dữ kiện đầy đủ để xác định hình thang cân (hình thang có 2 góc kề một đáy bằng nhau hoặc 2 đường chéo bằng nhau)."
            });
        }

        // 5. Hình bình hành
        if (has2PairsParallel || hasOppSidesEqual || hasDiagMidpoint)
        {
            var candidates = new List<string> { "Hình bình hành", "Hình chữ nhật (nếu thêm góc vuông)", "Hình thoi (nếu thêm 2 đường chéo vuông góc)", "Hình vuông (nếu thêm cả hai)" };
            return Json(new PropertyBuilderViewModel
            {
                AvailableProperties = selectedProps,
                SelectedProperties = selectedProps,
                DeduceState = "Insufficient",
                CandidateShapes = candidates,
                Explanation = "⚠️ Dữ kiện xác định được lớp Hình bình hành, nhưng chưa đủ để xác định hình này có phải là Hình chữ nhật, Hình thoi hay Hình vuông hay không."
            });
        }

        // 6. Hình thang
        if (has1PairParallel)
        {
            var candidates = new List<string> { "Hình thang thường", "Hình thang cân", "Hình bình hành", "Hình chữ nhật", "Hình vuông" };
            return Json(new PropertyBuilderViewModel
            {
                AvailableProperties = selectedProps,
                SelectedProperties = selectedProps,
                DeduceState = "Insufficient",
                CandidateShapes = candidates,
                Explanation = "⚠️ Dữ kiện chỉ đủ để biết đây là một Hình thang. Cần thêm dữ kiện về góc hoặc đường chéo để xác định cụ thể loại hình!"
            });
        }

        // Các dữ kiện đơn lẻ chưa đủ
        var genericCandidates = new List<string> { "Hình chữ nhật", "Hình vuông", "Hình thoi", "Hình bình hành", "Hình thang cân" };
        return Json(new PropertyBuilderViewModel
        {
            AvailableProperties = selectedProps,
            SelectedProperties = selectedProps,
            DeduceState = "Insufficient",
            CandidateShapes = genericCandidates,
            Explanation = "⚠️ Dữ kiện hiện tại CHƯA ĐỦ để xác định duy nhất một hình học. Hãy tích chọn thêm các tính chất khác!"
        });
    }
}
