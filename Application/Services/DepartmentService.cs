using Application.Dto.Departments.Requests;
using Application.Dto.Departments.Responses;
using Application.Dto.Employees.Responses;
using Application.Exceptions.Departments;
using Application.Interfaces;
using Domain.DbModels;
using Domain.Interfaces;
using Mapster;

namespace Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public DepartmentService(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository)
    {
        _departmentRepository = departmentRepository;
        _employeeRepository = employeeRepository;
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

    public async Task<List<GetEmployeeWithPassportResponse>> GetEmployeesByDepartmentIdAsync(int id)
    {
        if (await _departmentRepository.GetByIdAsync(id) is null)
        {
            throw new DepartmentNotFound();
        }
        
        var res = await _employeeRepository.GetEmployeesByDepartmentIdAsync(id);

        if (res.Count == 0)
        {
            throw new DepartmentIsEmpty();
        }
        
        return res.Select(e => e.Adapt<GetEmployeeWithPassportResponse>()).ToList();
    }
}