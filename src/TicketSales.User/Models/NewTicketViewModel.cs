using System.ComponentModel.DataAnnotations;

namespace TicketSales.User.Models
{
    public class NewTicketViewModel
    {
        [Display(Name = "Reference Id")] public long Id { get; set; }

        [Required] public ConcertViewModel Concert { get; set; }

        [Required] public long ConcertId { get; set; }

        [Required] [Display(Name = "Username")] public string Username { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        [Display(Name = "Number of Tickets")]
        public int TicketsToBuy { get; set; }

        public NewTicketViewModel()
        {
            Concert = new ConcertViewModel();
        }

        public NewTicketViewModel(ConcertViewModel concert)
        {
            Concert = concert;
        }
    }
}