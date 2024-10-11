namespace StudentManagement.BLL.Dtos.Greade;

public class GradeResponseDTO
{
    public int GradeId { get; set; }
    public int StudentId { get; set; }
    public string? StudentName { get; set; }
    public int CourseId { get; set; }
    public string? CourseName { get; set; }
    public int Point { get; set; }
}