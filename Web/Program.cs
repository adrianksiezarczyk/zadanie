using Mapster;
using Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDaprClient();

builder.Services.AddHttpClient<ICarService, CarService>((sp, c) =>
{
    c.BaseAddress = new Uri(sp.GetService<IConfiguration>()["ApiConfigs:CarsService:Uri"]);
    c.DefaultRequestHeaders.Add("dapr-app-id", "cars-service");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
