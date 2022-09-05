using Cars.Application.Cars.Commands;
using Cars.Application.Cars.Queries;

namespace Web.Services;
public interface ICarService
{
    Task<GetCars.Response> GetCars(GetCars.Query request);
    Task<GetCarFilterOptions.Response> GetFilterOptions();
    Task ReserveCar(ReserveCar.Command request);

}
