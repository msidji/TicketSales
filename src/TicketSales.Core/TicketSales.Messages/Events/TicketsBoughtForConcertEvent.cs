namespace TicketSales.Messages.Events
{
    public class TicketsBoughtForConcertEvent : IntegrationEvent
    {
        public long ConcertId { get; set; }

        public string Username { get; set; }

        public int TicketsToBuy { get; set; }
    }
}