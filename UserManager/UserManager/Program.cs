using FastEndpoints;
using FastEndpoints.Swagger;
using UserManager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints().SwaggerDocument();

// Register our own services
builder.Services.AddSingleton<IUserService, DummyUserService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseFastEndpoints(c => c.Endpoints.RoutePrefix = "api/v1").UseSwaggerGen();
app.UseOpenApi();

app.Run();
