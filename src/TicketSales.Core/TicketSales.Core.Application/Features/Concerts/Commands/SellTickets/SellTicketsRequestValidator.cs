using FluentValidation;

namespace TicketSales.Core.Application.Features.Concerts.Commands.SellTickets
{
    public class SellTicketsRequestValidator : AbstractValidator<SellTicketsRequest>
    {
        public SellTicketsRequestValidator()
        {
            RuleFor(p => p.Command.ConcertId)
                .NotEmpty().WithMessage("{p.Command.ConcertId} is required.")
                .GreaterThan(0).WithMessage("{p.Command.ConcertId} should be greater than zero.");

            RuleFor(p => p.Command.TicketsToBuy)
                .NotEmpty().WithMessage("{p.Command.TicketsToBuy} is required.")
                .GreaterThan(0).WithMessage("{p.Command.TicketsToBuy} should be greater than zero.");
        }
    }
}