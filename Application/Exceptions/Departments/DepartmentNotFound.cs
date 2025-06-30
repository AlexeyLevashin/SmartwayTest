using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Departments;

public class DepartmentNotFound(string? message = "Отдел не найден") : NotFoundException(message);