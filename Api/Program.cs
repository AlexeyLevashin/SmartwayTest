using Api.Middlewares;
using Application.Extensions;
using DataAccess.Extensions;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfrastructure();
builder.Services.AddMigrations(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddDapperContext();

var app = builder.Build();
app.Services.UseMigrations();
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.Services.ConfigureMapping();

if (app.Environment.IsDevelopment())
{
    app.MapSwagger();
    app.UseSwaggerUI();    
}

app.Run();