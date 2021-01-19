using System;
using System.Collections.Specialized;
using BookShop.Api.Jobs;
using BookShop.Api.Rabbit;
using BookShop.Data;
using BookShop.Logic;
using BookShop.MessageContract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace BookShop.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransitConfiguration<ProvideBookResponseConsumer>(Configuration);

            services.AddSingleton<IBookProvider, ProvideBookRequestProducer>();
            services.AddSingleton(sp => new BookShopContextFactory(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton(sp =>
                new BookShopService(1, 
                    sp.GetService<BookShopContextFactory>() ?? throw new ArgumentNullException(),
                    sp.GetService<IBookProvider>() ?? throw new ArgumentNullException()));

            services.AddSingleton<IJobFactory, InjectableJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>(isp =>
            {
                var properties = new NameValueCollection
                {
                    ["quartz.scheduler.interruptJobsOnShutdownWithWait"] = "true",
                    ["quartz.scheduler.interruptJobsOnShutdown"] = "true"
                };
                return new StdSchedulerFactory(properties);
            });
            services.AddSingleton<CheckBookShopStateJob>();
            services.AddHostedService<QuartzHostedService>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}