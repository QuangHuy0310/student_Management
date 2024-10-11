using StudentManagement.DAL.Models;

namespace StudentManagement.DAL.Repositories.IRepositories;

public interface IGradeRepository
{
    Task<IEnumerable<Grade>> Get();
    Task<Grade> Get(int id);
    Task<Grade> Create(Grade grade);
    Task<Grade> Update(Grade grade);
    Task Delete(int id);
    Task<List<Grade>> Filter(int search, string filter, int pageSize, int pageNumber = 1);
}