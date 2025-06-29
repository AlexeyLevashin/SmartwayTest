using Application.Dto.Department.Requests;
using Application.Dto.Department.Responses;
using Application.Exceptions.Departments;
using Application.Interfaces;
using Domain.DbModels;
using Domain.Interfaces;
using Mapster;

namespace Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<GetDepartmentResponse> CreateAsync(CreateDepartmentRequest department)
    {
        if (await _departmentRepository.GetByPhoneAsync(department.Phone) is not null)
        {
            throw new DepartmentPhoneIsExist();
        }
        
        return (await _departmentRepository.CreateAsync(department.Adapt<DbDepartment>())).Adapt<GetDepartmentResponse>();
    }

    public async Task<List<GetDepartmentResponse>> GetAllAsync()
    {
        var res = await _departmentRepository.GetAllAsync();

        return res.Select(d => d.Adapt<GetDepartmentResponse>()).ToList();
    }

    public async Task<GetDepartmentResponse> GetByIdAsync(int id)
    {
        var res = await _departmentRepository.GetByIdAsync(id);
        if (res is null)
        {
            throw new DepartmentNotFound();
        }

        return res.Adapt<GetDepartmentResponse>();
    }
}