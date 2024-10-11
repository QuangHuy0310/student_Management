using StudentManagement.DAL.Models;

namespace StudentManagement.DAL.Repositories.IRepositories;

public interface ICourseRegistrationRepository
{
    Task<IEnumerable<StudentCourse>> Get();
    Task<StudentCourse> Get(int id);
    Task<StudentCourse> Create(StudentCourse studentCourse);
    Task<StudentCourse> Update(StudentCourse studentCourse);
    Task Delete(int id);
    
    Task<IEnumerable<StudentCourse?>> Filter(string search, string filter, int pageSize, int pageNumber = 1);
}
