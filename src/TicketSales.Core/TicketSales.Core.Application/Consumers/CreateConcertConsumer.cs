using System;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using TicketSales.Core.Application.Features.Concerts.Commands.CreateConcert;
using TicketSales.Messages.Commands;
using TicketSales.Messages.Events;

namespace TicketSales.Core.Application.Consumers
{
    public class CreateConcertConsumer : IConsumer<CreateConcertCommand>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateConcertConsumer> _logger;

        public CreateConcertConsumer(IMediator mediator, IMapper mapper, ILogger<CreateConcertConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<CreateConcertCommand> context)
        {
            var result = await _mediator.Send(_mapper.Map<CreateConcertRequest>(context.Message));
            _logger.LogInformation("CreateConcertCommand consumed successfully. Created Concert Id : {newConcertId}",
                result?.Id);

            await context.Publish(_mapper.Map<ConcertCreatedEvent>(result));

            _logger.LogInformation("ConcertCreatedEvent published successfully.");
        }
    }
}