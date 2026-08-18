using System.Text.Json;
using FastEndpoints;
using FastEndpoints.Swagger;
using Scalar.AspNetCore;
using WildShaper.Api.Common.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWildShaperApi();

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();

builder.Services.AddCors(options =>
{
    options.AddPolicy("WildShaperWeb", policy =>
    {
        var allowedOrigins = builder.Configuration
            .GetSection("Cors:AllowedOrigins")
            .Get<string[]>() ?? [];

        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("WildShaperWeb");

app.UseFastEndpoints(config =>
{
    config.Serializer.Options.PropertyNamingPolicy =
        JsonNamingPolicy.CamelCase;
});

app.UseSwaggerGen(options =>
{
    options.Path = "/openapi/{documentName}.json";
});

app.MapScalarApiReference(options =>
{
    options.Title = "WildShaper API";
    options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});

app.Run();

public partial class Program;