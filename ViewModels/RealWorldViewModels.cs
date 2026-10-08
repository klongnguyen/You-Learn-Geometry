namespace YouLearnGeometry.ViewModels;

public class PointDto
{
    public double X { get; set; }
    public double Y { get; set; }
}

public class RealWorldSampleItem
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string ExpectedShape { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Mode { get; set; } = "polygon"; // "polygon" | "circle"
    public List<PointDto> DefaultVertices { get; set; } = new();
    public PointDto CircleCenter { get; set; } = new() { X = 50, Y = 50 };
    public double CircleRadius { get; set; } = 35.0; // percent of width
    public string SuggestedLessonCode { get; set; } = string.Empty;
    public string SuggestedLessonTitle { get; set; } = string.Empty;
}

public class RealWorldViewModel
{
    public List<RealWorldSampleItem> Samples { get; set; } = new();
    public string ActiveSampleId { get; set; } = "tile_square";
}

public class RealWorldAnalyzeRequest
{
    public string Mode { get; set; } = "polygon";
    public List<PointDto> Points { get; set; } = new();
    public PointDto CircleCenter { get; set; } = new() { X = 50, Y = 50 };
    public double CircleRadius { get; set; } = 35;
    public double ImageWidth { get; set; } = 600;
    public double ImageHeight { get; set; } = 600;
}

public class RealWorldAnalyzeResult
{
    public string ShapeCode { get; set; } = string.Empty;
    public string ShapeName { get; set; } = string.Empty;
    public int Confidence { get; set; } = 95;
    public List<string> Characteristics { get; set; } = new();
    public List<double> SideLengths { get; set; } = new();
    public List<double> Angles { get; set; } = new();
    public double Perimeter { get; set; }
    public double Area { get; set; }
    public string FormulaPerimeter { get; set; } = string.Empty;
    public string FormulaArea { get; set; } = string.Empty;
    public string? SuggestedLessonId { get; set; }
    public string SuggestedLessonTitle { get; set; } = string.Empty;
    public string SuggestedLessonCode { get; set; } = string.Empty;
    public string KnowledgeTip { get; set; } = string.Empty;
}
