namespace YouLearnGeometry.Models;

public class PracticeExercise
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    public string QuestionText { get; set; } = string.Empty;

    public string Type { get; set; } = "single_choice"; // "single_choice" | "multi_choice" | "numeric"

    public List<string> Options { get; set; } = new();

    public List<string> CorrectAnswers { get; set; } = new();

    public string Hint1 { get; set; } = string.Empty;

    public string Hint2 { get; set; } = string.Empty;

    public string Explanation { get; set; } = string.Empty;
}

public class TestQuestion
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    public string QuestionText { get; set; } = string.Empty;

    public string Type { get; set; } = "single_choice"; // "single_choice" | "multi_choice"

    public List<string> Options { get; set; } = new();

    public List<string> CorrectAnswers { get; set; } = new();

    public string Explanation { get; set; } = string.Empty;
}
