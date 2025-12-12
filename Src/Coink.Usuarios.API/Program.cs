using Coink.Usuarios.Application.Interfaces;
using Coink.Usuarios.Application.UseCases.Command;
using Coink.Usuarios.Infrastructure.Persistence;
using Coink.Usuarios.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// ----------------------
// Configuración DB
// ----------------------
builder.Services.Configure<DatabaseOptions>(
    builder.Configuration.GetSection("ConnectionStrings")
);
builder.Services.AddSingleton<DapperDbContext>();

// ----------------------
// Repositorios
// ----------------------
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IParametrosRepository, ParametrosRepository>();

// ----------------------
// MediatR
// ----------------------
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Coink.Usuarios.Application.AssemblyMarker).Assembly)
);

// ----------------------
// FluentValidation
// ----------------------
// Registramos los validadores pero **deshabilitamos la validación automática**
builder.Services.AddValidatorsFromAssembly(typeof(Coink.Usuarios.Application.AssemblyMarker).Assembly);

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.DisableDataAnnotationsValidation = true; // Desactiva validación automática de DataAnnotations
        fv.AutomaticValidationEnabled = false;     // Desactiva validación automática de FluentValidation
    });

// ----------------------
// Swagger y controladores
// ----------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ----------------------
// Middleware
// ----------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
