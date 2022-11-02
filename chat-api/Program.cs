using Domain.Interfaces;
using Infra.RabbitMQ;
using Infra.RabbitMQ.Service;
using MediatR;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

var configSection = builder.Configuration.GetSection("RabbitMQ");
var settings = new RabbitMQSettings();
configSection.Bind(settings);
builder.Services.AddSingleton(settings);

builder.Services.AddSingleton<IConnectionFactory>(x => new ConnectionFactory
{
    HostName = settings.HostName,
    Port = settings.port,
    UserName = settings.UserName,
    Password = settings.Password
});

builder.Services.AddSingleton<RabbitModelFactory>();
builder.Services.AddSingleton(x => x.GetRequiredService<RabbitModelFactory>().CreateChannel());
builder.Services.AddSingleton<IMessageService, MessageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
