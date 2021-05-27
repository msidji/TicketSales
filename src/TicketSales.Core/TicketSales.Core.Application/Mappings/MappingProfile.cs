using AutoMapper;
using TicketSales.Core.Application.Features.Concerts.Commands.SellTickets;
using TicketSales.Messages.Commands;

namespace TicketSales.Core.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BuyTicketsForConcertCommand, SellTicketsRequest>()
                .ForMember(d => d.Command, opt => opt.MapFrom(s => s))
                .ReverseMap();
        }
    }
}