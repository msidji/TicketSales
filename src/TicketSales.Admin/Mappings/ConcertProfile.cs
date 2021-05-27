using AutoMapper;
using TicketSales.Admin.Models;
using TicketSales.Messages.Commands;
using TicketSales.Messages.Events;

namespace TicketSales.Admin.Mappings
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            CreateMap<NewConcertViewModel, CreateConcertCommand>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.TicketsInSale, opt => opt.MapFrom(s => s.Tickets))
                .ForMember(d => d.EventDate, opt => opt.MapFrom(s => s.EventDate))
                .ReverseMap();

            CreateMap<ConcertCreatedEvent, ConcertViewModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ConcertId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.EventDate, opt => opt.MapFrom(s => s.EventDate))
                .ForMember(d => d.Tickets, opt => opt.MapFrom(s => s.TicketsInSale))
                .ForMember(d => d.TicketsSold, opt => opt.MapFrom(s => s.TicketsSold))
                .ForAllOtherMembers(d => d.Ignore());
        }
    }
}