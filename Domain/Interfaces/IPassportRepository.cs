using Domain.DbModels;

namespace Domain.Interfaces;

public interface IPassportRepository
{
    public Task<int> CreateAsync(DbPassport dbPassport);
    public Task<DbPassport> UpdateByEmployeeIdAsync(DbPassport dbPassport);
    public Task<DbPassport?> GetByEmployeeIdAsync(int employeeId);
    public Task<bool> IsPassportExistByNumber(string number);
}