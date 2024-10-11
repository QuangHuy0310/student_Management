using AutoMapper;
using StudentManagement.BLL.Dtos.StudentCourse;
using StudentManagement.BLL.Services.IServices;
using StudentManagement.DAL.Models;
using StudentManagement.DAL.Repositories.IRepositories;

namespace StudentManagement.BLL.Services;

public class CourseRegistrationService : ICourseRegistrationService
{
    private readonly ICourseRegistrationRepository _courseRegistrationRepository;
    private readonly IMapper _mapper;

    public CourseRegistrationService(ICourseRegistrationRepository courseRegistrationRepository, IMapper mapper)
    {
        _courseRegistrationRepository = courseRegistrationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StudentCourseResponseDTO>> Get()
    {
        var studentCourses = await _courseRegistrationRepository.Get();
        var response = MappingStudentCourseResponses(studentCourses);
        return response;
    }

    private static IEnumerable<StudentCourseResponseDTO> MappingStudentCourseResponses(IEnumerable<StudentCourse?> studentCourses)
    {
        var result = new List<StudentCourseResponseDTO>();
        foreach (var studentCourse in studentCourses)
        {
            var response = MappingStudentCourseToStudentCourseResponse(studentCourse);
            result.Add(response);
        }

        return result;
    }

    private static StudentCourseResponseDTO MappingStudentCourseToStudentCourseResponse(StudentCourse? studentCourse)
    {
        return new StudentCourseResponseDTO()
        {
            StudentCourseId = studentCourse!.StudentCourseId,
            StudentId = studentCourse.StudentId,
            StudentName = studentCourse.Students?.FirstName,
            CourseId = studentCourse.CourseId,
            CourseName = studentCourse.Courses?.CourseName
        };
    }

    public async Task<StudentCourseResponseDTO> Get(int id)
    {
        var studentCourse = await _courseRegistrationRepository.Get(id);
        var response = MappingStudentCourseToStudentCourseResponse(studentCourse);
        return response;
    }

    public async Task<StudentCourseResponseDTO> Create(StudentCourseUpsertDTO studentCourseUpsertDto)
    {
        var studentCourse = _mapper.Map<StudentCourse>(studentCourseUpsertDto);
        var newStudentCourse = await _courseRegistrationRepository.Create(studentCourse);
        var response = MappingStudentCourseToStudentCourseResponse(newStudentCourse);
        return response;
    }

    public async Task<StudentCourseResponseDTO> Update(int id, StudentCourseUpsertDTO studentCourseUpsertDto)
    {
        var existingStudentCourse = await _courseRegistrationRepository.Get(id);
        _mapper.Map(studentCourseUpsertDto, existingStudentCourse);
        await _courseRegistrationRepository.Update(existingStudentCourse);
        return MappingStudentCourseToStudentCourseResponse(existingStudentCourse);
    }

    public async Task Delete(int id)
    {
        await _courseRegistrationRepository.Delete(id);
    }
 
    
    public async Task<IEnumerable<StudentCourseResponseDTO>> Filter(string search, string filter, int pageSize, int pageNumber = 1)
    {
        var studentCourses = await _courseRegistrationRepository.Filter(search, filter, pageSize, pageNumber );
        return MappingStudentCourseResponses(studentCourses);
    }
}