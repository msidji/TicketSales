using AutoMapper;
using TicketSales.Admin.Models;
using TicketSales.Core.Domain.Entities;
using TicketSales.Messages.Commands;
using TicketSales.Messages.Events;

namespace TicketSales.Admin.Mappings
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            CreateMap<NewConcertViewModel, CreateConcertCommand>()
                .ForMember(d => d.Concert,
                    opt => opt.MapFrom(s => new Concert()
                    {
                        Name = s.Name,
                        TicketsInSale = s.Tickets,
                        EventDate = s.EventDate,
                    }))
                .ReverseMap();

            CreateMap<ConcertCreatedEvent, ConcertViewModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Concert.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Concert.Name))
                .ForMember(d => d.EventDate, opt => opt.MapFrom(s => s.Concert.EventDate))
                .ForMember(d => d.Tickets, opt => opt.MapFrom(s => s.Concert.TicketsInSale))
                .ForMember(d => d.TicketsSold, opt => opt.MapFrom(s => s.Concert.TicketsSold))
                .ReverseMap();
        }
    }
}