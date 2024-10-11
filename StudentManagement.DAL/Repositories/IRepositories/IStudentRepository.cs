using StudentManagement.DAL.Models;

namespace StudentManagement.DAL.Repositories.IRepositories;

public interface IStudentRepository
{
  
    Task<IEnumerable<Student>> Get();
    Task<Student> Get(int id);
    Task<Student> Create(Student student);
    Task<Student> Update(Student student);
    Task Delete(int id);
    Task<List<Student>> Filter(string? search, string filter, int pageSize, int pageNumber = 1);
}