using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DAL.DbContext;
using StudentManagement.DAL.Models;
using StudentManagement.DAL.Repositories.IRepositories;

namespace StudentManagement.DAL.Repositories;

public class GradeRepository : IGradeRepository
{
    private readonly ApplicationDbContext _context;

    public GradeRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Grade>> Get()
    {
        return await _context.Grades.Include(_=>_.Students)
                                    .Include(_=>_.Courses)
                                    .ToListAsync();
    }

    public async Task<Grade> Get(int id)
    {
        return await _context.Grades.Include(_ => _.Students)
                                    .Include(_ => _.Courses)
                                    .FirstOrDefaultAsync(_=>_.GradeId == id);
    }
    public async Task<Grade> Create(Grade grade)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(_ => _.CourseId == grade.CourseId);
        var student = await _context.Students.FirstOrDefaultAsync(_ => _.StudentId == grade.StudentId);
        if (course == null || student == null)
            throw new AppException.AppException("Course or accounts not found");

        var isStudentJoinCourse =
            await _context.Grades.AnyAsync(_ => _.CourseId == course.CourseId && _.StudentId == student.StudentId);
        if (isStudentJoinCourse)
            throw new AppException.AppException("CourseId " + grade.CourseId + " is already join " + "StudentId" + grade.StudentId);
        
        var isEnrolled = await _context.StudentCourses
            .AnyAsync(sc => sc.StudentId == student.StudentId && sc.CourseId == course.CourseId);
        if (!isEnrolled)
            throw new AppException.AppException("Student has not enrolled in this course");

        var newGrade = new Grade()
        {
            StudentId = student.StudentId,
            CourseId = course.CourseId,
            Point = grade.Point
        };
        
        await _context.Grades.AddAsync(newGrade);
        await _context.SaveChangesAsync();
        return newGrade;
    }

    public async Task<Grade> Update(Grade grade)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(_ => _.CourseId == grade.CourseId);
        var student = await _context.Students.FirstOrDefaultAsync(_ => _.StudentId == grade.StudentId);

        if (course == null || student == null)
            throw new AppException.AppException("Course or accounts not found");

        var existingGrade = await _context.Grades.FindAsync(grade.GradeId);

        if (existingGrade == null)
            throw new AppException.AppException("Grade Not Found");

        var isEnrolled = await _context.StudentCourses
            .AnyAsync(sc => sc.StudentId == student.StudentId && sc.CourseId == course.CourseId);
        if (!isEnrolled)
            throw new AppException.AppException("Student has not enrolled in this course");

        var isStudentJoinCourse = await _context.Grades
            .AnyAsync(g => g.CourseId == grade.CourseId && g.StudentId == grade.StudentId && g.GradeId != grade.GradeId);
        if (isStudentJoinCourse)
            throw new AppException.AppException("CourseId " + grade.CourseId + " is already join " + "StudentId" + grade.StudentId);

        existingGrade.StudentId = grade.StudentId;
        existingGrade.CourseId = grade.CourseId;
        existingGrade.Point = grade.Point;

        _context.Update(existingGrade);
        await _context.SaveChangesAsync();
        return existingGrade;
    }

    public async Task Delete(int id)
    {
        var deleteGrade = await Get(id);
        _context.Grades.Remove(deleteGrade);
        await _context.SaveChangesAsync();
    }
    
    
    public async Task<List<Grade>> Filter(int search, string filter, int pageSize, int pageNumber = 1)
    {
        var getData = _context.Grades.Include(g => g.Students).AsQueryable();

        getData = getData.Where(x => x.Point == search);

        if (!string.IsNullOrEmpty(filter))
        {
            switch (filter)
            {
                case "desc":
                    getData = getData.OrderByDescending(x => x.Students.LastName);
                    break;
                case "asc":
                    getData = getData.OrderBy(x => x.Students.LastName);
                    break;
                default:
                    getData = getData.OrderByDescending(x => x.Students.LastName);
                    break;
            }
        }

        getData = getData.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return await getData.ToListAsync();
    }

}