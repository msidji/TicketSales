using System;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using TicketSales.Admin.Models;
using TicketSales.Admin.Services;
using TicketSales.Messages.Events;

namespace TicketSales.Admin.Consumers
{
    public class ConcertCreatedConsumer : IConsumer<ConcertCreatedEvent>
    {
        private readonly ConcertMessageStore _store;
        private readonly IMapper _mapper;
        private readonly ILogger<ConcertCreatedConsumer> _logger;

        public ConcertCreatedConsumer(ConcertMessageStore store, IMapper mapper, ILogger<ConcertCreatedConsumer> logger)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Consume(ConsumeContext<ConcertCreatedEvent> context)
        {
            _logger.LogInformation($"Consuming ConcertCreatedEvent with id {context.Message.Id}.");

            var newConcert = _mapper.Map<ConcertViewModel>(context.Message);
            var existingConcert = _store.Concerts.Find(x => x.Id == newConcert.Id);
            if (existingConcert == null)
            {
                _store.AddConcert(newConcert);
            }

            return Task.CompletedTask;
        }
    }
}