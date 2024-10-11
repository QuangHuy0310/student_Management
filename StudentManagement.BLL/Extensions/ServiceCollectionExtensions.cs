using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using StudentManagement.BLL.Helpers.AppSettings;
using StudentManagement.BLL.Helpers.AutoMapper;
using StudentManagement.BLL.Services;
using StudentManagement.BLL.Services.IServices;
using StudentManagement.DAL.DbContext;
using StudentManagement.DAL.Repositories;
using StudentManagement.DAL.Repositories.IRepositories;

namespace StudentManagement.BLL.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(AppSettings.ConnectionStrings, b => b.MigrationsAssembly("StudentManagement.API"));
        });
        return services;
    }

    public static IServiceCollection AddService(this IServiceCollection services)
    {
        //services.AddScoped<>();
        // Accounts
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        
        //Address
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        
        
        //Course
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        
        //Grade
        services.AddScoped<IGradeService, GradeService>();
        services.AddScoped<IGradeRepository, GradeRepository>();
        
        //StudentCourse
        services.AddScoped<ICourseRegistrationService, CourseRegistrationService>();
        services.AddScoped<ICourseRegistrationRepository, CourseRegistrationRepository>();
        
        return services;
    }

    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            // Specify the default API version
            options.DefaultApiVersion = new ApiVersion(1, 2);
            
            options.AssumeDefaultVersionWhenUnspecified = true;
            
            options.ReportApiVersions = true;
            
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
        
        return services;
    }
    
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        //auto mapper config
        var mapper = MappingConfig.RegisterMaps().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
    
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Student Management Web API", Version = "v1" });
            
            /*
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description =
                    "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                    "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                    "Example: \"Bearer 12345abcdef\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
            */
        });
            
        return services;
    }
}