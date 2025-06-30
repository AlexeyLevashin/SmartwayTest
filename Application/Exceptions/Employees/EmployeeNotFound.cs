using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Employees;

public class EmployeeNotFound(string? message = "Данного сотрудника нет в системе") : NotFoundException(message);