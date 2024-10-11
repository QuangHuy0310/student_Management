using System.ComponentModel.DataAnnotations;

namespace StudentManagement.BLL.Dtos.Student;

public class StudentUpsertDTO
{
    [Required]
    public string FirstName { get; set; } = null!;
    
    [Required]
    public string LastName { get; set; } = null!;
    
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    public string? Gender { get; set; } 
    
    public DateTime? DateOfBirth { get; set; }
    
    public string? Phone { get; set; }
}