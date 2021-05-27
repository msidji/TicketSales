using AutoMapper;
using TicketSales.Messages.Events;
using TicketSales.User.Models;

namespace TicketSales.User.Mappings
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            CreateMap<ConcertCreatedEvent, ConcertViewModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Concert.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Concert.Name))
                .ForMember(d => d.EventDate, opt => opt.MapFrom(s => s.Concert.EventDate))
                .ForMember(d => d.TicketsAvailable,
                    opt => opt.MapFrom(s => s.Concert.TicketsInSale - s.Concert.TicketsSold))
                .ReverseMap();
        }
    }
}