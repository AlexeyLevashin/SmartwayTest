using Application.Dto.Department.Responses;
using Application.Dto.Employee.Requests;
using Application.Dto.Employee.Responses;
using Application.Dto.Passport.Requests;
using Application.Dto.Passport.Responses;
using Application.Interfaces;
using Application.Services;
using Domain.DbModels;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        return services;
    }
    
    public static IServiceProvider ConfigureMapping(this IServiceProvider serviceProvider)
    {
        TypeAdapterConfig<CreateEmployeeRequest, DbEmployee>.NewConfig()
            .Map(dest => dest.Passport, src => src.Passport);

        TypeAdapterConfig<DbEmployeeDetails, GetEmployeeResponse>.NewConfig()
            .Map(dest => dest.Passport,
                src => new GetPassportResponse { Type = src.PassportType, Number = src.PassportNumber })
            .Map(dest => dest.Department,
                src => new GetEmployeeDepartmentResponse { Name = src.DepartmentName, Phone = src.DepartmentPhone });

        TypeAdapterConfig<DbEmployeePassport, GetEmployeeWithPassportResponse>.NewConfig()
            .Map(dest => dest.Passport,
                src => new GetPassportResponse { Type = src.PassportType, Number = src.PassportNumber });

        TypeAdapterConfig<UpdateEmployeeRequest, DbEmployee>
            .NewConfig()
            .IgnoreNullValues(true);
        
        TypeAdapterConfig<UpdatePassportRequest, DbPassport>
            .NewConfig()
            .IgnoreNullValues(true);
        
        return serviceProvider;
    }
}