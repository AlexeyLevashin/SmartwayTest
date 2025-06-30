using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Companies;

public class CompanyNotFound(string? message = "Компания не найдена") : NotFoundException(message);
