using System;

namespace TicketSales.Core.Domain.Entities
{
    public class Concert : BaseEntity
    {
        public string Name { get; set; }
        public long TicketsInSale { get; set; }
        public long TicketsSold { get; set; }
        public DateTime? EventDate { get; set; }
    }
}