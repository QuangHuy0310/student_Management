using Microsoft.Build.Framework;

namespace StudentManagement.BLL.Dtos.StudentCourse;

public class StudentCourseUpsertDTO
{
    [Required]
    public int StudentId { get; set; }
    
    [Required]
    public int CourseId { get; set; }
}