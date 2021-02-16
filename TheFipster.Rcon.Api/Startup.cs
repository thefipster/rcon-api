using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Quartz;
using SimpleInjector;
using System;
using System.IO;
using System.Reflection;
using TheFipster.Rcon.Api.Abstractions;
using TheFipster.Rcon.Api.Components;
using TheFipster.Rcon.Api.Decorators;
using TheFipster.Rcon.Api.Models.Config;
using TheFipster.Rcon.Api.Repository;

namespace TheFipster.Rcon.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly Container _container;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _container = new Container();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RconSettings>(_configuration.GetSection(RconSettings.SettingsKey));
            services.Configure<StorageSettings>(_configuration.GetSection(StorageSettings.SettingsKey));

            services.AddControllers();

            services.AddSimpleInjector(_container, options =>
            {
                options.AddAspNetCore()
                       .AddControllerActivation();
            });

            ConfigureContainer();

            services.AddHealthChecks();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "RCON API",
                    Description = "Interact with games using the RCON protocol.",
                    Contact = new OpenApiContact
                    {
                        Name = "thefipster",
                        Email = "hey@thefipster.com",
                        Url = new Uri("https://www.thefipster.com"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddQuartz();
        }

        private void ConfigureContainer()
        {
            _container.Register<IStorageProvider, StorageProvider>(Lifestyle.Singleton);
            _container.Register<IHistoryStore, HistoryStore>(Lifestyle.Scoped);
            _container.Register<ICronJobStore, CronJobStore>(Lifestyle.Scoped);

            _container.Register<IRconClient, RconClient>(Lifestyle.Scoped);
            _container.RegisterDecorator<IRconClient, RconClientTimer>(Lifestyle.Scoped);
            _container.RegisterDecorator<IRconClient, RconClientHistoryRecorder>(Lifestyle.Scoped);
            _container.RegisterDecorator<IRconClient, RconClientLogger>(Lifestyle.Scoped);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RCON API");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
