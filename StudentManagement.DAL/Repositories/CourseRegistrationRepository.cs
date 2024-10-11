using Microsoft.EntityFrameworkCore;
using StudentManagement.DAL.DbContext;
using StudentManagement.DAL.Models;
using StudentManagement.DAL.Repositories.IRepositories;

namespace StudentManagement.DAL.Repositories;

public class CourseRegistrationRepository : ICourseRegistrationRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRegistrationRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<StudentCourse>> Get()
    {
        return await _context.StudentCourses.Include(_=>_.Students)
                                            .Include(_=>_.Courses)
                                            .ToListAsync();
    }

    public async Task<StudentCourse> Get(int id)
    {
        return await _context.StudentCourses.Include(_=>_.Students)
                                            .Include(_=>_.Courses)
                                            .FirstOrDefaultAsync(_=>_.StudentCourseId == id);
    }

    public async Task<StudentCourse> Create(StudentCourse studentCourse)
    {
        var student = await _context.Students.FirstOrDefaultAsync(_ => _.StudentId == studentCourse.StudentId);
        var course = await _context.Courses.FirstOrDefaultAsync(_ => _.CourseId == studentCourse.CourseId);
        if (student == null || course == null)
            throw new AppException.AppException("Student or Course not found");

        var isStudentJoinCourse =
            await _context.StudentCourses.AnyAsync(_ =>
                _.StudentId == student.StudentId && _.CourseId == course.CourseId);
        if (isStudentJoinCourse)
            throw new AppException.AppException($"Student with ID {student.StudentId} is already enrolled in Course with ID {course.CourseId}");

        var newStudentCourse = new StudentCourse()
        {
            StudentId = student.StudentId,
            CourseId = course.CourseId
        };
        
        await _context.StudentCourses.AddAsync(newStudentCourse);
        await _context.SaveChangesAsync();
        return newStudentCourse;
    }

    public async Task<StudentCourse> Update(StudentCourse studentCourse)
    {
        // Kiểm tra xem sinh viên và khóa học đã tồn tại và có đăng ký khóa học chưa
        var student = await _context.Students.FirstOrDefaultAsync(_ => _.StudentId == studentCourse.StudentId);
        var course = await _context.Courses.FirstOrDefaultAsync(_ => _.CourseId == studentCourse.CourseId);
    
        if (student == null || course == null)
        {
            throw new AppException.AppException("Student or Course not found");
        }

        // Lấy StudentCourse cũ từ cơ sở dữ liệu
        var existingStudentCourse = await _context.StudentCourses.FirstOrDefaultAsync(_ => _.StudentCourseId == studentCourse.StudentCourseId);

        if (existingStudentCourse == null)
        {
            throw new AppException.AppException("StudentCourse not found");
        }

        // Kiểm tra xem khóa học mới trùng với khóa học đã đăng ký hay không
        var isStudentJoinCourse = await _context.StudentCourses
            .AnyAsync(_ => _.StudentId == student.StudentId && _.CourseId == course.CourseId && _.StudentCourseId != studentCourse.StudentCourseId);

        if (isStudentJoinCourse)
        {
            throw new AppException.AppException($"Student with ID {student.StudentId} is already enrolled in Course with ID {course.CourseId}");
        }

        // Kiểm tra xem có sự thay đổi trong thông tin đăng ký hay không
        if (existingStudentCourse.StudentId != studentCourse.StudentId || existingStudentCourse.CourseId != studentCourse.CourseId)
        {
            existingStudentCourse.StudentId = studentCourse.StudentId;
            existingStudentCourse.CourseId = studentCourse.CourseId;
        }

        _context.StudentCourses.Update(existingStudentCourse);
        await _context.SaveChangesAsync();
        return existingStudentCourse;
    }

    public async Task Delete(int id)
    {
        var deleteStudentCourse = await Get(id);
        _context.StudentCourses.Remove(deleteStudentCourse);
        await _context.SaveChangesAsync();
    }
    
    
    public async Task<IEnumerable<StudentCourse?>> Filter(string search, string filter, int pageSize, int pageNumber = 1)
    {
        var getData = _context.StudentCourses
            .Include(sc => sc.Courses) // Load Courses for each StudentCourse
            .AsQueryable();
        
        search = search.ToUpper();
        getData = getData.Where(x => x.Courses.CourseName.ToUpper().Contains(search));
        

        if (!string.IsNullOrEmpty(filter))
        {
            switch (filter)
            {
                case "desc":
                    getData = getData.OrderByDescending(x => x.StudentCourseId);
                    break;
                case "asc":
                    getData = getData.OrderBy(x => x.StudentCourseId);
                    break;
                default:
                    getData = getData.OrderByDescending(x => x.StudentCourseId);
                    break;
            }
        }

        getData = getData.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return await getData.ToListAsync();
    }

}