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

            CreateMap<TicketsBoughtForConcertEvent, TicketViewModel>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.ConcertId, opt => opt.MapFrom(s => s.ConcertId))
                .ForMember(d => d.ConcertName, opt => opt.Ignore())
                .ForMember(d => d.Username, opt => opt.MapFrom(s => s.Username))
                .ForMember(d => d.TicketsBought, opt => opt.MapFrom(s => s.TicketsToBuy));
        }
    }
}