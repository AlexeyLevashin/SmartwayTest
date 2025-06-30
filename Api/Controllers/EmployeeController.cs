using Application.Dto.Employees.Requests;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private IEmployeeService _employeeService;
    
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateEmployeeRequest createEmployeeRequest)
    {
        return Ok(await _employeeService.CreateAsync(createEmployeeRequest));
    }
    
    [HttpGet("company/{companyId}")]
    public async Task<IActionResult> GetCompanyEmployees(int companyId)
    {
        return Ok(await _employeeService.GetCompanyEmployeesAsync(companyId));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _employeeService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, UpdateEmployeeRequest updateEmployeeRequest)
    {
        return Ok(await _employeeService.UpdateAsync(updateEmployeeRequest, id));
    }
}