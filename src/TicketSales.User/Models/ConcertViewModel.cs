using System;
using System.ComponentModel.DataAnnotations;

namespace TicketSales.User.Models
{
    public class ConcertViewModel
    {
        [Display(Name = "Reference Id")] public long Id { get; set; }

        [Display(Name = "Name")] public string Name { get; set; }

        [Display(Name = "Tickets Available")] public int TicketsAvailable { get; set; }

        [Display(Name = "Date")] public DateTime? EventDate { get; set; }
    }
}