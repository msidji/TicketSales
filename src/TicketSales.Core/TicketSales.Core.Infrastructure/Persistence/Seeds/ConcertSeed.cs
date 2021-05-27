using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using TicketSales.Core.Domain.Entities;

namespace TicketSales.Core.Infrastructure.Persistence.Seeds
{
    public static class ConcertSeed
    {
        public static async Task SeedAsync(CoreDbContext dbContext, ILogger<CoreDbContext> logger)
        {
            if (!dbContext.Concerts.Any())
            {
                await dbContext.Concerts.AddRangeAsync(GetConcertsSeeds());
                await dbContext.SaveChangesAsync();

                logger.LogInformation(
                    "Seed database data associated with context {DbContextName} and set of {DbSetType}",
                    nameof(CoreDbContext), typeof(Concert));
            }
        }

        private static IEnumerable<Concert> GetConcertsSeeds()
        {
            return new List<Concert>()
            {
                new Concert()
                {
                    Name = "Wolfmania 2021",
                    TicketsInSale = 10,
                    EventDate = new DateTime(2021, 6, 15)
                },
                new Concert()
                {
                    Name = "FinTech Serbia Fest",
                    TicketsInSale = 50,
                    EventDate = new DateTime(2021, 8, 20)
                }
            };
        }
    }
}