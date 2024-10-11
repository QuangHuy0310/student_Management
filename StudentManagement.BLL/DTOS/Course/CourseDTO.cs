using Microsoft.Build.Framework;
using StudentManagement.DAL.Models;

namespace StudentManagement.BLL.Dtos.Course;

public class CourseDTO
{
    [Required]
    public string CourseName { get; set; } = null!;
    
    public int Credits { get; set; }
}