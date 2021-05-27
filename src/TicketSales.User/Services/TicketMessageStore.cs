using System.Collections.Generic;
using TicketSales.User.Models;

namespace TicketSales.User.Services
{
    public class TicketMessageStore
    {
        public List<TicketViewModel> Tickets { get; private set; } = new List<TicketViewModel>();

        public void AddTicket(TicketViewModel ticketViewModel)
        {
            if (Tickets == null)
            {
                Tickets = new List<TicketViewModel>();
            }

            Tickets.Add(ticketViewModel);
        }
    }
}