using System;

namespace TicketSales.Messages.Events
{
    public class ConcertCreatedEvent : IntegrationEvent
    {
        public long ConcertId { get; set; }
        public string Name { get; set; }
        public long TicketsInSale { get; set; }
        public long TicketsSold { get; set; }
        public DateTime? EventDate { get; set; }
    }
}