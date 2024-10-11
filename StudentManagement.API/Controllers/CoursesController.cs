using Microsoft.AspNetCore.Mvc;
using StudentManagement.BLL.Dtos.Course;
using StudentManagement.BLL.Services.IServices;

namespace StudentManagement.GUI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CoursesController : Controller
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }
    
    
    [HttpGet("filter")]
    public async Task<IActionResult> Get(int search, string filter, int pageSize, int pageNumber = 1)
    {
        var result = await _courseService.Filter(search, filter, pageSize, pageNumber);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var courses = await _courseService.Get();
        return Ok(courses);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var course = await _courseService.Get(id);
        return Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CourseDTO courseDto)
    {
        var newCourse = await _courseService.Create(courseDto);
        return CreatedAtAction(nameof(Get), new {id = newCourse.CourseId}, newCourse);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CourseDTO courseDto)
    {
        await _courseService.Update(id, courseDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _courseService.Delete(id);
        return Ok(new { message = "Course Delete Successfully!" });
    }
}