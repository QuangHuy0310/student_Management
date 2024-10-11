using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.DAL.Models;

public class Address
{
    [Key]
    public int AddressId { get; set; }
    
    public string? Street { get; set; }
    
    public string? Ward { get; set; }
    
    [Required(ErrorMessage = "The District field is required.")]
    public string District { get; set; } = null!;

    [Required(ErrorMessage = "The City field is required.")]
    public string City { get; set; } = null!;
   
    [Required]
    public int StudentId { get; set; }
    [ForeignKey("StudentId")] 
    public Student Students { get; set; } = null!;
}

