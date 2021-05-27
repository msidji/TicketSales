using MediatR;
using TicketSales.Messages.Commands;

namespace TicketSales.Core.Application.Features.Concerts.Commands.SellTickets
{
    public class SellTicketsRequest : IRequest<bool>
    {
        public BuyTicketsForConcertCommand Command { get; set; }
    }
}