using FluentValidation;

namespace TicketSales.Core.Application.Features.Concerts.Commands.CreateConcert
{
    public class CreateConcertRequestValidator : AbstractValidator<CreateConcertRequest>
    {
        public CreateConcertRequestValidator()
        {
            RuleFor(p => p.Command.Concert.Name)
                .NotEmpty().WithMessage("{p.Command.Concert.Name} is required.")
                .NotNull();

            RuleFor(p => p.Command.Concert.TicketsInSale)
                .NotEmpty().WithMessage("{p.Command.Concert.TicketsInSale} is required.")
                .GreaterThan(0).WithMessage("{p.Command.Concert.TicketsInSale} should be greater than zero.");
        }
    }
}