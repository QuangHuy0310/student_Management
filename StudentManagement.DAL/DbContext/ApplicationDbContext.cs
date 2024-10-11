using Microsoft.EntityFrameworkCore;
using StudentManagement.DAL.Models;
namespace StudentManagement.DAL.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {}

    // DbSet
    public DbSet<Student> Students { get; set; } 

    public DbSet<Course> Courses { get; set; } 

    public DbSet<Grade> Grades { get; set; } 

    public DbSet<Address> Addresses { get; set; } 

    public DbSet<StudentCourse> StudentCourses { get; set; }
}
