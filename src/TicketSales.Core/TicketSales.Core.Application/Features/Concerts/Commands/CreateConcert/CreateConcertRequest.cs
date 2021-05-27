using MediatR;
using TicketSales.Core.Domain.Entities;
using TicketSales.Messages.Commands;

namespace TicketSales.Core.Application.Features.Concerts.Commands.CreateConcert
{
    public class CreateConcertRequest : IRequest<Concert>
    {
        public CreateConcertCommand Command { get; set; }
    }
}