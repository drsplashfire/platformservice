using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlatformService.Controllers;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;
using Serilog.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMem"));
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddHttpClient<ICommanDataCLient, HttpCommandDataCLient>();

Console.WriteLine($"the server is running on endpoint: {builder.Configuration["CommandsService"]}");

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    // Determine the port based on environment
//    var port = builder.Environment.IsDevelopment( )
//        ? 5175  // Use the port from launchSettings.json for development
//        : 80;   // Use port 80 for containerized environment

//    serverOptions.ListenAnyIP(port, options =>
//    {
//        options.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
//    });
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
PrepDb.PrepPopulation(app);
app.Run();
