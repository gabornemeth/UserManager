using System.Runtime.CompilerServices;
using FastEndpoints;
using FastEndpoints.Swagger;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
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
    options.Authority = builder.Configuration["JwtAuthority"];
    options.Audience = builder.Configuration["JwtAudience"];
});
builder.Services.AddAuthorization();

// Register our own services
builder.Services.AddScoped<AbstractValidator<UserManager.Models.User>>(_ => new UserValidator());
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository>(services =>
{
    var mapper = services.GetService<IMapper>();
    return new MongoUserRepository(builder.Configuration["USERMANAGER_MONGODB_CONNECTIONSTRING"],
        builder.Configuration["USERMANAGER_MONGODB_DATABASE"], mapper);
});
var app = builder.Build();

app.SeedData();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
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
