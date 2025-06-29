using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Passport.Requests;

public class CreatePassportRequest
{
    [Required(ErrorMessage = "Тип паспорта обязателен.")]
    [MinLength(1, ErrorMessage = "Тип паспорта не может быть пустым.")]
    public string Type { get; set; }

    [Required(ErrorMessage = "Номер паспорта обязателен.")]
    [MinLength(1, ErrorMessage = "Номер паспорта не может быть пустым.")]
    public string Number { get; set; }
}