using Application.Dto.Departments.Requests;
using Application.Dto.Departments.Responses;
using Application.Dto.Employees.Responses;

namespace Application.Interfaces;

public interface IDepartmentService
{
    public Task<GetDepartmentResponse> CreateAsync(CreateDepartmentRequest departmentRequest);
    public Task<List<GetDepartmentResponse>> GetAllAsync();
    public Task<GetDepartmentResponse> GetByIdAsync(int id);
    public Task<List<GetEmployeeWithPassportResponse>> GetEmployeesByDepartmentIdAsync(int id);
}