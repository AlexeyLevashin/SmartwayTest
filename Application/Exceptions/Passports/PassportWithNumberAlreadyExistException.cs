using Application.Exceptions.Abstractions;

namespace Application.Exceptions.Passports;

public class PassportWithNumberAlreadyExistException(string? message = "Паспорт с данным номером уже существует")
    : BadRequestException(message);