using Domain.DbModels;

namespace Domain.Interfaces;

public interface IDepartmentRepository
{
    public Task<DbDepartment> CreateAsync(DbDepartment dbDepartment);
    public Task<List<DbDepartment>> GetAllAsync();
    public Task<DbDepartment?> GetByIdAsync(int id);
    public Task<DbDepartment?> GetByPhoneAsync(string phone);
}