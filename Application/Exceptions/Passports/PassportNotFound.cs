using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Passports;

public class PassportNotFound(string? message = "Данный сотрудник не имеет паспорта") : NotFoundException(message);