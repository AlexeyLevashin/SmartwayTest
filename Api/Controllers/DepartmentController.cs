using Application.Dto.Departments.Requests;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/departments")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateDepartmentRequest createDepartmentRequest)
    {
        var result = await _departmentService.CreateAsync(createDepartmentRequest);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _departmentService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _departmentService.GetByIdAsync(id);
        return Ok(result);
    }
    
    [HttpGet("{id}/employees")]
    public async Task<IActionResult> GetEmployees(int id)
    {
        return Ok(await _departmentService.GetEmployeesByDepartmentIdAsync(id));
    }
}