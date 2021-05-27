using TicketSales.Core.Domain.Entities;

namespace TicketSales.Messages.Commands
{
    public class CreateConcertCommand
    {
        public Concert Concert { get; set; }
    }
}