using StudentManagement.BLL.Dtos.Course;
using StudentManagement.DAL.Models;

namespace StudentManagement.BLL.Services.IServices;

public interface ICourseService
{
    Task<IEnumerable<Course>> Get();
    Task<Course> Get(int id);
    Task<Course> Create(CourseDTO courseDto);
    Task<Course> Update(int id, CourseDTO courseDto);
    Task Delete(int id);
    
    Task<List<Course>> Filter(int search, string filter, int pageSize, int pageNumber = 1);
}