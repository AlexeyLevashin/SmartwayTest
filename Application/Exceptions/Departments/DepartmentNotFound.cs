using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Departments;

public class DepartmentNotFound : NotFoundException
{
    public DepartmentNotFound(string? message = "Отдел не найден") : base(message) { }
}