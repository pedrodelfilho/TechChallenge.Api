using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using TechChallenge.Api.Extensions;
using TechChallenge.Api.Options.IoC;
using TechChallenge.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BdPadraoConnection")));

builder.Services.AddCors();
builder.Services.AddApiProblemDetails();
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddVersioning();
builder.Services.AddSwagger();
builder.Services.ResolveLog();
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.UseProblemDetails();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(builder => builder
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseRouting();

app.MapControllers();

app.UseMetricServer();
app.UseHttpMetrics();
app.UseMiddleware<LatencyMiddlewareExtension>();

app.Run();
