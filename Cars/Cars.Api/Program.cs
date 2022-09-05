using Cars.Application;
using Cars.Infrastructure;
using FluentValidation.AspNetCore;
using Mapster;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddDapr();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddFluentValidationAutoValidation(x => x.DisableDataAnnotationsValidation = true);
var app = builder.Build();

TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);

app.UseRouting();
app.UseCloudEvents();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapSubscribeHandler();
});

app.Run();
