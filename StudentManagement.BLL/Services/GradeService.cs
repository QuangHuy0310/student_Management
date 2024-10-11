using System.Diagnostics;
using AutoMapper;
using StudentManagement.BLL.Dtos.Greade;
using StudentManagement.BLL.Services.IServices;
using StudentManagement.DAL.Models;
using StudentManagement.DAL.Repositories.IRepositories;

namespace StudentManagement.BLL.Services;

public class GradeService : IGradeService
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IMapper _mapper;

    public GradeService(IGradeRepository gradeRepository, IMapper mapper)
    {
        _gradeRepository = gradeRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<GradeResponseDTO>> Get()
    {
        var grades = await _gradeRepository.Get();
        var responses = MappingGradeResponses(grades);

        return responses;
    }

    private static List<GradeResponseDTO> MappingGradeResponses(IEnumerable<Grade?> grades)
    {
        var result = new List<GradeResponseDTO>();
        foreach (var grade in grades)
        {
            var response = MappingGradeToGradeResponse(grade);
            result.Add(response);
        }

        return result;
    }
    private static GradeResponseDTO MappingGradeToGradeResponse(Grade grade)
    {
        var response = new GradeResponseDTO()
        {
            GradeId = grade.GradeId,
            StudentId = grade.StudentId,
            StudentName =  $"{grade.Students?.FirstName} {grade.Students?.LastName}",
            CourseId = grade.CourseId,
            CourseName = grade.Courses?.CourseName,
            Point = grade.Point
        };
        return response;
    }

    public async Task<GradeResponseDTO> Get(int id)
    {
        var grade = await _gradeRepository.Get(id);
        var response = MappingGradeToGradeResponse(grade);
        return response;
    }

    public async Task<GradeResponseDTO> Create(GradeUpsertDTO gradeUpsertDto)
    {
        var mappingGrade = _mapper.Map<Grade>(gradeUpsertDto);
        var newGrade = await _gradeRepository.Create(mappingGrade);
        var response = MappingGradeToGradeResponse(newGrade);
        return response;
    }

    public async Task<GradeResponseDTO> Update(int id, GradeUpsertDTO gradeUpsertDto)
    {
        var existingGrade = await _gradeRepository.Get(id);
        _mapper.Map(gradeUpsertDto, existingGrade);
        await _gradeRepository.Update(existingGrade);
        return MappingGradeToGradeResponse(existingGrade);
    }
    
    
    public async Task Delete(int id)
    {
        await _gradeRepository.Delete(id);
    }
    
    
    public async Task<List<GradeResponseDTO>> Filter(int search, string filter, int pageSize, int pageNumber = 1)
    {
        var grades = await _gradeRepository.Filter(search, filter, pageSize, pageNumber);
        return grades.Select(x => new GradeResponseDTO
        {
            GradeId = x.GradeId,
            StudentId = x.StudentId,
            StudentName = $"{x.Students?.FirstName} {x.Students?.LastName}",
            CourseId = x.CourseId,
            CourseName = x.Courses?.CourseName,
            Point = x.Point
        }).ToList();
    }
}