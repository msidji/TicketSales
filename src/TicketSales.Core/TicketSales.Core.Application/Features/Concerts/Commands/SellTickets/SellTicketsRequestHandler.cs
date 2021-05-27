using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TicketSales.Core.Application.Contracts.Persistence;
using TicketSales.Core.Application.Exceptions;
using TicketSales.Core.Domain.Entities;

namespace TicketSales.Core.Application.Features.Concerts.Commands.SellTickets
{
    public class SellTicketsRequestHandler : IRequestHandler<SellTicketsRequest, bool>
    {
        private readonly IConcertRepository _concertRepository;
        private readonly ILogger<SellTicketsRequest> _logger;

        public SellTicketsRequestHandler(IConcertRepository concertRepository, ILogger<SellTicketsRequest> logger)
        {
            _concertRepository = concertRepository ?? throw new ArgumentNullException(nameof(concertRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(SellTicketsRequest request, CancellationToken cancellationToken)
        {
            if (request.Command == null)
            {
                throw new ArgumentNullException(nameof(request.Command), "Command is null.");
            }

            var concert = await _concertRepository.GetByIdAsync(request.Command.ConcertId);
            if (concert == null)
            {
                throw new NotFoundException(nameof(Concert), request.Command.ConcertId);
            }

            var isSellTicketsSuccess = concert.SellTickets(request.Command.TicketsToBuy);
            if (isSellTicketsSuccess)
            {
                await _concertRepository.UpdateAsync(concert);
            }

            _logger.LogInformation(
                $"Tickets, {request.Command.TicketsToBuy}, successfully sold for concert {request.Command.ConcertId}");

            return isSellTicketsSuccess;
        }
    }
}