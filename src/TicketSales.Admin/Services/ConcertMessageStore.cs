using System.Collections.Generic;
using TicketSales.Admin.Models;

namespace TicketSales.Admin.Services
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

        public void SellTickets(long concertId, int numOfTicketsToSell)
        {
            var concert = GetConcertById(concertId);
            if (concert != null)
            {
                concert.TicketsSold += numOfTicketsToSell;
            }
        }
    }
}