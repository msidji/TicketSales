using System.ComponentModel.DataAnnotations;

namespace TicketSales.User.Models
{
    public class TicketViewModel
    {
        [Display(Name = "Reference Id")] public long Id { get; set; }

        [Display(Name = "Username")] public string Username { get; set; }

        [Display(Name = "Concert Id")] public long ConcertId { get; set; }

        [Display(Name = "Concert")] public string ConcertName { get; set; }

        [Display(Name = "Bought")] public int TicketsBought { get; set; }
    }
}