using Cars.Application.Cars.Commands;
using Cars.Application.Cars.Queries;
using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Api.Controllers;

public class CarsController : BaseController
{
    [HttpGet]
    public async Task<GetCars.Response> GetCars([FromQuery] GetCars.Query request, CancellationToken cancellationToken)
        => await Mediator.Send(request, cancellationToken);

    [HttpGet("filter-options")]
    public async Task<GetCarFilterOptions.Response> GetFilterOptions(CancellationToken cancellationToken)
        => await Mediator.Send(new GetCarFilterOptions.Query(), cancellationToken);

    [HttpPost("car-reservation")]
    [Topic("pubsub", "car-reservation")]
    public async Task ReserveCar(ReserveCar.Command request, CancellationToken cancellationToken)
        => await Mediator.Send(request, cancellationToken);
}