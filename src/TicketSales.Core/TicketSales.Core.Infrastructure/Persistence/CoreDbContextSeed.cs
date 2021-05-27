using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketSales.Core.Infrastructure.Persistence.Seeds;

namespace TicketSales.Core.Infrastructure.Persistence
{
    public class CoreDbContextSeed
    {
        public static async Task SeedAsync(CoreDbContext dbContext, ILogger<CoreDbContext> logger)
        {
            await ConcertSeed.SeedAsync(dbContext, logger);
        }
    }
}