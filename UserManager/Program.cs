using System.Runtime.CompilerServices;
using FastEndpoints;
using FastEndpoints.Swagger;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using UserManager;
using UserManager.Mappings;
using UserManager.Mongo;
using UserManager.Services;
using UserManager.Validators;

[assembly: InternalsVisibleTo("UserManager.Test")]
;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints().SwaggerDocument();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new UserProfile()));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["JwtBearer:Authority"];
    options.Audience = builder.Configuration["JwtBearer:Audience"];
});
builder.Services.AddAuthorization();

// Register our own services
builder.Services.AddScoped<AbstractValidator<UserManager.Models.User>>(_ => new UserValidator());
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository>(services =>
{
    var mapper = services.GetService<IMapper>() ?? throw new ArgumentException($"Could not find {nameof(IMapper)} dependency registration.");
    var connectionString = builder.Configuration["MongoDB:ConnectionString"] ?? throw new ArgumentException("'MongoDB:ConnectionString' is not configured.");
    var database = builder.Configuration["MongoDB:Database"] ?? throw new ArgumentException("'MongoDB:Database' is not configured.");
    return new MongoUserRepository(connectionString, database, mapper);
});
var app = builder.Build();

app.SeedData();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
if (!app.Environment.IsDevelopment())
{
    app.UseDefaultExceptionHandler();
}
app.UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api/v1";
    c.Serializer.RequestDeserializer = async (req, tDto, jCtx, ct) =>
    {
        using var reader = new StreamReader(req.Body);
        var json = await reader.ReadToEndAsync();
        return Newtonsoft.Json.JsonConvert.DeserializeObject(json, tDto);
    };
});
app.UseSwaggerGen();
app.UseOpenApi();

app.Run();
