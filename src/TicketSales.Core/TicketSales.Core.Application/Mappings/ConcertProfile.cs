using AutoMapper;
using TicketSales.Core.Application.Features.Concerts.Commands.CreateConcert;
using TicketSales.Core.Domain.Entities;
using TicketSales.Messages.Commands;
using TicketSales.Messages.Events;

namespace TicketSales.Core.Application.Mappings
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            CreateMap<CreateConcertCommand, Concert>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.TicketsInSale, opt => opt.MapFrom(s => s.TicketsInSale))
                .ForMember(d => d.EventDate, opt => opt.MapFrom(s => s.EventDate))
                .ReverseMap();

            CreateMap<CreateConcertRequest, Concert>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Command.Name))
                .ForMember(d => d.TicketsInSale, opt => opt.MapFrom(s => s.Command.TicketsInSale))
                .ForMember(d => d.EventDate, opt => opt.MapFrom(s => s.Command.EventDate))
                .ReverseMap();

            CreateMap<ConcertCreatedEvent, Concert>()
                .ReverseMap()
                .ForMember(d => d.ConcertId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.TicketsInSale, opt => opt.MapFrom(s => s.TicketsInSale))
                .ForMember(d => d.EventDate, opt => opt.MapFrom(s => s.EventDate))
                .ForAllOtherMembers(d => d.Ignore());

            CreateMap<CreateConcertCommand, CreateConcertRequest>()
                .ForMember(d => d.Command, opt => opt.MapFrom(s => s))
                .ReverseMap();
        }
    }
}