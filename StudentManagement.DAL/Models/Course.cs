using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace StudentManagement.DAL.Models;

[DataContract]
public class Course
{
    [Key]
    [DataMember]
    public int CourseId { get; set; }
    
    [Required]
    [DataMember]
    public string CourseName { get; set; } = null!;
    
    [DataMember]
    public int Credits { get; set; }
    
    public List<StudentCourse> CourseStudents { get; set; } = null!;
}