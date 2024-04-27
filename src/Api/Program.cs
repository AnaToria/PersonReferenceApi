using System.Text.Json.Serialization;
using Api.Middlewares;
using Application.Extensions;
using Infrastructure.Extensions;
using Serilog;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("serilog.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("serilog.json");

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddHttpContextAccessor();
builder.Services.AddCors();
var app = builder.Build();

app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
app.UseSerilogRequestLogging();

app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.Run();

