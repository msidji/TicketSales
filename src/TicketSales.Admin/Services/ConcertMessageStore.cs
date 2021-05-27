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
    }
}