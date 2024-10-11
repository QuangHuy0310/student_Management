using Microsoft.AspNetCore.Mvc;
using StudentManagement.BLL.Dtos.Adrress;
using StudentManagement.BLL.Services.IServices;

namespace StudentManagement.GUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressesController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressesController(IAddressService addressService)
    {
        _addressService = addressService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
       var addresses = await _addressService.Get();
        return Ok(addresses);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var address = await _addressService.Get(id);
        return Ok(address);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddressDTO addressDto)
    {
        var newAddress = await _addressService.Create(addressDto);
        return CreatedAtAction(nameof(Get), new {id = newAddress.AddressId}, newAddress);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AddressDTO addressDto)
    {
        await _addressService.Update(id, addressDto);
        return NoContent(); 
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _addressService.Delete(id);
        return Ok(new { message = "Address deleted Successfully!" });
    }
}