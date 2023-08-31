using Application.Dkron;
using CrystalQuartz.AspNetCore;
using Infrastructure.Dkron;
using Infrastructure.Quartz;
using Quartz;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

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
    .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
    .WriteTo.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt"); //para registrar o console no log.txt

Log.Logger = logConfiguration.CreateLogger();

var app = builder.Build();

var factory = app.Services.GetRequiredService<ISchedulerFactory>();
app.UseCrystalQuartz(() => factory.GetScheduler());

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
