using System;
using System.Text;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TicketSales.Admin.Consumers;
using TicketSales.Admin.Mappings;
using TicketSales.Admin.Services;
using TicketSales.Messages.Commands;
using TicketSales.Messages.Common;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace TicketSales.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Get Environment Configurations
            var rabbitMqHost = Configuration["RabbitMq:Host"];
            var rabbitMqVirtualHost = Configuration["RabbitMq:VirtualHost"];
            var rabbitMqHostUsername = Configuration["RabbitMq:HostUsername"];
            var rabbitMqHostPassword = Configuration["RabbitMq:HostPassword"];

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Add Data Stores
            services.AddSingleton<TestMessageStore>();
            services.AddSingleton<ConcertMessageStore>();

            // Add MessageBroker Services
            services.AddMassTransit(x =>
            {
                x.AddConsumer<TestEventHandler>();
                x.AddConsumer<ConcertCreatedConsumer>();
                x.AddConsumer<TicketsBoughtForConcertConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(rabbitMqHost, rabbitMqVirtualHost, h =>
                    {
                        h.Username(rabbitMqHostUsername);
                        h.Password(rabbitMqHostPassword);
                    });

                    cfg.ReceiveEndpoint(host, EventBusConstants.AdminQueue, e =>
                    {
                        e.PrefetchCount = 16;

                        e.ConfigureConsumer<TestEventHandler>(provider);
                        e.ConfigureConsumer<ConcertCreatedConsumer>(provider);
                        e.ConfigureConsumer<TicketsBoughtForConcertConsumer>(provider);

                        var virtualHost = string.IsNullOrEmpty(rabbitMqVirtualHost) ? "" : rabbitMqVirtualHost + "/";
                        var uriBase = new StringBuilder($"rabbitmq://localhost/{virtualHost}");
                        EndpointConvention.Map<TestCommand>(new Uri(uriBase + EventBusConstants.CoreQueue));
                        EndpointConvention.Map<CreateConcertCommand>(new Uri(uriBase + EventBusConstants.CoreQueue));
                    });

                    // or, configure the endpoints by convention
                    //cfg.ConfigureEndpoints(provider); // TODO: This caused Event to be published twice
                }));
            });

            services.AddHostedService<BusService>();

            // Add Mapper Configuration
            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new ConcertProfile()); });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Add Framework Services
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}