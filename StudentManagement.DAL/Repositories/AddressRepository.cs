using Microsoft.EntityFrameworkCore;
using StudentManagement.DAL.DbContext;
using StudentManagement.DAL.Models;
using StudentManagement.DAL.Repositories.IRepositories;

namespace StudentManagement.DAL.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly ApplicationDbContext _context;

    public AddressRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Address>> Get()
    {
        return await _context.Addresses.ToListAsync();
    }

    public async Task<Address> Get(int id)
    {
        return await _context.Addresses.FirstOrDefaultAsync(_=>_.AddressId == id);
    }

    public async Task<Address> Create(Address address)
    {
        await _context.Addresses.AddAsync(address);
        await _context.SaveChangesAsync();
        return address;
    }

    public async Task<Address> Update(Address address)
    {
       _context.Addresses.Update(address);
       await _context.SaveChangesAsync();
       return address;
    }

    public async Task Delete(int id)
    {
        var deleteAddress = await Get(id);
        _context.Addresses.Remove(deleteAddress);
        await _context.SaveChangesAsync();
    }
}