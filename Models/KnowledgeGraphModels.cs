namespace YouLearnGeometry.Models;

public class GraphNode
{
    public string Id { get; set; } = string.Empty; // Unique code (e.g. "HinhVuong", "Prop_4GocVuong")
    public string Label { get; set; } = string.Empty; // Display name
    public string Type { get; set; } = string.Empty; // "Shape", "Property", "Concept", "Theorem"
    public string Definition { get; set; } = string.Empty;
    public string IdentificationSign { get; set; } = string.Empty; // Dấu hiệu nhận biết
    public string LearningTip { get; set; } = string.Empty; // Mẹo học tính kế thừa
    public string MinGrade { get; set; } = "6"; // "6" | "8"
    public List<string> Properties { get; set; } = new();
}

public class GraphRelationship
{
    public string SourceId { get; set; } = string.Empty;
    public string TargetId { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // "IS_SPECIAL_CASE_OF", "HAS_PROPERTY", "REQUIRES"
    public string Label { get; set; } = string.Empty; // Display label
    public string Explanation { get; set; } = string.Empty; // Giải thích vì sao quan hệ này tồn tại
}

public class CytoscapeGraphData
{
    public List<CytoscapeNodeElement> Nodes { get; set; } = new();
    public List<CytoscapeEdgeElement> Edges { get; set; } = new();
}

public class CytoscapeNodeElement
{
    public CytoscapeNodeData Data { get; set; } = new();
}

public class CytoscapeNodeData
{
    public string Id { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string MinGrade { get; set; } = "6";
    public string Definition { get; set; } = string.Empty;
    public string IdentificationSign { get; set; } = string.Empty;
    public string LearningTip { get; set; } = string.Empty;
    public string Color { get; set; } = "#3b82f6";
}

public class CytoscapeEdgeElement
{
    public CytoscapeEdgeData Data { get; set; } = new();
}

public class CytoscapeEdgeData
{
    public string Id { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string Target { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Explanation { get; set; } = string.Empty;
}
