using System;
using System.ComponentModel.DataAnnotations;

namespace TicketSales.Admin.Models
{
    public class ConcertViewModel
    {
        [Display(Name = "Reference Id")] public long Id { get; set; }

        [Display(Name = "Name")] public string Name { get; set; }

        [Display(Name = "Num of Tickets")] public int Tickets { get; set; }

        [Display(Name = "Sold")] public int TicketsSold { get; set; }

        [Display(Name = "Date")] public DateTime? EventDate { get; set; }
    }
}