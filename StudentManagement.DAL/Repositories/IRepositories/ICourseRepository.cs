using StudentManagement.DAL.Models;

namespace StudentManagement.DAL.Repositories.IRepositories;

public interface ICourseRepository
{
    Task<List<Course>> Get();
    Task<Course> Get(int id);
    Task<Course> Create(Course course);
    Task<Course> Update(Course course);
    Task Delete(int id);
    Task<List<Course>> Filter(int? search, string filter, int pageSize, int pageNumber = 1);
}