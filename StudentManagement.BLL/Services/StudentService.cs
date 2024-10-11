using AutoMapper;
using StudentManagement.BLL.Dtos.Greade;
using StudentManagement.BLL.Dtos.Student;
using StudentManagement.BLL.Dtos.StudentCourse;
using StudentManagement.BLL.Services.IServices;
using StudentManagement.DAL.Models;
using StudentManagement.DAL.Repositories.IRepositories;

namespace StudentManagement.BLL.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public StudentService(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<StudentResponseDTO>> Get()
    {
        var students = await _studentRepository.Get();
        var response = MappingStudentResponses(students);
        return response;
    }

    private static List<StudentResponseDTO> MappingStudentResponses(IEnumerable<Student> students)
    {
        var result = new List<StudentResponseDTO>();
        foreach (var student in students)
        {
            var response = MappingStudentToStudentResponse(student);
            result.Add(response);
        }

        return result;
    }

    private static StudentResponseDTO MappingStudentToStudentResponse(Student student)
    {
        var response = new StudentResponseDTO()
        {
            StudentId = student.StudentId,
            FullName = student.FirstName + " " + student.LastName,
            Email = student.Email,
            Gender = student.Gender,
            DateOfBirth = student.DateOfBirth,
            Phone = student.Phone,
            AddressId = student.Address?.AddressId ?? 0,
            AddressDistrict = student.Address?.District,
            AddressCity = student.Address?.City,
            Courses = student.StudentCourses?.Select(course => new StudentCourseDTO()
            {
                CourseId = course.CourseId,
            }).ToList() ?? new List<StudentCourseDTO>(),
            Grades = student.Grades?.Select(grade => new GradeDTO
            {
                CourseId = grade.CourseId,
                Point = grade.Point,
            }).ToList() ?? new List<GradeDTO>(),
        };

        return response;
    }

    public async Task<StudentResponseDTO> Get(int id)
    {
        var student = await _studentRepository.Get(id);
        var response = MappingStudentToStudentResponse(student);
        return response;
    }

    public async Task<Student> Create(StudentUpsertDTO studentUpsertDto)
    {
        var newStudent = _mapper.Map<Student>(studentUpsertDto);
        var createdStudent = await _studentRepository.Create(newStudent);
        return createdStudent;
    }

    public async Task<StudentResponseDTO> Update(int id, StudentUpsertDTO studentUpsertDto)
    {
        var existingStudent = await _studentRepository.Get(id);
        _mapper.Map(studentUpsertDto, existingStudent);
        await _studentRepository.Update(existingStudent);
        return MappingStudentToStudentResponse(existingStudent);
    }

    public async Task Delete(int id)
    {
        await _studentRepository.Delete(id);
    }
    
    
    public async Task<List<StudentResponseDTO>> Filter(string? search, string filter, int pageSize, int pageNumber = 1)
    {
        var students = await _studentRepository.Filter(search, filter, pageSize, pageNumber);
        return students.Select(student => new StudentResponseDTO
        {
            StudentId = student.StudentId,
            FullName = student.FirstName + " " + student.LastName,
            Email = student.Email,
            Gender = student.Gender,
            DateOfBirth = student.DateOfBirth,
            Phone = student.Phone,
            AddressId = student.Address?.AddressId ?? 0,
            AddressDistrict = student.Address?.District,
            AddressCity = student.Address?.City,
            Courses = student.StudentCourses?.Select(course => new StudentCourseDTO()
            {
                CourseId = course.CourseId,
            }).ToList() ?? new List<StudentCourseDTO>(),
            Grades = student.Grades?.Select(grade => new GradeDTO
            {
                CourseId = grade.CourseId,
                Point = grade.Point,
            }).ToList() ?? new List<GradeDTO>(),
        }).ToList();
    }

}