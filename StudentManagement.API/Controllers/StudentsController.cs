using Microsoft.AspNetCore.Mvc;
using StudentManagement.BLL.Dtos.Student;
using StudentManagement.BLL.Services.IServices;

namespace StudentManagement.GUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    
    
    [HttpGet("filter")]
    public async Task<IActionResult> Get(string? search, string filter, int pageSize, int pageNumber = 1)
    {
        var result = await _studentService.Filter(search, filter, pageSize, pageNumber);
        return Ok(result);
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var students = await _studentService.Get();
        return Ok(students);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var student = await _studentService.Get(id);
        return Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> Create(StudentUpsertDTO studentUpsertDto)
    {
        var newStudent = await _studentService.Create(studentUpsertDto);
        return CreatedAtAction(nameof(Get), new {id = newStudent.StudentId}, newStudent);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, StudentUpsertDTO studentUpsertDto)
    {
        var existingStudent = await _studentService.Update(id, studentUpsertDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _studentService.Delete(id);
        return Ok(new { message = "Student deleted Successfully!" });
    }
}