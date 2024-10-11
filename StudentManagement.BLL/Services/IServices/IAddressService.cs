using StudentManagement.BLL.Dtos.Adrress;
using StudentManagement.DAL.Models;

namespace StudentManagement.BLL.Services.IServices;

public interface IAddressService
{
    Task<IEnumerable<Address>> Get();
    Task<Address> Get(int id);
    Task<Address> Create(AddressDTO addressDto);
    Task<Address> Update(int id, AddressDTO addressDto);
    Task Delete(int id);
}