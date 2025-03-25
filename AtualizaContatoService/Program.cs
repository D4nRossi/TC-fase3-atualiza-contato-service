using AtualizaContatoService.Models;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var rabbitHost = config["RabbitMq:Host"] ?? "localhost";
var rabbitUser = config["RabbitMq:Username"] ?? "guest";
var rabbitPass = config["RabbitMq:Password"] ?? "guest";

// MassTransit + RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(rabbitHost, "/", h =>
        {
            h.Username(rabbitUser);
            h.Password(rabbitPass);
        });
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
