using Microsoft.OpenApi;
using PharmaRnd.API.Extensions;
using PharmaRnd.API.Middlewares;
using PharmaRnd.API.Validation;
using PharmaRnd.Application;
using PharmaRnd.Infrastructure;
using Serilog;
using Serilog.Events;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.Seq(
         builder.Configuration.GetConnectionString("Seq")
         ?? throw new ArgumentNullException("Seq"))
    .Enrich.WithThreadId()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentUserName()
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .CreateLogger();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddSerilog();
builder.Services
    .AddInfrastructure()
    .AddApplication();
builder.Services.AddFluentValidationAutoValidation(configuration =>
{
    configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
});

var app = builder.Build();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "openapi/{documentName}.json";
        options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_1;
    });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("../openapi/v1.json", "PharmaRnd API v1");
    });
    
    await app.ApplyMigrations();    
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization(); 

app.MapControllers();

app.Run();
