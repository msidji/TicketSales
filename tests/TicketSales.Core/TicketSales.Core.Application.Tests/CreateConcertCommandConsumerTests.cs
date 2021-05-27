using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using TicketSales.Core.Application.Consumers;
using TicketSales.Messages.Commands;
using TicketSales.Messages.Events;
using Xunit;

namespace TicketSales.Core.Application.Tests
{
    public class CreateConcertCommandConsumerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<CreateConcertConsumer>> _loggerMock;
        private readonly CreateConcertConsumer _consumer;
        private readonly Mock<ConsumeContext<CreateConcertCommand>> _consumeCtx;

        public CreateConcertCommandConsumerTests()
        {
            _mediator = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<CreateConcertConsumer>>();
            _consumer = new CreateConcertConsumer(_mediator.Object, _mapperMock.Object, _loggerMock.Object);
            _consumeCtx = new Mock<ConsumeContext<CreateConcertCommand>>();
        }

        [Fact]
        public async Task ConsumeCreateConcertCommand_PublishesConcertCreatedEvent()
        {
            // Arange

            // Act
            await _consumer.Consume(_consumeCtx.Object);

            // Assert
            _consumeCtx.Verify(x => x.Publish(It.IsAny<ConcertCreatedEvent>(), default(CancellationToken)));
        }
    }
}