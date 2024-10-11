using System.ComponentModel.DataAnnotations;

namespace StudentManagement.DAL.Models;

public class StudentCourse
{
    [Key]
    public int StudentCourseId { get; set; }
    
    [Required]
    public int StudentId { get; set; }
    public Student Students { get; set; } = null!;
    
    [Required]
    public int CourseId { get; set; }
    public Course Courses { get; set; } = null!;
}
