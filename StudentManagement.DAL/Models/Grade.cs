using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.DAL.Models;

public class Grade
{
    [Key]
    public int GradeId { get; set; }
    
    [Required]
    public int StudentId { get; set; }
    public Student Students { get; set; } = null!;
    
    [Required]
    public int CourseId { get; set; }
    public Course Courses { get; set; } = null!;
    
    [Required]
    public int Point { get; set; }
    
}

