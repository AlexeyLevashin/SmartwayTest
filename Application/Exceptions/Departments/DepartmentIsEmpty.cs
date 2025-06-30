using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Departments;

public class DepartmentIsEmpty(string? message = "Отдел не содержит сотрудников") : NotFoundException(message);