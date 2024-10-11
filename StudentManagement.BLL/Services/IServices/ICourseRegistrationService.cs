using StudentManagement.BLL.Dtos.StudentCourse;

namespace StudentManagement.BLL.Services.IServices;

public interface ICourseRegistrationService
{
    Task<IEnumerable<StudentCourseResponseDTO>> Get();
    Task<StudentCourseResponseDTO> Get(int id);
    Task<StudentCourseResponseDTO> Create(StudentCourseUpsertDTO studentCourseUpsertDto);
    Task<StudentCourseResponseDTO> Update(int id, StudentCourseUpsertDTO studentCourseUpsertDto);
    Task Delete(int id);
    Task<IEnumerable<StudentCourseResponseDTO>> Filter(string search, string filter, int pageSize, int pageNumber = 1);
}