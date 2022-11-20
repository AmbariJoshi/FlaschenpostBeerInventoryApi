using BeerInventoryAPI.ServiceFactory;
using Serilog;
using BeerInventoryApi.Logger;
using BeerInventoryApi.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Factory class returning individual service instances added as scoped
builder.Services.AddScoped<ServicesFactory>();

//register and initialize serilog for logging
var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();


builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
//Initialize logger for further use throughout
LogService.InitLogger(logger);

// Services based on input user request resolved for di. Add new service reference in DependencyResolver to use it
DependencyHandler.Init();

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
LogService.LogInfo("Program", "Startup", "Service started successfully");

app.Run();
