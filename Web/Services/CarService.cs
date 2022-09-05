using Cars.Application.Cars.Commands;
using Cars.Application.Cars.Queries;
using Dapr.Client;
using Web.Helpers;

namespace Web.Services;

public class CarService : ICarService
{
    private readonly HttpClient client;
    private readonly DaprClient daprClient;

    public CarService(HttpClient client, DaprClient daprClient)
    {
        this.client = client;
        this.daprClient = daprClient;
    }

    public async Task<GetCars.Response> GetCars(GetCars.Query request)
    {
        var response = await client.GetAsync($"cars?{HttpHelper.GetQueryString(request)}");
        return await response.ReadContentAs<GetCars.Response>();
    }

    public async Task<GetCarFilterOptions.Response> GetFilterOptions()
    {
        var response = await client.GetAsync("cars/filter-options");
        return await response.ReadContentAs<GetCarFilterOptions.Response>();
    }

    public async Task ReserveCar(ReserveCar.Command request)
        => await daprClient.PublishEventAsync("pubsub", "car-reservation", request);
}
