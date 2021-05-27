using System;
using System.ComponentModel.DataAnnotations;

namespace TicketSales.Admin.Models
{
    public class NewConcertViewModel
    {
        [Required] [Display(Name = "Name")] public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Num of Tickets")]
        public int Tickets { get; set; }

        [Display(Name = "Date")] public DateTime? EventDate { get; set; }
    }
}