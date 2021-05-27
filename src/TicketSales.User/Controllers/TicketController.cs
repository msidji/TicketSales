using Microsoft.AspNetCore.Mvc;
using TicketSales.User.Services;

namespace TicketSales.User.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketMessageStore _ticketMessageStore;
        private readonly ConcertMessageStore _concertMessageStore;

        public TicketController(TicketMessageStore ticketMessageStore, ConcertMessageStore concertMessageStore)
        {
            _ticketMessageStore = ticketMessageStore;
            _concertMessageStore = concertMessageStore;
        }

        // GET: TicketController
        public ActionResult Index()
        {
            var tickets = _ticketMessageStore.Tickets;
            foreach (var ticket in tickets)
            {
                var concert = _concertMessageStore.GetConcertById(ticket.ConcertId);
                if (concert != null)
                {
                    ticket.ConcertName = concert.Name;
                }
            }

            return View(tickets);
        }
    }
}