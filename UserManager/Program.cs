using System.Runtime.CompilerServices;
using FastEndpoints;
using FastEndpoints.Swagger;
using FluentValidation;
using UserManager.Mappings;
using UserManager.Mongo;
using UserManager.Services;

[assembly:InternalsVisibleTo("UserManager.Test")] ;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints().SwaggerDocument();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new UserProfile()));
builder.Services.AddAuthentication().AddJwtBearer(options => options.Audience = "usermanager-api");
builder.Services.AddAuthorization();

// Register our own services
builder.Services.AddScoped<AbstractValidator<UserManager.Models.User>>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, MongoUserRepository>();
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
