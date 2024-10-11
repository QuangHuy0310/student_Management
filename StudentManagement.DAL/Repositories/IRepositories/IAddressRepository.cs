using StudentManagement.DAL.Models;

namespace StudentManagement.DAL.Repositories.IRepositories;

public interface IAddressRepository
{
    Task<IEnumerable<Address>> Get();
    Task<Address> Get(int id);
    Task<Address> Create(Address address);
    Task<Address> Update(Address address);
    Task Delete(int id);
}