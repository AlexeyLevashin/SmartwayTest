﻿using System.ComponentModel.DataAnnotations;
using Application.Dto.Passports.Requests;

namespace Application.Dto.Employees.Requests;

public class UpdateEmployeeRequest
{
    [MinLength(1, ErrorMessage = "Имя не может быть пустым.")]
    public string? Name { get; set; }

    [MinLength(1, ErrorMessage = "Фамилия не может быть пустой.")]
    public string? Surname { get; set; }

    [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Телефон должен содержать от 10 до 15 цифр и может начинаться с '+'.")]
    public string? Phone { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "ID компании не может быть отрицательным.")]
    public int? CompanyId { get; set; }

    public int? DepartmentId { get; set; }

    public UpdatePassportRequest? Passport { get; set; }
}