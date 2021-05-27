using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TicketSales.Core.Application.Contracts.Persistence;
using TicketSales.Core.Domain.Entities;

namespace TicketSales.Core.Application.Features.Concerts.Commands.CreateConcert
{
    public class CreateConcertRequestHandler : IRequestHandler<CreateConcertRequest, Concert>
    {
        private readonly IConcertRepository _concertRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateConcertRequest> _logger;

        public CreateConcertRequestHandler(IConcertRepository concertRepository, IMapper mapper,
            ILogger<CreateConcertRequest> logger)
        {
            _concertRepository = concertRepository ?? throw new ArgumentNullException(nameof(concertRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Concert> Handle(CreateConcertRequest request, CancellationToken cancellationToken)
        {
            if (request.Command == null)
            {
                throw new ArgumentNullException(nameof(request.Command), "Command is null.");
            }

            if (request.Command.Concert == null)
            {
                throw new ArgumentNullException(nameof(request.Command.Concert), "Concert is null.");
            }

            var concertEntity = _mapper.Map<Concert>(request.Command.Concert);
            var newConcert = await _concertRepository.AddAsync(concertEntity);

            _logger.LogInformation($"Concert with id {newConcert.Id} is successfully created.");

            return newConcert;
        }
    }
}