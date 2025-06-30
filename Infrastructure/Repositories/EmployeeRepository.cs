using DataAccess.Dapper;
using DataAccess.Dapper.Interfaces;
using Domain.DbModels;
using Domain.Interfaces;
using Infrastructure.Repositories.Scripts.Employees;

namespace Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDapperContext _dapperContext;

    public EmployeeRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    
    public void BeginTransaction()
    {
        _dapperContext.BeginTransaction();
    }

    public void Commit()
    {
        _dapperContext.Commit();
    }

    public void Rollback()
    {
        _dapperContext.Rollback();
    }
    
    public async Task<int> CreateAsync(DbEmployee employee)
    {
        var queryObject = new QueryObject(PostgresEmployeeElement.CreateEmployee, new
        {
            name = employee.Name,
            surname = employee.Surname,
            phone = employee.Phone,
            companyId = employee.CompanyId,
            departmentId = employee.DepartmentId
        });
            
        var employeeId = await _dapperContext.CommandWithResponse<int>(queryObject);
        return employeeId;
    }
    
    public async Task<DbEmployee> UpdateAsync(DbEmployee dbEmployee)
    {
        var queryObject = new QueryObject(PostgresEmployeeElement.UpdateEmployee, new
        {
            name=dbEmployee.Name,
            surname=dbEmployee.Surname,
            phone=dbEmployee.Phone,
            companyId=dbEmployee.CompanyId,
            departmentId=dbEmployee.DepartmentId,
            employeeId=dbEmployee.Id
        });
        
        return await _dapperContext.CommandWithResponse<DbEmployee>(queryObject);
    }

    public async Task DeleteAsync(int employeeId)
    {
        var queryObject = new QueryObject(PostgresEmployeeElement.DeleteEmployeeById, new
        {
            id = employeeId
        });
        
        await _dapperContext.Command(queryObject);
    }

    public async Task<DbEmployee?> GetByIdAsync(int id)
    {
        var queryObject = new QueryObject(PostgresEmployeeElement.GetEmployeeById, new
        {
            id = id
        });

        return await _dapperContext.FirstOrDefault<DbEmployee>(queryObject);
    }

    public async Task<DbEmployee?> GetByPhoneAsync(string phone)
    {
        var queryObject = new QueryObject(PostgresEmployeeElement.GetEmployeeByPhone, new
        {
            phone = phone
        });

        return await _dapperContext.FirstOrDefault<DbEmployee>(queryObject);
    }

    public async Task<List<DbEmployeeDetails>?> GetEmployeesByCompanyIdAsync(int companyId)
    {
        var queryObject = new QueryObject(PostgresEmployeeElement.GetCompanyEmployees, new { companyId = companyId });
        var res = await _dapperContext.ListOrEmpty<DbEmployeeDetails>(queryObject);
        
        return res;
    }
    
    public async Task<List<DbEmployeePassport>> GetEmployeesByDepartmentIdAsync(int departmentId)
    {
        var queryObject = new QueryObject(PostgresEmployeeElement.GetDepartmentEmployees, new { departmentId = departmentId });
        
        return await _dapperContext.ListOrEmpty<DbEmployeePassport>(queryObject);
    }
}