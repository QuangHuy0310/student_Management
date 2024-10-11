using AutoMapper;
using StudentManagement.BLL.Dtos.Adrress;
using StudentManagement.BLL.Dtos.Course;
using StudentManagement.BLL.Dtos.Greade;
using StudentManagement.BLL.Dtos.Student;
using StudentManagement.BLL.Dtos.StudentCourse;
using StudentManagement.DAL.Models;

namespace StudentManagement.BLL.Helpers.AutoMapper;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            //Mapping Student
            config.CreateMap<StudentUpsertDTO, Student>().ReverseMap();

            //Mapping Address
            config.CreateMap<AddressDTO, Address>().ReverseMap();
            
            //Mapping Course
            config.CreateMap<CourseDTO, Course>().ReverseMap();
            
            //Mapping Grades
            config.CreateMap<GradeDTO, Grade>().ReverseMap();
            config.CreateMap<GradeUpsertDTO, Grade>().ReverseMap();
            
            //Mapping AccountCourse
            config.CreateMap<StudentCourseDTO, StudentCourse>().ReverseMap();
            config.CreateMap<StudentCourseUpsertDTO, StudentCourse>().ReverseMap();
        });
        return mappingConfig;
    }
}