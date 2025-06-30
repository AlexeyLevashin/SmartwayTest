using DataAccess.Dapper;
using DataAccess.Dapper.Interfaces;
using Domain.DbModels;
using Domain.Interfaces;
using Infrastructure.Repositories.Scripts.Passports;

namespace Infrastructure.Repositories;

public class PassportRepository : IPassportRepository
{
    private readonly IDapperContext _dapperContext;

    public PassportRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<int> CreateAsync(DbPassport dbPassport)
    {
        var queryObject = new QueryObject(PostgresPassportElement.CreatePassport, new
        {
            type = dbPassport.Type,
            number = dbPassport.Number,
            employeeId = dbPassport.EmployeeId
        });

        return await _dapperContext.CommandWithResponse<int>(queryObject);
    }

    public async Task<DbPassport> UpdateByEmployeeIdAsync(DbPassport dbPassport)
    {
        var queryObject = new QueryObject(PostgresPassportElement.UpdatePassport, new
        {
            type = dbPassport.Type,
            number = dbPassport.Number,
            employeeId = dbPassport.EmployeeId
        });
        
        return await _dapperContext.CommandWithResponse<DbPassport>(queryObject);
    }

    public async Task<DbPassport?> GetByEmployeeIdAsync(int employeeId)
    {
        var queryObject = new QueryObject(PostgresPassportElement.GetPassportByEmployeeId, new
        {
            employeeId=employeeId
        });
        
        return await _dapperContext.FirstOrDefault<DbPassport>(queryObject);
    }
    
    public async Task<bool> IsPassportExistByNumber(string number)
    {
        var queryObject = new QueryObject(PostgresPassportElement.IsPassportExistByNumber, new
        {
            number
        });
        
        return await _dapperContext.CommandWithResponse<bool>(queryObject);
    }
}