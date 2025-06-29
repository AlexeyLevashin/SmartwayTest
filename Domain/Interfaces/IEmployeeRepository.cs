using Domain.DbModels;

namespace Domain.Interfaces;

public interface IEmployeeRepository
{
    public Task<int> CreateAsync(DbEmployee employee);
    public Task<DbEmployee> UpdateAsync(DbEmployee dbEmployee);
    public Task DeleteAsync(int id);
    public Task<DbEmployee?> GetByIdAsync(int id);
    public Task<DbEmployee?> GetByPhoneAsync(string phone);
    public Task<List<DbEmployeeDetails>> GetEmployeesByCompanyIdAsync(int companyId);
}