using Application;
using Infrastructure.Dkron;
using Infrastructure.Quartz.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddQuartzService();
builder.Services.AddScoped<IDkronService, DkronService>();
builder.Services.AddHttpClient<IDkronService, DkronService>(c => c.BaseAddress = new Uri("http://localhost:8080/"));

builder.Services.AddScoped<ISqlDataManager, SqlDataManager>();

var logConfiguration = new LoggerConfiguration()//Para exibir o console no terminal 
    .WriteTo.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt"); //para registrar o console no log.txt

Log.Logger = logConfiguration.CreateLogger();

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
