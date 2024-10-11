using System.Text.Json;
using System.Text.Json.Serialization;
using StudentManagement.BLL.Extensions;
using StudentManagement.BLL.Helpers.AppSettings;
using StudentManagement.BLL.Helpers.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure the application's configuration settings
builder.Configuration.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
builder.Configuration.AddJsonFile("appsettings.json", false, true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);
builder.Configuration.AddEnvironmentVariables();
// Map AppSettings section in appsettings.json file value to AppSetting model
builder.Configuration.GetSection("AppSettings").Get<AppSettings>(options => options.BindNonPublicProperties = true);

builder.Services.AddControllers(options =>
    {
        options.ReturnHttpNotAcceptable = true; 
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    })
    .AddXmlDataContractSerializerFormatters();

builder.Services
    .AddDatabase()
    .AddAutoMapper()
    .AddService()
    .AddEndpointsApiExplorer()
    .AddVersioning()
    .AddSwagger();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();