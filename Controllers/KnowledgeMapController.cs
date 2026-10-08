using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouLearnGeometry.Services;

namespace YouLearnGeometry.Controllers;

[Authorize]
public class KnowledgeMapController : Controller
{
    private readonly INeo4jService _neo4j;

    public KnowledgeMapController(INeo4jService neo4j)
    {
        _neo4j = neo4j;
    }

    [HttpGet]
    public IActionResult Index(string? selectedShape = null)
    {
        ViewBag.SelectedShape = selectedShape;
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetGraphData(string? minGrade = null)
    {
        try
        {
            var data = await _neo4j.GetGraphDataAsync(minGrade);
            return Json(data);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetNodeDetails(string id)
    {
        try
        {
            var node = await _neo4j.GetNodeDetailsAsync(id);
            if (node == null) return NotFound();
            return Json(node);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
