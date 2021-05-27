using System.Collections.Generic;
using TicketSales.User.Models;

namespace TicketSales.User.Services
{
    public class ConcertMessageStore
    {
        public List<ConcertViewModel> Concerts { get; private set; } = new List<ConcertViewModel>();

        public void AddConcert(ConcertViewModel concertViewModel)
        {
            if (Concerts == null)
            {
                Concerts = new List<ConcertViewModel>();
            }

            Concerts.Add(concertViewModel);
        }

        public ConcertViewModel GetConcertById(long id)
        {
            var concert = Concerts.Find(x => x.Id == id);
            return concert ?? new ConcertViewModel();
        }

        public NewTicketViewModel GetNewTicketForConcert(long concertId)
        {
            var concert = GetConcertById(concertId);
            var newTicket = new NewTicketViewModel()
            {
                Concert = concert ?? new ConcertViewModel(),
            };

            return newTicket;
        }

        public void SellTickets(long concertId, int numOfTicketsToSell)
        {
            var concert = GetConcertById(concertId);
            if (concert != null)
            {
                concert.TicketsAvailable -= numOfTicketsToSell;
            }
        }
    }
}