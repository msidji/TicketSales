using TicketSales.Core.Application.Contracts.Persistence;
using TicketSales.Core.Domain.Entities;
using TicketSales.Core.Infrastructure.Persistence;

namespace TicketSales.Core.Infrastructure.Repositories
{
    public class ConcertRepository : BaseRepository<Concert>, IConcertRepository
    {
        public ConcertRepository(CoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}