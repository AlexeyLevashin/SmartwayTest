using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Department.Requests;

public class CreateDepartmentRequest
{
    [Required(ErrorMessage = "Название отдела обязательно для заполнения.")]
    [MinLength(1, ErrorMessage = "Название отдела не может быть пустым.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Номер телефона отдела обязателен для заполнения.")]
    [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Номер телефона должен содержать только цифры и может начинаться с '+', длина от 10 до 15 символов.")]
    public string Phone { get; set; }
}