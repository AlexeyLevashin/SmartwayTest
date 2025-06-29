namespace Application.Exceptions.Abstractions;

public class NotFoundException : Exception
{
    protected NotFoundException(string? message) : base(message) { }
}