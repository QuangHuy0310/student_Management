using Microsoft.AspNetCore.Mvc;
using StudentManagement.BLL.Dtos.Greade;
using StudentManagement.BLL.Services.IServices;

namespace StudentManagement.GUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradesController : Controller
{
    private readonly IGradeService _gradeService;

    public GradesController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }
    
    
    [HttpGet("filter")]
    public async Task<IActionResult> Get(int search, string filter, int pageSize, int pageNumber = 1)
    {
        var result = await _gradeService.Filter(search, filter, pageSize, pageNumber);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var grades = await _gradeService.Get();
        return Ok(grades);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var grade = await _gradeService.Get(id);
        return Ok(grade);
    }

    [HttpPost]
    public async Task<IActionResult> Create(GradeUpsertDTO gradeUpsertDto)
    {
        var newGrade = await _gradeService.Create(gradeUpsertDto);
        return CreatedAtAction(nameof(Get), new {id = newGrade.GradeId}, newGrade);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, GradeUpsertDTO gradeUpsertDto)
    {
        await _gradeService.Update(id, gradeUpsertDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _gradeService.Delete(id);
        return Ok(new { message = "Grade delete Successfully!" });
    }
}