namespace Application.Exceptions.Abstractions;

public class BadRequestException : Exception
{
    protected BadRequestException(string? message) : base(message) { }
}