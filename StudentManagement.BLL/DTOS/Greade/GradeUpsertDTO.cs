using Microsoft.Build.Framework;

namespace StudentManagement.BLL.Dtos.Greade;

public class GradeUpsertDTO
{
    [Required]
    public int StudentId { get; set; }
    
    [Required]
    public int CourseId { get; set; }
    
    [Required]
    public int Point { get; set; }
}