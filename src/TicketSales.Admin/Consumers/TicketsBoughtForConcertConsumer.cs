using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using TicketSales.Admin.Services;
using TicketSales.Messages.Events;

namespace TicketSales.Admin.Consumers
{
    public class TicketsBoughtForConcertConsumer : IConsumer<TicketsBoughtForConcertEvent>
    {
        private readonly ConcertMessageStore _store;
        private readonly ILogger<TicketsBoughtForConcertConsumer> _logger;

        public TicketsBoughtForConcertConsumer(ConcertMessageStore store,
            ILogger<TicketsBoughtForConcertConsumer> logger)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Consume(ConsumeContext<TicketsBoughtForConcertEvent> context)
        {
            _logger.LogInformation($"Admin Consuming TicketsBoughtForConcertEvent with id {context.Message.Id}.");

            _store.SellTickets(context.Message.ConcertId, context.Message.TicketsToBuy);

            return Task.CompletedTask;
        }
    }
}