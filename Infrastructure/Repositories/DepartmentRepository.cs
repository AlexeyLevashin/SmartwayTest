using DataAccess.Dapper;
using DataAccess.Dapper.Interfaces;
using Domain.DbModels;
using Domain.Interfaces;
using Infrastructure.Repositories.Scripts.Departments;


namespace Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly IDapperContext _dapperContext;

    public DepartmentRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    
    public async Task<DbDepartment> CreateAsync(DbDepartment dbDepartment)
    {
        var queryObject = new QueryObject(
            PostgresDepartmentElement.CreateDepartment, new { name = dbDepartment.Name, phone = dbDepartment.Phone });

        return await _dapperContext.CommandWithResponse<DbDepartment>(queryObject);
    }
    
    public async Task<List<DbDepartment>> GetAllAsync()
    {
        var queryObject = new QueryObject(PostgresDepartmentElement.GetAllDepartments);
        return await _dapperContext.ListOrEmpty<DbDepartment>(queryObject);
    }
    
    public async Task<DbDepartment?> GetByIdAsync(int id)
    {
        var queryObject = new QueryObject(PostgresDepartmentElement.GetDepartmentById, new { Id = id });
        return await _dapperContext.FirstOrDefault<DbDepartment>(queryObject);
    }
    
    public async Task<DbDepartment?> GetByPhoneAsync(string phone)
    {
        var queryObject = new QueryObject(PostgresDepartmentElement.GetDepartmentByPhone, 
            new { phone = phone });
        
        return await _dapperContext.FirstOrDefault<DbDepartment>(queryObject);
    }
}