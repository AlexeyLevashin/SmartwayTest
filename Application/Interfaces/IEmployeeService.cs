using Application.Dto.Employees.Requests;
using Application.Dto.Employees.Responses;

namespace Application.Interfaces;

public interface IEmployeeService
{
    public Task<CreateEmployeeResponse> CreateAsync(CreateEmployeeRequest createEmployeeRequest);
    public Task DeleteAsync(int id);
    public Task<GetEmployeeResponse> UpdateAsync(UpdateEmployeeRequest updateEmployeeRequest, int id);
    public Task<List<GetEmployeeResponse>> GetCompanyEmployeesAsync(int companyId);
}