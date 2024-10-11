using Microsoft.AspNetCore.Mvc;
using StudentManagement.BLL.Dtos.StudentCourse;
using StudentManagement.BLL.Services.IServices;

namespace StudentManagement.GUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseRegistrationsController : Controller
{
    private readonly ICourseRegistrationService _courseRegistrationService;

    public CourseRegistrationsController(ICourseRegistrationService courseRegistrationService)
    {
        _courseRegistrationService = courseRegistrationService;
    }
    
    [HttpGet("filter")]
    public async Task<IActionResult> Get(string search, string filter, int pageSize, int pageNumber = 1)
    {
        var result = await _courseRegistrationService.Filter(search, filter, pageSize, pageNumber);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var studentCourses = await _courseRegistrationService.Get();
        return Ok(studentCourses);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var studentCourse = await _courseRegistrationService.Get(id);
        return Ok(studentCourse);
    }

    [HttpPost]
    public async Task<IActionResult> Create(StudentCourseUpsertDTO studentCourseUpsertDto)
    {
        var newStudentCourse = await _courseRegistrationService.Create(studentCourseUpsertDto);
        return CreatedAtAction(nameof(Get), new {id = newStudentCourse.StudentCourseId}, newStudentCourse);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, StudentCourseUpsertDTO studentCourseUpsertDto)
    {
        await _courseRegistrationService.Update(id, studentCourseUpsertDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _courseRegistrationService.Delete(id);
        return Ok(new { message = "CourseRegistration delete Successfully!" });
    }
}