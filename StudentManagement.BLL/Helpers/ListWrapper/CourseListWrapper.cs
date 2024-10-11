using System.Runtime.Serialization;
using StudentManagement.DAL.Models;
namespace StudentManagement.BLL.Helpers.ListWrapper;

[DataContract(Name = "Courses", Namespace = "http://schemas.datacontract.org/2004/07/StudentManagement.DAL.Models")]
public class CourseListWrapper
{
    [DataMember]
    public List<Course> Courses { get; set; }
}



