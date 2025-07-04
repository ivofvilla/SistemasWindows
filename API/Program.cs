using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using System.Reflection;
using API.Dados;

var builder = WebApplication.CreateBuilder(args);

// DbContext com InMemory
builder.Services.AddDbContext<DbContexto>(options =>
    options.UseInMemoryDatabase("ClienteDB"));

// MediatR com assembly atual
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

// NECESSÁRIO para Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilitar Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilita rota para controllers
app.UseAuthorization();
app.MapControllers();

// Rota de teste
app.MapGet("/", () => "API Cliente Rodando");

app.Run();
