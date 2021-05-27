using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketSales.Core.Application;
using TicketSales.Core.Application.Consumers;
using TicketSales.Core.Infrastructure;
using TicketSales.Messages.Common;

namespace TicketSales.Core.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Get Environment Configurations
            var rabbitMqHost = Configuration["RabbitMq:Host"];
            var rabbitMqVirtualHost = Configuration["RabbitMq:VirtualHost"];
            var rabbitMqHostUsername = Configuration["RabbitMq:HostUsername"];
            var rabbitMqHostPassword = Configuration["RabbitMq:HostPassword"];

            // Add Custom Services
            services.AddInfrastructureServices();
            services.AddApplicationServices();

            // Add MessageBroker Services
            services.AddMassTransit(x =>
            {
                x.AddConsumer<TestCommandHandler>();
                x.AddConsumer<CreateConcertConsumer>();
                x.AddConsumer<BuyTicketsForConcertConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(rabbitMqHost, rabbitMqVirtualHost, h =>
                    {
                        h.Username(rabbitMqHostUsername);
                        h.Password(rabbitMqHostPassword);
                    });

                    cfg.ReceiveEndpoint(host, EventBusConstants.CoreQueue, e =>
                    {
                        e.PrefetchCount = 16;

                        e.ConfigureConsumer<TestCommandHandler>(provider);
                        e.ConfigureConsumer<CreateConcertConsumer>(provider);
                        e.ConfigureConsumer<BuyTicketsForConcertConsumer>(provider);
                    });

                    // or, configure the endpoints by convention
                    cfg.ConfigureEndpoints(provider);
                }));
            });

            services.AddHostedService<BusService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) => { await context.Response.WriteAsync("Hello World!"); });
        }
    }
}