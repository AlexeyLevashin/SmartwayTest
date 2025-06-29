using Application.Dto.Department.Requests;
using Application.Dto.Department.Responses;

namespace Application.Interfaces;

public interface IDepartmentService
{
    public Task<GetDepartmentResponse> CreateAsync(CreateDepartmentRequest departmentRequest);
    public Task<List<GetDepartmentResponse>> GetAllAsync();
    public Task<GetDepartmentResponse> GetByIdAsync(int id);
}