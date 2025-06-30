using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Departments;

public class DepartmentPhoneIsExist(string? message = "Данный телефон занят другим отделом")
    : BadRequestException(message);
