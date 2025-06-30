using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Employees;

public class EmployeePhoneIsExist(string? message = "Сотрудник с данным номером телефона уже существует")
    : BadRequestException(message);