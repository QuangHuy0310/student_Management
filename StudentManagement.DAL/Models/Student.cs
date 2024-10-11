using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace StudentManagement.DAL.Models;

public class Student
{
    [Key]
    public int StudentId { get; set; }

    [Required] 
    public string FirstName { get; set; } = null!;
    
    [Required]
    public string LastName { get; set; } = null!;
    
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    public string? Gender { get; set; }
    
    public DateTime? DateOfBirth { get; set; }
    
    public string? Phone { get; set; }
    
    public Address? Address { get; set; } 
    
    public List<Grade> Grades { get; set; } = null!;
    
    public List<StudentCourse> StudentCourses { get; set; } = null!;
}