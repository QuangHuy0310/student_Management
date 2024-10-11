using AutoMapper;
using StudentManagement.BLL.Dtos.Course;
using StudentManagement.BLL.Services.IServices;
using StudentManagement.DAL.DbContext;
using StudentManagement.DAL.Models;
using StudentManagement.DAL.Repositories.IRepositories;

namespace StudentManagement.BLL.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public CourseService(ICourseRepository courseRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Course>> Get()
    {
        return await _courseRepository.Get();
    }

    public async Task<Course> Get(int id)
    {
        return await _courseRepository.Get(id);
    }

    public async Task<Course> Create(CourseDTO courseDto)
    {
        var course = _mapper.Map<Course>(courseDto);
        await _courseRepository.Create(course);
        return course;
    }

    public async Task<Course> Update(int id, CourseDTO courseDto)
    {
        var existingCourse = await Get(id);
        _mapper.Map(courseDto, existingCourse);
        await _courseRepository.Update(existingCourse);
        return existingCourse;
    }

    public async Task Delete(int id)
    {
        await _courseRepository.Delete(id);
        
    }
    
    
    public async Task<List<Course>> Filter(int search, string filter, int pageSize, int pageNumber = 1)
    {
        var courses = await _courseRepository.Filter(search, filter, pageSize, pageNumber);
        return courses.Select(x => new Course()
        {
            CourseId = x.CourseId,
            CourseName = x.CourseName,
            Credits = x.Credits
        }).ToList();
    }
}