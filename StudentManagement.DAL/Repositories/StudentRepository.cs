using Microsoft.EntityFrameworkCore;
using StudentManagement.DAL.DbContext;
using StudentManagement.DAL.Models;
using StudentManagement.DAL.Repositories.IRepositories;

namespace StudentManagement.DAL.Repositories;

public class StudentRepository: IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Student>> Get()
    {

        return await _context.Students.Include(_=>_.Address).Include(_=>_.Grades).Include(_=>_.StudentCourses).ToListAsync();
    }

    public async Task<Student> Get(int id)
    {
        return await _context.Students.Include(_=>_.Address).Include(_=>_.Grades).Include(_=>_.StudentCourses).FirstOrDefaultAsync(_=>_.StudentId == id);
    }

    public async Task<Student> Create(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<Student> Update(Student student)
    {
         _context.Update(student);
         await _context.SaveChangesAsync();
         return student;
    }

    public async Task Delete(int id)
    {
        var deletesStudent = await Get(id);
        _context.Students.Remove(deletesStudent);
        await _context.SaveChangesAsync();
    }
    
    
    public async Task<List<Student>> Filter(string? search, string filter, int pageSize, int pageNumber = 1)
    {
        var getData = _context.Students
            .Include(s => s.StudentCourses) 
            .Include(s => s.Grades) 
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            getData = getData.Where(x => x.LastName.Contains(search));
        }

        if (!string.IsNullOrEmpty(filter))
        {
            switch (filter)
            {
                case "desc":
                    getData = getData.OrderByDescending(x => x.StudentId);
                    break;
                case "asc":
                    getData = getData.OrderBy(x => x.StudentId);
                    break;
                default:
                    getData = getData.OrderByDescending(x => x.StudentId);
                    break;
            }
        }

        getData = getData.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return await getData.ToListAsync();
    }

}