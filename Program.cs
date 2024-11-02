var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

// Add services using Startup class
var startup = new CodeTechTech.Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the middleware pipeline using Startup class
startup.Configure(app, app.Environment);

app.Run();