using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketSales.Core.Application.Contracts.Persistence;
using TicketSales.Core.Infrastructure.Persistence;
using TicketSales.Core.Infrastructure.Repositories;

namespace TicketSales.Core.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<CoreDbContext>(options =>
                options.UseInMemoryDatabase("ticketSalesInMemoryDb"));

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IConcertRepository, ConcertRepository>();

            return services;
        }
    }
}