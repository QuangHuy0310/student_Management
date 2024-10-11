namespace StudentManagement.BLL.Dtos.StudentCourse;

public class StudentCourseResponseDTO
{
    public int StudentCourseId { get; set; }
    
    public int StudentId { get; set; }
    
    public string? StudentName { get; set; } 
    
    public int CourseId { get; set; }
    public string? CourseName { get; set; }
}