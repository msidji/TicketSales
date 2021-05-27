using System;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using TicketSales.Messages.Events;
using TicketSales.User.Models;
using TicketSales.User.Services;

namespace TicketSales.User.Consumers
{
    public class TicketsBoughtForConcertConsumer : IConsumer<TicketsBoughtForConcertEvent>
    {
        private readonly ConcertMessageStore _concertMessageStore;
        private readonly TicketMessageStore _ticketMessageStore;
        private readonly IMapper _mapper;
        private readonly ILogger<TicketsBoughtForConcertConsumer> _logger;

        public TicketsBoughtForConcertConsumer(ConcertMessageStore store, TicketMessageStore ticketMessageStore,
            IMapper mapper, ILogger<TicketsBoughtForConcertConsumer> logger)
        {
            _concertMessageStore = store ?? throw new ArgumentNullException(nameof(store));
            _ticketMessageStore = ticketMessageStore ?? throw new ArgumentNullException(nameof(_ticketMessageStore));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Consume(ConsumeContext<TicketsBoughtForConcertEvent> context)
        {
            _logger.LogInformation($"User Consuming TicketsBoughtForConcertEvent with id {context.Message.Id}.");

            _concertMessageStore.SellTickets(context.Message.ConcertId, context.Message.TicketsToBuy);

            _ticketMessageStore.AddTicket(_mapper.Map<TicketViewModel>(context.Message));

            return Task.CompletedTask;
        }
    }
}