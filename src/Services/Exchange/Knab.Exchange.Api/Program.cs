using Knab.Exchange.Api;
using Knab.Exchange.Infrastructure.Common.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

// Add services to the container.


builder.AddCustomSerilog();

builder.AddCustomConfiguration();


builder.Services.AddMemoryCache();

builder.AddCustomController();

builder.AddCustomCors();

builder.AddCoreApiVersioning();


builder.AddCustomServices();

builder.AddCustomAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerWithVersioning();
}

app.UseCors("default");

app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/", () => Results.LocalRedirect("~/swagger"));

app.MapControllers();

try
{
    app.Logger.LogInformation("Starting web host ({ApplicationName})...", appSettings.AppName);
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Host terminated unexpectedly ({ApplicationName})...", appSettings.AppName);
}
finally
{
    Log.CloseAndFlush();
}