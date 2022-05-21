using Knab.Identity.Api;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
builder.AddCustomSerilog();

builder.AddCustomConfiguration();

builder.AddCustomController();

builder.AddCustomCors();

builder.AddCustomOpenIddict();


var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseMigrationsEndPoint();
else
    app.UseStatusCodePagesWithRedirects("/Home/Error");

app.UseStaticFiles();

app.UseRouting();


app.UseCors("default");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

try
{
    app.Logger.LogInformation("Starting web host...");
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}