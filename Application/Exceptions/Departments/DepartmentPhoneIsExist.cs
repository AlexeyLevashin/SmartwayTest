using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Departments;

public class DepartmentPhoneIsExist : BadRequestException
{
    public DepartmentPhoneIsExist(string? message = "Данный телефон занят другим отделом") : base(message) { }
}