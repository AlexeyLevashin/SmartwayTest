using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Passport.Requests;

public class UpdatePassportRequest
{
    [MinLength(1, ErrorMessage = "Тип паспорта не может быть пустым.")]
    public string? Type { get; set; }

    [MinLength(1, ErrorMessage = "Номер паспорта не может быть пустым.")]
    public string? Number { get; set; }
}