using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);

// Add logging to project
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add Ocelot to project
builder.Services.AddOcelot();
  
var app = builder.Build();


app.MapGet("/", () => "Hello World!");

// Add ocelot to pipeline
app.UseOcelot(); // possibly add .Wait() here

app.Run();