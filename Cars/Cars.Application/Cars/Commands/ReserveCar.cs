using Cars.Application.Interfaces;
using FluentValidation;
using MediatR;

namespace Cars.Application.Cars.Commands;
public static class ReserveCar
{
    public record Command(long CarId) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command>
    {
        private readonly IApplicationDbContext dataContext;

        public Handler(IApplicationDbContext dataContext)
            => this.dataContext = dataContext;

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            dataContext.Reservations.Add(new()
            {
                CarId = request.CarId
            });

            await dataContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class QueryValidator : AbstractValidator<Command>
    {
        public QueryValidator(IApplicationDbContext dataContext)
        {
            RuleFor(c => c.CarId)
                .Must(carId =>
                {
                    return dataContext.Cars
                        .Where(c => c.Id == carId)
                        .Any();
                })
                .WithMessage("Car not found");
        }
    }
}