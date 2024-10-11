using StudentManagement.BLL.Dtos.Greade;
using StudentManagement.DAL.Models;

namespace StudentManagement.BLL.Services.IServices;

public interface IGradeService
{
    Task<IEnumerable<GradeResponseDTO>> Get();
    Task<GradeResponseDTO> Get(int id);
    Task<GradeResponseDTO> Create(GradeUpsertDTO gradeUpsertDto);
    Task<GradeResponseDTO> Update(int id, GradeUpsertDTO gradeUpsertDto);
    Task Delete(int id);
    
    Task<List<GradeResponseDTO>> Filter(int search, string filter, int pageSize, int pageNumber = 1);
}