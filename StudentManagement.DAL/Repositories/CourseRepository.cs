using Microsoft.EntityFrameworkCore;
using StudentManagement.DAL.DbContext;
using StudentManagement.DAL.Models;
using StudentManagement.DAL.Repositories.IRepositories;

namespace StudentManagement.DAL.Repositories;

public class CourseRepository: ICourseRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Course>> Get()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<Course> Get(int id)
    {
        return await _context.Courses.FirstOrDefaultAsync(_=>_.CourseId == id);
    }

    public async Task<Course> Create(Course course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public async Task<Course> Update(Course course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public async Task Delete(int id)
    {
        var deleteCourse = await Get(id);
        _context.Remove(deleteCourse);
        await _context.SaveChangesAsync();
    }
    
   
    public async Task<List<Course>> Filter(int? search, string filter, int pageSize, int pageNumber = 1)
    {
        var getData = _context.Courses.AsQueryable();

        if (search.HasValue)
        {
            getData = getData.Where(x => x.Credits.ToString().Contains(search.Value.ToString()));
        }

        if (!string.IsNullOrEmpty(filter))
        {
            switch (filter)
            {
                case "desc":
                    getData = getData.OrderByDescending(x => x.CourseId);
                    break;
                case "asc":
                    getData = getData.OrderBy(x => x.CourseId);
                    break;
                default:
                    getData = getData.OrderByDescending(x => x.CourseId);
                    break;
            }
        }

        getData = getData.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        var result = await getData.Select(x => new Course
        {
            CourseId = x.CourseId,
            CourseName = x.CourseName,
            Credits = x.Credits
        }).ToListAsync();

        return result;
    }
}