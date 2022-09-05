using Cars.Application.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cars.Application.Cars.Queries;
public static class GetCarFilterOptions
{
    public record Query() : IRequest<Response>;
    public record Response(string[] Categories, int[] ProductionYears, string[] Brands, string[] Models);
    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IApplicationDbContext dataContext;

        public Handler(IApplicationDbContext dataContext)
            => this.dataContext = dataContext;

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            //hacks for demo purpose

            var categories = await dataContext.Cars
                .GroupBy(c => c.Category)
                .Select(c => c.Key)
                .ToListAsync(cancellationToken);

            var productionYears = await dataContext.Cars
                .GroupBy(c => c.ProductionYear)
                .Select(c => c.Key)
                .ToListAsync(cancellationToken);

            var brands = await dataContext.Cars
                .GroupBy(c => c.Brand)
                .Select(c => c.Key)
                .ToListAsync(cancellationToken);

            var models = await dataContext.Cars
                .GroupBy(c => c.Model)
                .Select(c => c.Key)
                .ToListAsync(cancellationToken);

            return new
            {
                categories,
                productionYears,
                brands,
                models
            }.Adapt<Response>();
        }
    }
}
