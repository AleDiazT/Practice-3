using Microsoft.OpenApi.Models;
using UPB.CoreLogic.Managers;
using UPB.PracticeThree.Middlewares;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<PatientManager>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var configurationBuilder = new ConfigurationBuilder() 
        .SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true) 
        .AddEnvironmentVariables();

IConfiguration Configuration = configurationBuilder.Build(); 
string siteTitle = Configuration.GetSection("Title").Value;

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

builder.Services.AddSwaggerGen(options =>
{
    options. SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1", 
        Title = siteTitle
    });

});

// SERILOG
var loggerConfiguration = new LoggerConfiguration()
    .ReadFrom.Configuration(Configuration);

Log.Logger = loggerConfiguration.CreateLogger();
builder.Services.AddSingleton(Log.Logger);

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseGlobalExceptionHandler();
//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
