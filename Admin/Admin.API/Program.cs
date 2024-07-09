using Admin.Application.Extentions;
using Admin.Infra.Extension;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApiVersioning();

builder.Services.AddHealthChecks().AddSqlServer(builder.Configuration.GetConnectionString("DBconnection"), tags: new[] { "DB Health Check" });
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

builder.Services.AddInfraServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Admin API", Version = "v1" });
});
var app = builder.Build();
app.MapHealthChecksUI();
app.MapHealthChecks("/adminHealth", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Admin API v1"));
}
app.MapControllers();
app.MapGet("/", () => "Admin API");

app.Run();