using Application.Dto.Departments.Responses;
using Application.Dto.Employees.Requests;
using Application.Dto.Employees.Responses;
using Application.Dto.Passports.Responses;
using Application.Exceptions.Companies;
using Application.Exceptions.Departments;
using Application.Exceptions.Employees;
using Application.Exceptions.Passports;
using Application.Interfaces;
using Domain.DbModels;
using Domain.Interfaces;
using Mapster;

namespace Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPassportRepository _passportRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public EmployeeService(IEmployeeRepository employeeRepository, IPassportRepository passportRepository, IDepartmentRepository departmentRepository)
    {
        _employeeRepository = employeeRepository;
        _passportRepository = passportRepository;
        _departmentRepository = departmentRepository;
    }


    public async Task<CreateEmployeeResponse> CreateAsync(CreateEmployeeRequest createEmployeeRequest)
    {
        var employee = await _employeeRepository.GetByPhoneAsync(createEmployeeRequest.Phone);
        
        if (await _departmentRepository.GetByIdAsync(createEmployeeRequest.DepartmentId) is null)
        {
            throw new DepartmentNotFound();
        }
        
        if (employee is not null)
        {
            throw new EmployeePhoneIsExist();
        }
        
        var isPassportExistByNumber =
            await _passportRepository.IsPassportExistByNumber(createEmployeeRequest.Passport.Number);
        if (isPassportExistByNumber)
        {
            throw new PassportWithNumberAlreadyExistException();
        }
        
        _employeeRepository.BeginTransaction();
        
        try
        {
            var employeeCandidate = createEmployeeRequest.Adapt<DbEmployee>();
            var employeeId = await _employeeRepository.CreateAsync(employeeCandidate);
            
            var passportCandidate = createEmployeeRequest.Passport.Adapt<DbPassport>();
            passportCandidate.EmployeeId = employeeId;
            
            await _passportRepository.CreateAsync(passportCandidate);

            _employeeRepository.Commit();
            return new CreateEmployeeResponse { Id = employeeId };
        }
        catch
        {
            _employeeRepository.Rollback();
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        
        if (employee is null)
        {
            throw new EmployeeNotFound();
        }
        
        await _employeeRepository.DeleteAsync(id);
    }

    public async Task<GetEmployeeResponse> UpdateAsync(UpdateEmployeeRequest updateEmployeeRequest, int id)
    {
        var dbEmployee = await _employeeRepository.GetByIdAsync(id);
        var dbPassport = await _passportRepository.GetByEmployeeIdAsync(id);

        if (dbEmployee is null)
        {
            throw new EmployeeNotFound();
        }
        
        if (updateEmployeeRequest.Phone is not null && await _employeeRepository.GetByPhoneAsync(updateEmployeeRequest.Phone) is not null && !string.Equals(updateEmployeeRequest.Phone, dbEmployee.Phone))
        {
            throw new EmployeePhoneIsExist();
        }
        
        if (dbPassport is null)
        {
            throw new PassportNotFound();
        }
        
        var dbDepartment = await _departmentRepository.GetByIdAsync(updateEmployeeRequest.DepartmentId ?? dbEmployee.DepartmentId);
        
        if (dbDepartment is null)
        {
            throw new DepartmentNotFound();
        }
        
        if (updateEmployeeRequest.Passport?.Number is not null && await _passportRepository.IsPassportExistByNumber(updateEmployeeRequest.Passport.Number) && !string.Equals(updateEmployeeRequest.Passport.Number, dbPassport.Number))
        {
            throw new PassportWithNumberAlreadyExistException();
        }
        
        var updatedEmployee = updateEmployeeRequest.Adapt(dbEmployee);

        var updatedPassport = updateEmployeeRequest.Passport?.Adapt(dbPassport) ?? dbPassport;

        _employeeRepository.BeginTransaction();
        
        try
        {
            var updatedDbEmployee = await _employeeRepository.UpdateAsync(updatedEmployee);
            var updatedDbPassport = await _passportRepository.UpdateByEmployeeIdAsync(updatedPassport);
            
            _employeeRepository.Commit();

            var res = updatedDbEmployee.Adapt<GetEmployeeResponse>();
            res.Passport = updatedDbPassport.Adapt<GetPassportResponse>();
            res.Department = dbDepartment.Adapt<GetEmployeeDepartmentResponse>();
            return res;
        }
        catch
        {
            _employeeRepository.Rollback();
            throw;
        }
    }

    public async Task<List<GetEmployeeResponse>> GetCompanyEmployeesAsync(int companyId)
    {
        var employees = await _employeeRepository.GetEmployeesByCompanyIdAsync(companyId);
        
        if (employees is null || employees.Count == 0)
        {
            throw new CompanyNotFound();
        }
        
        return employees.Select(e => e.Adapt<GetEmployeeResponse>()).ToList();
    }
}