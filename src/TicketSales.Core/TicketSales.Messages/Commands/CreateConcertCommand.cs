using System;

namespace TicketSales.Messages.Commands
{
    public class CreateConcertCommand
    {
        public string Name { get; set; }
        public long TicketsInSale { get; set; }
        public DateTime? EventDate { get; set; }
    }
}