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
        var employeeTask = _employeeRepository.GetByPhoneAsync(createEmployeeRequest.Phone);
        var departmentTask = _departmentRepository.GetByIdAsync(createEmployeeRequest.DepartmentId);
        var passportTask = _passportRepository.IsPassportExistByNumber(createEmployeeRequest.Passport.Number);
        
        await Task.WhenAll(employeeTask, departmentTask, passportTask);
        
        var employee = await employeeTask;
        var departmentExists = await departmentTask;
        var isPassportExistByNumber = await passportTask;
        
        if (departmentExists is null)
            throw new DepartmentNotFound();

        if (employee is not null)
            throw new EmployeePhoneIsExist();

        if (isPassportExistByNumber)
            throw new PassportWithNumberAlreadyExistException();
        
        var employeeCandidate = createEmployeeRequest.Adapt<DbEmployee>();
        var passportCandidate = createEmployeeRequest.Passport.Adapt<DbPassport>();
    
        _employeeRepository.BeginTransaction();
        try
        {
            var employeeId = await _employeeRepository.CreateAsync(employeeCandidate);
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
        var employeeTask = _employeeRepository.GetByIdAsync(id);
        var passportTask = _passportRepository.GetByEmployeeIdAsync(id);
        var departmentId = updateEmployeeRequest.DepartmentId ?? -1;
        var departmentTask = _departmentRepository.GetByIdAsync(departmentId);

        await Task.WhenAll(employeeTask, passportTask, departmentTask).ConfigureAwait(false);

        var dbEmployee = await employeeTask.ConfigureAwait(false);
        var dbPassport = await passportTask.ConfigureAwait(false);
        var dbDepartment = await departmentTask.ConfigureAwait(false);

        if (dbEmployee is null) throw new EmployeeNotFound();
        if (dbPassport is null) throw new PassportNotFound();
        if (dbDepartment is null) throw new DepartmentNotFound();
        
        Task<DbEmployee?> phoneCheckTask = Task.FromResult<DbEmployee?>(null);
        Task<bool> passportCheckTask = Task.FromResult(false);

        if (updateEmployeeRequest.Phone is not null && 
            !string.Equals(updateEmployeeRequest.Phone, dbEmployee.Phone, StringComparison.Ordinal))
        {
            phoneCheckTask = _employeeRepository.GetByPhoneAsync(updateEmployeeRequest.Phone);
        }

        if (updateEmployeeRequest.Passport?.Number is not null && 
            !string.Equals(updateEmployeeRequest.Passport.Number, dbPassport.Number, StringComparison.Ordinal))
        {
            passportCheckTask = _passportRepository.IsPassportExistByNumber(updateEmployeeRequest.Passport.Number);
        }

        await Task.WhenAll(phoneCheckTask, passportCheckTask).ConfigureAwait(false);

        if (await phoneCheckTask.ConfigureAwait(false) is not null) throw new EmployeePhoneIsExist();
        if (await passportCheckTask.ConfigureAwait(false)) throw new PassportWithNumberAlreadyExistException();
        
        var employeeForUpdate = updateEmployeeRequest.Adapt(dbEmployee);
        var passportForUpdate = updateEmployeeRequest.Passport?.Adapt(dbPassport) ?? dbPassport;

        _employeeRepository.BeginTransaction();
        try
        {
            var updatedDbEmployee = await _employeeRepository.UpdateAsync(employeeForUpdate);
            var updatedDbPassport = await _passportRepository.UpdateByEmployeeIdAsync(passportForUpdate);
    
            _employeeRepository.Commit();
            
            return new GetEmployeeResponse
            {
                Id = updatedDbEmployee.Id,
                Name = updatedDbEmployee.Name,
                Surname = updatedDbEmployee.Surname,
                Phone = updatedDbEmployee.Phone,
                Passport = updatedDbPassport.Adapt<GetPassportResponse>(),
                Department = dbDepartment.Adapt<GetEmployeeDepartmentResponse>()
            };
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
        
        return employees.Select(e => e.Adapt<GetEmployeeResponse>()).ToList();
    }
}