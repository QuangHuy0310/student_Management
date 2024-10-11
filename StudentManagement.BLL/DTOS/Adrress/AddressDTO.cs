using Microsoft.Build.Framework;
namespace StudentManagement.BLL.Dtos.Adrress;

public class AddressDTO
{
    public string? Street { get; set; }
    
    public string? Ward { get; set; }
    
    [Required]
    public string District { get; set; } = null!;

    [Required]
    public string City { get; set; } = null!;

    [Required]
    public int StudentId { get; set; }
}