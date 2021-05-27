using System;

namespace TicketSales.Core.Domain.Entities
{
    public class Concert : BaseEntity
    {
        public string Name { get; set; }
        public long TicketsInSale { get; set; }
        public long TicketsSold { get; set; }
        public DateTime? EventDate { get; set; }

        public bool SellTickets(int numOfTicketsToSell)
        {
            if (numOfTicketsToSell <= 0)
            {
                return false;
                /*throw new ArgumentException("Number of tickets to buy has to be bigger than 0.", nameof(numOfTicketsToSell));*/
            }

            if (TicketsSold + numOfTicketsToSell > TicketsInSale)
            {
                return false;
            }

            TicketsSold += numOfTicketsToSell;
            return true;
        }
    }
}