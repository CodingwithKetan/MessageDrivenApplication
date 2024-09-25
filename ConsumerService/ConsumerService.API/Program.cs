using ConsumerService.API;
using ConsumerService.Domain.Interfaces;
using ConsumerService.Infrastructure;
using ConsumerService.Infrastructure.Interfaces;
using ConsumerService.Infrastructure.Services;
using RabbitMQ.Client;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }); ;

builder.Services.AddSingleton<IConnection>(sp =>
{
    var factory = new ConnectionFactory()
    {
        HostName = "localhost"
    };
    return factory.CreateConnection();
});

builder.Services.AddSingleton<IModel>(sp =>
{
    var connection = sp.GetRequiredService<IConnection>();
    return connection.CreateModel();
});

builder.Services.AddHostedService<Worker>();

builder.Services.AddScoped<IMessageQueueService, RabbitMQService>();
builder.Services.AddScoped<IMessageHandler, MessageHandler>();
builder.Services.AddSingleton<IProcessingResultService, ProcessingResultService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();

app.Run();