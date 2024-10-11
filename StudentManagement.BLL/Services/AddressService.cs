using AutoMapper;
using StudentManagement.BLL.Dtos.Adrress;
using StudentManagement.BLL.Services.IServices;
using StudentManagement.DAL.Models;
using StudentManagement.DAL.Repositories.IRepositories;

namespace StudentManagement.BLL.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public AddressService(IAddressRepository addressRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Address>> Get()
    {
        return await _addressRepository.Get();
    }

    public async Task<Address> Get(int id)
    {
        return await _addressRepository.Get(id);
    }

    public async Task<Address> Create(AddressDTO addressDto)
    {
        var newAddress = _mapper.Map<Address>(addressDto);
        await _addressRepository.Create(newAddress);
        return newAddress;
    }

    public async Task<Address> Update(int id, AddressDTO addressDto)
    {
        var existingAddress = await _addressRepository.Get(id);
        _mapper.Map(addressDto, existingAddress);
        await _addressRepository.Update(existingAddress);
        return existingAddress;
    }

    public async Task Delete(int id)
    {
        await _addressRepository.Delete(id);
    }
}