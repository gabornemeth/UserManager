using FastEndpoints;
using FastEndpoints.Swagger;
using UserManager.Mappings;
using UserManager.Mongo;
using UserManager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints().SwaggerDocument();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new MongoDbProfile()));

// Register our own services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, MongoUserRepository>();
var app = builder.Build();

app.SeedData();

app.UseHttpsRedirection();
app.UseFastEndpoints(c => c.Endpoints.RoutePrefix = "api/v1").UseSwaggerGen();
app.UseOpenApi();

app.Run();
