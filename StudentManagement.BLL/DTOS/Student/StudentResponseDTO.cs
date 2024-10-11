using StudentManagement.BLL.Dtos.Greade;
using StudentManagement.BLL.Dtos.StudentCourse;

namespace StudentManagement.BLL.Dtos.Student;

public class StudentResponseDTO
{
    public int StudentId { get; set; }

    public string FullName { get; set; } = null!;
    
    public string Email { get; set; } = null!;

    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Phone { get; set; }

    public int? AddressId { get; set; }

    public string? AddressCity { get; set; }

    public string? AddressDistrict { get; set; }

    public List<StudentCourseDTO> Courses { get; set; } = null!;

    public List<GradeDTO> Grades { get; set; } = null!;
}