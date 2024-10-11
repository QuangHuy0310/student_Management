using StudentManagement.BLL.Dtos.Student;
using StudentManagement.DAL.Models;

namespace StudentManagement.BLL.Services.IServices;

public interface IStudentService
{
    Task<IEnumerable<StudentResponseDTO>> Get();
    Task<StudentResponseDTO> Get(int id);
    Task<Student> Create(StudentUpsertDTO studentUpsertDto);
    Task<StudentResponseDTO> Update(int id, StudentUpsertDTO studentUpsertDto);
    Task Delete(int id);
    Task<List<StudentResponseDTO>> Filter(string? search, string filter, int pageSize, int pageNumber = 1);
}