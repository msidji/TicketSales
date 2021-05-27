namespace TicketSales.Messages.Commands
{
    public class BuyTicketsForConcertCommand
    {
        public long ConcertId { get; set; }
        public string Username { get; set; }
        public int TicketsToBuy { get; set; }
    }
}