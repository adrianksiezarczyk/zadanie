using Cars.Application.Common;
using Cars.Application.Interfaces;
using Mapster;
using MediatR;

namespace Cars.Application.Cars.Queries;
public static class GetCars
{
    //public record Query(string? Category, int? ProductionYear, string? Brand, string? Model, int PageNumber, int PageSize) : IRequest<Response>;
    public class Query : IRequest<Response>
    {
        public string? Category { get; set; }
        public int? ProductionYear { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public record ResponseItem(long Id, string Category, string Brand, string Model, int ProductionYear);
    public record Response(ResponseItem[] Data, int TotalItems);
    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IApplicationDbContext dataContext;

        public Handler(IApplicationDbContext dataContext)
            => this.dataContext = dataContext;

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var cars = dataContext.Cars.AsQueryable();

            if (!string.IsNullOrEmpty(request.Category))
                cars = cars.Where(c => c.Category == request.Category);

            if (request.ProductionYear.HasValue)
                cars = cars.Where(c => c.ProductionYear == request.ProductionYear.Value);

            if (!string.IsNullOrEmpty(request.Brand))
                cars = cars.Where(c => c.Brand == request.Brand);

            if (!string.IsNullOrEmpty(request.Model))
                cars = cars.Where(c => c.Model == request.Model);

            var result = await PaginatedList.Create(cars, request.PageNumber, request.PageSize);

            return new { result.Data, result.TotalItems }.Adapt<Response>();
        }
    }
}
