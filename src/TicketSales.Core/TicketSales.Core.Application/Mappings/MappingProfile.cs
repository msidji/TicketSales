using AutoMapper;
using TicketSales.Core.Application.Features.Concerts.Commands.CreateConcert;
using TicketSales.Core.Application.Features.Concerts.Commands.SellTickets;
using TicketSales.Core.Domain.Entities;
using TicketSales.Messages.Commands;

namespace TicketSales.Core.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Concert, CreateConcertRequest>().ReverseMap();
            CreateMap<CreateConcertCommand, CreateConcertRequest>()
                .ForMember(d => d.Command, opt => opt.MapFrom(s => s))
                .ReverseMap();

            CreateMap<BuyTicketsForConcertCommand, SellTicketsRequest>()
                .ForMember(d => d.Command, opt => opt.MapFrom(s => s))
                .ReverseMap();
        }
    }
}