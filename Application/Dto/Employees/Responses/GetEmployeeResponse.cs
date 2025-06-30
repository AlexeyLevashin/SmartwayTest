using Application.Dto.Departments.Responses;
using Application.Dto.Passports.Responses;

namespace Application.Dto.Employees.Responses;

public class GetEmployeeResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public int CompanyId { get; set; }
    public GetPassportResponse Passport { get; set; }
    public GetEmployeeDepartmentResponse Department { get; set; }
}   