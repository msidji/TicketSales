using TicketSales.Core.Domain.Entities;

namespace TicketSales.Messages.Events
{
    public class ConcertCreatedEvent : IntegrationEvent
    {
        public Concert Concert { get; set; }
    }
}