using System;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using TicketSales.Core.Application.Features.Concerts.Commands.SellTickets;
using TicketSales.Messages.Commands;
using TicketSales.Messages.Events;

namespace TicketSales.Core.Application.Consumers
{
    public class BuyTicketsForConcertConsumer : IConsumer<BuyTicketsForConcertCommand>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<BuyTicketsForConcertConsumer> _logger;

        public BuyTicketsForConcertConsumer(IMediator mediator, IMapper mapper,
            ILogger<BuyTicketsForConcertConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<BuyTicketsForConcertCommand> context)
        {
            var result = await _mediator.Send(_mapper.Map<SellTicketsRequest>(context.Message));
            _logger.LogInformation("BuyTicketsForConcertCommand consumed successfully.");

            if (result)
            {
                await context.Publish(new TicketsBoughtForConcertEvent()
                {
                    TicketsToBuy = context.Message.TicketsToBuy,
                    Username = context.Message.Username,
                    ConcertId = context.Message.ConcertId
                });
                _logger.LogInformation("TicketsBoughtForConcertEvent published successfully.");
            }
        }
    }
}