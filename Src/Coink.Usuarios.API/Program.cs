
using Coink.Usuarios.Application.Interfaces;
using Coink.Usuarios.Infrastructure.Persistence;
using Coink.Usuarios.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseOptions>(
    builder.Configuration.GetSection("ConnectionStrings")
);
builder.Services.AddSingleton<DapperDbContext>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IParametrosRepository, ParametrosRepository>();
builder.Services.AddAutoMapper(cfg =>
{    
}, AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Coink.Usuarios.Application.AssemblyMarker).Assembly)
);

builder.Services.AddValidatorsFromAssembly(typeof(Coink.Usuarios.Application.AssemblyMarker).Assembly);

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.DisableDataAnnotationsValidation = true; 
        fv.AutomaticValidationEnabled = false;     
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
